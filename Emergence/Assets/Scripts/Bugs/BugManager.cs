using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class BugManager : MonoBehaviour
{
    #region Properties
    [SerializeField]
    List<Bug> m_BugList = new List<Bug>();
    [SerializeField]
    GameObject m_BugPrefab = null;
    [SerializeField]
    Vector3 m_InitialSpawnPoint = Vector3.zero;
    [SerializeField]
    GameManager m_GameManager = null;
    #endregion

    #region API
    public static BugManager Instance
    {
        get
        {
            return s_Instance;
        }
    }

    /// <summary>
    /// Create a bug with genes from 2 parents.
    /// </summary>
    /// <param name="dad">Parent 1</param>
    /// <param name="mom">Parent 2</param>
    public void CreateNewBug(Bug dad, Bug mom)
    {
        if (dad == null && mom == null)
        {
            CreateNewBug();
            return;
        }
        else if (dad == null && mom != null)
        {
            CreateNewBug(mom);
            return;
        }
        else if (dad != null && mom == null)
        {
            CreateNewBug(dad);
            return;
        }

        Bug bug = InstantiateBug();
        if (bug == null)
        {
            return;
        }

        // Genetics
        bug.Speed = dad.Speed.intChildren();
        bug.TimeTilReadyForMating = mom.TimeTilReadyForMating.intChildren();
        bug.FoodHPPerByte = dad.FoodHPPerByte.intChildren();
    }

    /// <summary>
    /// Create a bug with genes from 1 parent.
    /// </summary>
    /// <param name="parent"></param>
    public void CreateNewBug(Bug parent)
    {
        Bug bug = InstantiateBug();
        if (parent == null)
        {
            return;
        }

        // Genetics
        bug.Speed = parent.Speed.intChildren();
        bug.TimeTilReadyForMating = parent.TimeTilReadyForMating.intChildren();
        bug.FoodHPPerByte = parent.FoodHPPerByte.intChildren();
    }

    /// <summary>
    /// Create a bug with no parent.
    /// </summary>
    public void CreateNewBug()
    {
        Bug bug = InstantiateBug();
    }

    public void CreateNewBugs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            InstantiateBug();
        }
    }

    /// <summary>
    /// Set bug to Dead.
    /// </summary>
    /// <param name="bug"></param>
    public void KillBug(Bug bug)
    {
        if (bug == null)
        {
            return;
        }

        m_DeadBugs.Add(bug);
        m_BugList.Remove(bug);

        onBugsAlive.Invoke(m_BugList.Count.ToString());
        onKilledBugs.Invoke(m_DeadBugs.Count.ToString());

        bug.KillBug();

        if (m_BugList.Count == 0)
        {
            m_GameManager.EndGame();
        }
    }

    /// <summary>
    /// Destroy a bug and remove it from the Bug List.
    /// </summary>
    /// <param name="bug"></param>
    public void DestroyBug(Bug bug)
    {
        if (bug == null)
        {
            return;
        }

        m_DeadBugs.Remove(bug);
        m_BugList.Remove(bug);
        m_BugsParent.name = "Bugs (" + m_BugList.Count + ")";
        onBugsAlive.Invoke(m_BugList.Count.ToString());
        bug.DestroyBug();
    }

    public void DestroyAllBugs()
    {
        List<Bug> tmp = m_BugList;
        foreach (Bug b in tmp)
        {
            b.DestroyBug();
        }
    }
    #endregion

    #region Events
    [Serializable]
    public class StringEvent : UnityEvent<string> { }
    [SerializeField]
    public StringEvent onKilledBugs = new StringEvent();
    [SerializeField]
    public StringEvent onBugsAlive = new StringEvent();
    #endregion

    #region Unity
    void Awake()
    {
        m_BugsParent = new GameObject("Bugs");
    }

	void Start () {
        
	}
	
	void Update () {

    }

    
    #endregion

    #region Private
    private static BugManager s_Instance = null;
    GameObject m_BugsParent = null;
    List<Bug> m_DeadBugs = new List<Bug>();

    /// <summary>
    /// Instantiate a bug and add it to the bug list.
    /// </summary>
    /// <returns></returns>
    Bug InstantiateBug(Vector3 SpawnPoint)
    {
        if (m_BugList.Count >= m_GameManager.MaxBugsCount)
        {
            m_GameManager.EndGame();
            return null;
        }
        GameObject bugGameobject = Instantiate(m_BugPrefab);
        bugGameobject.transform.SetParent(m_BugsParent.transform);
        bugGameobject.transform.position = SpawnPoint;

        Bug newBug = bugGameobject.GetComponent<Bug>();
        m_BugList.Add(newBug);

        m_BugsParent.name = "Bugs (" + m_BugList.Count + ")";
        onBugsAlive.Invoke(m_BugList.Count.ToString());

        newBug.TimeTilReadyForMating = newBug.TimeTilReadyForMating.intChildren();

        return newBug;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Bug InstantiateBug()
    {
        return InstantiateBug(m_InitialSpawnPoint);
    }
    #endregion
}
