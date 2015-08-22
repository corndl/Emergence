using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Properties
    [SerializeField]
    BugManager m_BugManager = null;
    #endregion

    #region API
    public static GameManager Instance 
    { 
        get 
        { 
            return s_Instance; 
        } 
    }

    public void StartGame()
    {

    }
    #endregion

    #region Unity
    void Awake()
    {
        s_Instance = this;
        m_BugManager = BugManager.Instance;
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
    #endregion

    #region Private
    private static GameManager s_Instance = null;
    #endregion
}
