using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour
{
    #region Properties
    [SerializeField]
    float m_Speed = 100;
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
	void Start () {
        m_TargetPosition = gameObject.transform.position;
        m_Rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        Move();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.layer == 9)
        {
            m_State = BugState.OnGround;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.layer == 9)
        {
            m_State = BugState.MidAir;
        }
    }
    #endregion

    #region Private
    Vector3 m_TargetPosition = new Vector3();
    Rigidbody m_Rigidbody = null;
    BugState m_State = BugState.MidAir;

    float m_TimeLastDecision = 0f;

    enum BugState
    {
        Crouched,
        Dead,
        OnGround,
        MidAir
    }

    /// <summary>
    /// Find target position and move towards it. (Currently random)
    /// </summary>
    void Move()
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
}
