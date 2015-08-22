using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PheromoneManager : MonoBehaviour
{
    #region Properties
    [SerializeField]
    List<Pheromone> m_Pheromones = new List<Pheromone>();
    #endregion

    #region API
    public void DropPheromone(Pheromone pheromone, Vector3 position)
    {

    }
    #endregion

    #region Unity
	void Start () {
	
	}
	
	void Update () {

    }
    #endregion

    #region Private

    #endregion
}
