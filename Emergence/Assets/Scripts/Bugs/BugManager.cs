using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BugManager : MonoBehaviour
{
    #region Properties
    List<Bug> m_BugList = new List<Bug>();
    #endregion

    #region API
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

    public void CreateNewBug(Bug parent)
    {
        Bug bug = InstantiateBug();
        if (parent == null)
        {
            return;
        }

        // Genetics
    }

    public void CreateNewBug()
    {
        Bug bug = InstantiateBug();
    }

    public void KillBug(Bug bug)
    {
        if (bug == null)
        {
            return;
        }

        m_BugList.Remove(bug);
        bug.KillBug();
    }
    #endregion

    #region Unity
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
    #endregion

    #region Private
    Bug InstantiateBug()
    {
        Bug newBug = new Bug();
        m_BugList.Add(newBug);
        return newBug;
    }
    #endregion
}
