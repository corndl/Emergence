using UnityEngine;
using System.Collections;

public class Maillet : Arme {

    #region Properties
    #endregion

    #region API

    public virtual void Attack()
    {
        base.Attack();
    }



    #endregion

    #region Unity
    // Use this for initialization
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        _StopAttack();
    }
    #endregion

    #region Private

    protected override void _StopAttack()
    {
        base._StopAttack();
    }
    #endregion
}
