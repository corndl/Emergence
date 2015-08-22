using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bug : MonoBehaviour
{
    #region Properties
    [SerializeField]
    float m_Speed = 100;
    [SerializeField]
    int m_EnvironmentLayer = 9;
    [SerializeField]
    int m_PheromoneLayer = 10;
    [SerializeField]
    List<Pheromone> m_Pheromones = new List<Pheromone>();
    [SerializeField]
    GameManager m_GameManager = null;
    #endregion

    #region API
    /// <summary>
    /// Set Bug as dead and stops all movements.
    /// </summary>
    public void KillBug()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_State = BugState.Dead;
    }

    /// <summary>
    /// Destroy the bug's gameobject.
    /// </summary>
    public void DestroyBug()
    {
        Destroy(this.gameObject);
    }
    #endregion

    #region Unity
    void Awake()
    {
        m_GameManager = GameObject.Find("Services").GetComponentInChildren<GameManager>();
    }

	void Start () {
        m_TargetPosition = gameObject.transform.position;
        m_Rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        switch (m_Behaviour)
        {
            case (BugBehaviour.Searching):
                Search();
                break;
            case (BugBehaviour.Fleeing):
                break;
            case (BugBehaviour.Gathering):
                break;
            case (BugBehaviour.Mating):
                break;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.layer == m_EnvironmentLayer)
        {
            m_State = BugState.OnGround;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.layer == m_EnvironmentLayer)
        {
            m_State = BugState.MidAir;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == m_PheromoneLayer)
        {
            Pheromone pheromone = coll.gameObject.GetComponent<Pheromone>();
            if (pheromone == null)
            {
                return;
            }
            ProcessPheromone(pheromone);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.layer == m_PheromoneLayer)
        {
            Pheromone pheromone = coll.gameObject.GetComponent<Pheromone>();
            if (pheromone == null)
            {
                return;
            }
            ReDropPheromone(pheromone);
        }
    }
    #endregion

    #region Private
    Vector3 m_TargetPosition = new Vector3();
    Rigidbody m_Rigidbody = null;
    BugState m_State = BugState.MidAir;
    BugBehaviour m_Behaviour = BugBehaviour.Searching;

    float m_TimeLastDecision = 0f;

    enum BugState
    {
        Crouched,
        Dead,
        OnGround,
        MidAir
    }

    enum BugBehaviour
    {
        Searching,
        Gathering,
        Mating,
        Fleeing
    }

    #region Behaviours
    /// <summary>
    /// Find target position and move towards it. (Currently random)
    /// </summary>
    void Search()
    {
        if (m_State != BugState.OnGround)
        {
            m_Rigidbody.velocity = Vector3.zero;
            if (m_State == BugState.MidAir)
            {
                m_Rigidbody.velocity = Vector3.down * m_Speed;
            }
            return;
        }

        Vector3 position = gameObject.transform.position;

        // Choose random direction for now
        if (Time.time - m_TimeLastDecision > 0.1f)
        {
            m_TimeLastDecision = Time.time;
            
            m_Rigidbody.velocity = Vector3.zero;
            m_TargetPosition = new Vector3(position.x + Random.Range(-5, 5), position.y, position.z + Random.Range(-5, 5));
        }

        Vector3 force = m_TargetPosition - position;
        force.Normalize();
        force *= m_Speed;

        m_Rigidbody.velocity = force;
    }
    #endregion

    #region Pheromones
    void ReDropPheromone(Pheromone pheromone)
    {

    }

    void DropPheromone(Pheromone.PheromoneType type)
    {
        GameObject pheromoneGameobject = Instantiate(m_GameManager.PheromonePrefab);
        Pheromone pheromone = pheromoneGameobject.GetComponent<Pheromone>();
        pheromone.Type = type;
        pheromone.Position = gameObject.transform.position;
        pheromone.Dropper = this;
        ProcessPheromone(pheromone);
    }

    void ProcessPheromone(Pheromone pheromone)
    {
        if (m_Pheromones.Contains(pheromone))
        {
            return;
        }

        switch (pheromone.Type)
        {
            case (Pheromone.PheromoneType.Ennemy):
                m_Behaviour = BugBehaviour.Fleeing;
                break;
            case (Pheromone.PheromoneType.Mating):
                if (pheromone.Dropper != this)
                {
                    m_TargetPosition = pheromone.Target;
                }
                break;
            case (Pheromone.PheromoneType.Food):
                m_Behaviour = BugBehaviour.Gathering;
                m_TargetPosition = pheromone.Target;
                break;
            case (Pheromone.PheromoneType.Home):
                if (m_Behaviour == BugBehaviour.Gathering)
                {
                    m_TargetPosition = pheromone.Target;
                }
                break;
        }
    }
    #endregion
    #endregion
}
