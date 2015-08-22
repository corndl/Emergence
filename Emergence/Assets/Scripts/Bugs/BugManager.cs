using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BugManager : MonoBehaviour
{
    #region Properties
    [SerializeField]
    List<Bug> m_BugList = new List<Bug>();
    [SerializeField]
    GameObject BugPrefab = null;
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
        // Genetics
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
    }

    /// <summary>
    /// Create a bug with no parent.
    /// </summary>
    public void CreateNewBug()
    {
        Bug bug = InstantiateBug();
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

        bug.KillBug();
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

        m_BugList.Remove(bug);
        bug.DestroyBug();
    }
    #endregion

    #region Unity
	void Start () {
        CreateNewBug();
	}
	
	void Update () {

    }
    #endregion

    #region Private
    private static BugManager s_Instance = null;

    /// <summary>
    /// Instantiate a bug and add it to the bug list.
    /// </summary>
    /// <returns></returns>
    Bug InstantiateBug()
    {
        GameObject bugGameobject = Instantiate(BugPrefab);
        Bug newBug = bugGameobject.GetComponent<Bug>();
        m_BugList.Add(newBug);
        return newBug;
    }
    #endregion
}
