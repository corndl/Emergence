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
	}
	
	void Update () {
        Move();
    }
    #endregion

    #region Private
    Vector3 m_TargetPosition = new Vector3();
    void Move()
    {
        // Choose random direction for now
        Vector3 position = gameObject.transform.position;
        if (new Vector2(m_TargetPosition.x - position.x, m_TargetPosition.z - position.z).magnitude < 0.1f)
        {
            m_TargetPosition = new Vector3(position.x + Random.Range(-5, 5), position.y, position.z + Random.Range(-5, 5));
        }

        gameObject.transform.Translate((m_TargetPosition - position) * Time.deltaTime * m_Speed);
    }
    #endregion
}
