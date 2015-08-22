using UnityEngine;
using System.Collections;

public class Arme : MonoBehaviour {

    #region Properties
    #endregion

    #region API

    public virtual void Attack()
    {
        Animator.SetBool("Attack", true);
    }


    
    #endregion

    #region Unity
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region Private
    protected Animator Animator;
    protected virtual void _StopAttack()
    {
        Animator.SetBool("Attack", false);
    }
    #endregion
}
