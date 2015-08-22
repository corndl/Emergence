using UnityEngine;
using System.Collections;

public class Maillet : Arme {

    #region Properties

    [SerializeField] private BugManager bm;
    #endregion

    #region API

    public virtual void Attack()
    {
        base.Attack();
    }



    #endregion

    #region Unity
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected  override void Update()
    {
        base.Update();
    }

    void OnTriggerEnter(Collider collider)
    {
       _StopAttack();
       Bug bug = collider.GetComponentInChildren<Bug>();
        if(bug != null)
            bm.KillBug(bug);
    }
    #endregion

    #region Private

    protected override void _StopAttack()
    {
        base._StopAttack();
    }
    #endregion
}
