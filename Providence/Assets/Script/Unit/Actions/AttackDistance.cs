using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public enum AttackDistanceCurMobe
{
    attack,
    none,
    move,
}
public class AttackDistance : AttackAction
{
    private float farRange;
    private AttackStatus status;
    AttackDistanceCurMobe curMode = AttackDistanceCurMobe.none;

    public AttackDistance(BaseMonster owner, Unit target, Action endCallback) 
        : base(owner, target, endCallback)
    {
        farRange = owner.curWeapon.Parameters.range * owner.curWeapon.Parameters.range;
    }

    public override void Update()
    {
        base.Update();
        UpdateDistance();
    }

    protected override void MoveEnd()
    {
        isMoving = false;
        curMode = AttackDistanceCurMobe.none;
    }

    public void UpdateDistance()
    {
        base.Update();
        curRange = (owner.transform.position - target.transform.position).sqrMagnitude;
        bool canAttack = curRange < farRange;
        //Debug.Log(" agent.curRange:" + curRange + "   farRange:" + farRange + "   curMode:" + curMode + "   isMoving:" + isMoving);
        switch (curMode)
        {
            case AttackDistanceCurMobe.move:
                if (canAttack)
                {
                    owner.Control.Stop(false);
                    curMode = AttackDistanceCurMobe.attack;
                    DoShoot();
                }
                else
                {
                    if (!isMoving)
                        ComeToTarget();
                }
                break;
            case AttackDistanceCurMobe.none:
                if (canAttack)
                {

                    owner.Control.Stop(false);
                    curMode = AttackDistanceCurMobe.attack;
                    DoShoot();
                    /*
                    if (UnityEngine.Random.Range(0, 100) < 90)
                    {
                    }
                    else
                    {
                        curMode = AttackDistanceCurMobe.move;
                        ComeOutTarget();
                    }*/
                }
                else
                {
                    ComeToTarget();
                }
        
                break;
            case AttackDistanceCurMobe.attack:
                //if (!canAttack)
                //{
                //    ComeToTarget();
                //}
                break;
        }
        //Debug.Log("In Range: " + isFar + "  " + curRange + "  status:" + status + "   isMoving:" + isMoving);
        
    }

    protected override void OnShootEnd(Unit obj)
    {
        base.OnShootEnd(obj);
        curMode = AttackDistanceCurMobe.none;
        //Debug.Log(" OnShootEnd  curMode:" + curMode);
        isMoving = false;
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
        Vector3 dirToTrg = target.transform.position - owner.transform.position;
        //Debug.Log(" ComeToTarget  isMoving:" + isMoving + "   " + dirToTrg);
        curMode = AttackDistanceCurMobe.move;
        //no so close
        MoveToTarget(owner.transform.position + dirToTrg,false);
        //MoveToTarget(target.transform.position,false);
    }
}

