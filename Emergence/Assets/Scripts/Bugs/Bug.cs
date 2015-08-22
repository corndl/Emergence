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

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
