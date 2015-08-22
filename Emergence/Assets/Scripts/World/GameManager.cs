using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Properties
    [Header("Bugs")]
    [SerializeField]
    BugManager m_BugManager = null;
    [SerializeField]
    int m_BugsCount = 0;
    [SerializeField]
    public GameObject PheromonePrefab = null;
    #endregion

    #region API
    public static GameManager Instance 
    { 
        get 
        { 
            return s_Instance; 
        } 
    }

    public BugManager BugManager
    {
        get
        {
            return m_BugManager;
        }
    }

    public void StartGame()
    {
        m_BugManager.CreateNewBugs(m_BugsCount);
    }
    #endregion

    #region Unity
    void Awake()
    {
        s_Instance = this;
    }

	void Start () {
        StartGame();
	}
	
	void Update () {

    }
    #endregion

    #region Private
    private static GameManager s_Instance = null;
    #endregion
}
