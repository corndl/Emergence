using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMatrix : MonoBehaviour
{
    #region Properties
    [SerializeField]
    Terrain m_Terrain = null;
    [SerializeField]
    int m_ResolutionX = 100;
    [SerializeField]
    int m_ResolutionY = 100;
    #endregion

    #region API
    public class Tile
    {
        public int X;
        public int Y;
        public List<Pheromone> Pheromones = new List<Pheromone>();
    }

    public Tile GetTile(Vector3 position)
    {
        int stepX = (int)(m_Plan.x / m_ResolutionX);
        int X = (int)(position.x / stepX);
        int stepY = (int)(m_Plan.y / m_ResolutionY);
        int Y = (int)(position.z / stepY);

        List<Pheromone> toRemove = new List<Pheromone>();

        foreach (Pheromone pheromone in m_Matrix[X, Y].Pheromones)
        {
            if (pheromone.HasToDie)
            {
                toRemove.Add(pheromone);
            }
        }

        foreach (Pheromone pheromone in toRemove)
        {
            m_Matrix[X, Y].Pheromones.Remove(pheromone);
        }

        return m_Matrix[X, Y];
    }

    public void DropPheromone(Pheromone pheromone, Vector3 position)
    {
        Tile tile = GetTile(position);
        tile.Pheromones.Add(pheromone);
    }
    #endregion

    #region Unity
    void Awake()
    {
        m_Matrix = new Tile[m_ResolutionX, m_ResolutionY];
        Vector3 terrainSize = m_Terrain.GetComponent<Collider>().bounds.size;
        m_Plan = new Vector2(terrainSize.x, terrainSize.z);

        FillMatrix();
    }

    void Start () {
	
	}
	
	void Update () {

    }
    #endregion

    #region Private
    Tile[,] m_Matrix;
    Vector2 m_Plan = Vector2.zero;

    void FillMatrix() {
        for (int i = 0; i < m_ResolutionX; i++)
        {
            for (int j = 0; j < m_ResolutionY; j++)
            {
                Tile t = new Tile();
                t.X = (int)(m_Plan.x / m_ResolutionX) * i;
                t.Y = (int)(m_Plan.y / m_ResolutionY) * j;
                m_Matrix[i, j] = t;
            }
        }
    }
    #endregion
}
