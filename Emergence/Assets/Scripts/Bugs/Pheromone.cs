using UnityEngine;
using System.Collections;

public class Pheromone
{
    #region Properties
    [SerializeField]
    float m_LifeDuracyInSeconds = 10f;

    public float LifeDuracyInSeconds
    {
        get
        {
            return m_LifeDuracyInSeconds;
        }
    }

    public bool HasToDie
    {
        get
        {
            return (Time.time - m_LifeDuracyInSeconds > m_InstanciationTime);
        }
    }

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
    public Pheromone(PheromoneType type, Vector3 target, Bug dropper)
    {
        Type = type;
        Target = target;
        Dropper = dropper;

        m_InstanciationTime = Time.time;
    }
    public enum PheromoneType
    {
        Home,
        Food,
        Mating,
        Ennemy
    }
    #endregion

    #region Private
    PheromoneType m_PheromoneType;
    Bug m_Dropper = null;
    Vector3 m_Target = new Vector3();
    float m_InstanciationTime = 0f;
    #endregion
}
