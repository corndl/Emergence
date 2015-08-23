using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotateGameOver : MonoBehaviour
{

    #region Properties
    [SerializeField]
    Image m_Background = null;
    #endregion

    // Use this for initialization
	void Start () {
        m_Background.transform.localScale *= 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_Background != null)
        {
            m_Background.transform.Rotate(Vector3.forward, 10f);
        }
	}
}
