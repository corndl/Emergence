using UnityEngine;
using System.Collections;

public class Pheromone : MonoBehaviour
{
    #region Properties
    [SerializeField]
    GameManager m_GameManager = null;

    public PheromoneType Type
    {
        get
        {
            return m_PheromoneType;
        }
        set
        {
            m_PheromoneType = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return gameObject.transform.position;
        }
        set
        {
            gameObject.transform.position = value;
        }
    }

    public Vector3 Target
    {
        get
        {
            return m_Target;
        }
        set
        {
            m_Target = value;
        }
    }

    public Bug Dropper
    {
        get
        {
            return m_Dropper;
        }
        set
        {
            m_Dropper = value;
        }
    }
    #endregion

    #region API
    public enum PheromoneType
    {
        Home,
        Food,
        Mating,
        Ennemy
    }

    public Pheromone Duplicate(Bug dropper, Vector3 position)
    {
        GameObject copyGameobject = Instantiate(m_GameManager.PheromonePrefab);
        Pheromone copy = copyGameobject.GetComponent<Pheromone>();
        copy.Type = Type;
        copy.Position = position;
        copy.Target = Target;
        copy.Dropper = dropper;

        return copy;
    }
    #endregion

    #region Unity
    void Awake()
    {
        m_GameManager = GameObject.Find("Services").GetComponentInChildren<GameManager>();
    }

	void Start () {
	
	}
	
	void Update () {

    }
    #endregion

    #region Private
    PheromoneType m_PheromoneType;
    Bug m_Dropper = null;
    Vector3 m_Target = new Vector3();
    #endregion
}
