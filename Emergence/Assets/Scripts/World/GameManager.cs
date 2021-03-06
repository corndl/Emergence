﻿using UnityEngine;
using UnityEngine.Events;
using System;
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
    int m_MaxBugsCount = 500;
    [SerializeField]
    public GameObject FoodPrefab = null;
    [SerializeField]
    CameraController m_Player = null;
    [SerializeField]
    WorldMatrix m_Matrix = null;
    #endregion

    #region Events
    [SerializeField]
    public UnityEvent onStartGame = new UnityEvent();
    [SerializeField]
    public UnityEvent onEndGame = new UnityEvent();
    #endregion

    #region API
    public int MaxBugsCount
    {
        get
        {
            return m_MaxBugsCount;
        }
    }
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

    public CameraController Player
    {
        get
        {
            return m_Player;
        }
    }

    public WorldMatrix Matrix
    {
        get
        {
            return m_Matrix;
        }
    }
    public void StartGame()
    {
        m_Food = Instantiate(FoodPrefab);
        Time.timeScale = 1;
        onStartGame.Invoke();
        m_BugManager.CreateNewBugs(m_BugsCount);
    }

    public void EndGame()
    {
        Destroy(m_Food);
        Time.timeScale = 0;
        m_BugManager.DestroyAllBugs();
        onEndGame.Invoke();
    }
    #endregion

    #region Unity
    void Awake()
    {
        s_Instance = this;
    }

	void Start () {
        //StartGame();
	}
	
	void Update () {

    }
    #endregion

    #region Private
    private static GameManager s_Instance = null;
    GameObject m_Food = null;
    #endregion
}
