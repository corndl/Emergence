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
    [SerializeField]
    WorldMatrix m_WorldMatrix;
    [SerializeField]
    float m_MinimumDistanceFleeing = 20f;
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
        m_WorldMatrix = m_GameManager.Matrix;
    }

	void Start () {
        m_TargetPosition = gameObject.transform.position;
        m_Rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        OutOfBonds();

        Move();
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
        /*
        if (coll.gameObject.layer == m_PheromoneLayer)
        {
            Pheromone pheromone = coll.gameObject.GetComponent<Pheromone>();
            if (pheromone == null)
            {
                return;
            }
            ProcessPheromone(pheromone);
        }//*/
    }

    void OnTriggerExit(Collider coll)
    {
        /*
        if (coll.gameObject.layer == m_PheromoneLayer)
        {
            Pheromone pheromone = coll.gameObject.GetComponent<Pheromone>();
            if (pheromone == null)
            {
                return;
            }
            ReDropPheromone(pheromone);
        }//*/
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

    void OutOfBonds()
    {
        if (gameObject.transform.position.y < -10)
        {
            m_GameManager.BugManager.DestroyBug(this);
        }
    }

    #region Behaviours
    /// <summary>
    /// Find target position and move towards it. (Currently random)
    /// </summary>
    void Move()
    {
        DetectPlayer();

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

            switch (m_Behaviour)
            {
                case (BugBehaviour.Searching):
                    Search();
                    break;
                case (BugBehaviour.Fleeing):
                    Flee();
                    break;
                case (BugBehaviour.Gathering):
                    Search();
                    break;
                case (BugBehaviour.Mating):
                    Search();
                    break;
            }
        }

        Vector3 force = m_TargetPosition - position;
        force.Normalize();
        force *= m_Speed;

        m_Rigidbody.velocity = force;
    }

    void Search()
    {
        Vector3 position = gameObject.transform.position;
        m_TargetPosition = new Vector3(position.x + Random.Range(-5, 5), position.y, position.z + Random.Range(-5, 5));
    }

    void Flee()
    {
        Vector3 playerPosition = m_GameManager.Player.transform.position;
        Vector3 position = gameObject.transform.position;

        m_TargetPosition = new Vector3(position.x - playerPosition.x, position.y, position.z - playerPosition.z);
    }
    #endregion

    #region Pheromones
    void GetPheromonesCurrentTile()
    {
        WorldMatrix.Tile tile = m_WorldMatrix.GetTile(gameObject.transform.position);
        foreach (Pheromone pheromone in tile.Pheromones)
        {
            ProcessPheromone(pheromone);
        }
    }

    void ReDropPheromone(Pheromone pheromone)
    {
        DropPheromone(pheromone.Type, pheromone.Target);
    }

    void DropPheromone(Pheromone.PheromoneType type, Vector3 target)
    {
        Pheromone pheromone = new Pheromone(type, target, this);
        m_WorldMatrix.DropPheromone(pheromone, gameObject.transform.position);
        ProcessPheromone(pheromone);
    }

    void ProcessPheromone(Pheromone pheromone)
    {
        // Flee for your life
        if (m_Behaviour == BugBehaviour.Fleeing)
        {
            return;
        }

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

    #region Senses
    void DetectPlayer()
    {
        Vector3 playerPosition = m_GameManager.Player.transform.position;
        Vector2 playerXZ = new Vector2(playerPosition.x, playerPosition.z);
        Vector2 bugXZ = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

        float distance = Mathf.Sqrt((playerXZ - bugXZ).sqrMagnitude);
        if (distance < m_MinimumDistanceFleeing)
        {
            m_Behaviour = BugBehaviour.Fleeing;
            DropPheromone(Pheromone.PheromoneType.Ennemy, playerPosition);
        }
        else
        {
            m_Behaviour = BugBehaviour.Searching;
        }
    }
    #endregion
    #endregion
}
