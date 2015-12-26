using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class AgentControl : BaseControl
{
    NavMeshAgent agent;

    protected override void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        base.Init();
    }

    public override bool MoveTo(Vector3 v)
    {
        this.TargetDirection = (v - transform.position).normalized;
        var movingOk = agent.SetDestination(v);
        //Debug.Log("AGENT control move to:" + v + " from:" + transform.position + "   movingOk:" + movingOk);
        return movingOk;
    }

    public override bool IsPathComplete()
    {
        var isComplete = agent.remainingDistance < 1;
        return isComplete;
    }

    protected override void UpdateCharacter()
    {

        this.TargetDirection = (agent.destination - transform.position).normalized;
        var angel = Vector3.Angle(TargetDirection, this.Direction);

        if (angel > 3)
            RotateToTarget(transform,TargetDirection);

        UpdateAnimator(agent.velocity);
    }
    public override void SetSpped(float speed)
    {

        if (agent != null)
            agent.speed = speed;
    }


    public override void Stop(bool setSpeedToZero = true)
    {
        if (agent != null)
        {
            if (setSpeedToZero)
            {
                agent.Stop();
                agent.speed = 0;
            }
            else
            {
                agent.ResetPath();
            }
        }

    }
}

