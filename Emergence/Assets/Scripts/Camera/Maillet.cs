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
        if (!_isAttacking)
            return;

       Bug bug = collider.GetComponent<Bug>() ?? collider.GetComponentInChildren<Bug>() ?? collider.GetComponentInParent<Bug>();
        if (bug != null)
        {
            _nbKill ++;
            BugManager.KillBug(bug);
            Debug.Log("KillingSpree !!! : "+ bug);
        }
        if(collider.gameObject.layer == 9)
            _StopAttack();
    }
    #endregion
    
    #region Private

    protected override void _StopAttack()
    {
        base._StopAttack();
        GameObject smock = Instantiate(_particulePrefab,transform.position,transform.rotation) as GameObject;
        smock.GetComponent<ParticleSystem>().Play();
        Destroy(smock,1f);
        
        _AudioSource.clip = _nbKill > 0 ? _sonKill : _sonNoKill;
        _AudioSource.Play();
        
    }
    #endregion
}
