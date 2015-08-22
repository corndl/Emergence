using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour
{
    #region Properties
    [SerializeField]
    float m_Speed = 0;
    #endregion

    #region API
    public void KillBug()
    {
        // smart stuff to do
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

    void Move()
    {
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
