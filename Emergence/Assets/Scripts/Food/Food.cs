using UnityEngine;
using System.Collections;
using System;

public class Food : MonoBehaviour
{
    #region Properties
    [SerializeField]
    FoodStateModel[] m_Models = new FoodStateModel[0];
    [SerializeField]
    int m_HP = 100;
    [SerializeField]
    int m_CurrentModel = 0;
    #endregion

    #region API
    public void Eat(int hp)
    {
        m_HP -= hp;

        if (m_HP < m_Models[m_CurrentModel].threshold && m_CurrentModel < m_Models.Length - 1)
        {
            m_CurrentModel++;
            InstantiateModel();
        }        
    }
    #endregion

    #region Unity
    void Start () 
    {
        InstantiateModel();
	}
	
	void Update () 
    {
    }
    #endregion

    #region Private
    GameObject m_Model = null;

    [Serializable]
    class FoodStateModel
    {
        public int threshold;
        public GameObject modelPrefab;
    }

    void InstantiateModel()
    {
        if (m_Model != null)
        {
            Destroy(m_Model);
        }
        m_Model = Instantiate(m_Models[m_CurrentModel].modelPrefab);
        m_Model.gameObject.transform.SetParent(gameObject.transform);
        m_Model.gameObject.transform.localPosition = Vector3.zero;
    }
    #endregion
}
