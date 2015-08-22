using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour
{
    #region API
    public void KillBug()
    {
        // smart stuff to do
        Destroy(this.gameObject);
    }
    #endregion

    #region Unity
	void Start () {
	
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
        if ((m_TargetPosition - position).magnitude < 0.1f)
        {
            m_TargetPosition = new Vector3(position.x + Random.Range(-5, 5), position.y, position.z + Random.Range(-5, 5));
        }

        gameObject.transform.Translate((m_TargetPosition - position) * Time.deltaTime);
    }
    #endregion
}
