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

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
