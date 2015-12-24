using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class AttackDistance : AttackAction
{
    private float farRange;
    private AttackStatus status;


    public AttackDistance(BaseMonster owner, Unit target, Action endCallback) 
        : base(owner, target, endCallback)
    {
        farRange = owner.curWeapon.Parameters.range;
    }

    public override void Update()
    {
        base.Update();
        UpdateDistance();
    }

    public void UpdateDistance()
    {
        base.Update();
        if (isMoving)
        {
            bool isFar = curRange < farRange;
            //Debug.Log("In Range: " + isClose + "  " + isFar + "  " + curRange + "  " + closeRange + "   " + farRange);
            if (!isFar)
            {
                if (UnityEngine.Random.Range(0, 100) < 90)
                {
                    status = AttackStatus.shoot;
                    isMoving = true;
                    DoShoot();
                }
                else
                {
                    status = AttackStatus.comeOut;
                    ComeOutTarget();
                }
            }
            else
            {
                ComeToTarget();
            }
        }
    }

    protected override void OnShootEnd(Unit obj)
    {
        base.OnShootEnd(obj);
        isMoving = false;
    }

    private void Stop()
    {
        owner.Control.Stop();
    }

    private void ComeOutTarget()
    {
        var x = UnityEngine.Random.Range(-1f, 1f);
        var y = UnityEngine.Random.Range(-1f, 1f);
        Vector3 dirToEscape = new Vector3(x,0,y);
        MoveToTarget(dirToEscape.normalized * 1 + owner.transform.position);
        
    }

    private void ComeToTarget()
    {
        Vector3 dirToTrg = target.transform.position - owner.transform.position ;
        //no so close
        MoveToTarget(owner.transform.position + dirToTrg.normalized * owner.curWeapon.Parameters.range);
    }
}

