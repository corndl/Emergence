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
	
	void Update () {
        Move();
    }
    #endregion

    #region Private
    Vector3 m_TargetPosition = new Vector3();
    Rigidbody m_Rigidbody = null;
    BugState m_State = BugState.Alive;

    enum BugState
    {
        Alive,
        Crouched,
        Dead
    }

    /// <summary>
    /// Find target position and move towards it. (Currently random)
    /// </summary>
    void Move()
    {
        if (m_State == BugState.Dead)
        {
            return;
        }

        // Choose random direction for now
        Vector3 position = gameObject.transform.position;
        
        m_Rigidbody.velocity = Vector3.zero;
        if (new Vector2(m_TargetPosition.x - position.x, m_TargetPosition.z - position.z).magnitude < 0.5f)
        {
            m_TargetPosition = new Vector3(position.x + Random.Range(-5, 5), position.y, position.z + Random.Range(-5, 5));
        }
        Vector3 force = m_TargetPosition - position;
        force.Normalize();
        force *= m_Speed;

        m_Rigidbody.AddForce(force);        
    }
    #endregion
}
