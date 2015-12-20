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
        this.Direction = v.normalized;
        return agent.SetDestination(v);
    }

    public override bool IsPathComplete()
    {
        return agent.remainingDistance < 1;
    }

    protected override void UpdateCharacter()
    {
        var angel = Vector3.Angle(TargetDirection, this.Direction);

        if (angel > 5)
            RotateToTarget(transform,TargetDirection);

        UpdateAnimator(agent.velocity);
    }
    public override void SetSpped(float speed)
    {
        if (agent != null)
            agent.speed = speed;
    }

    public override void Stop()
    {
        if (agent != null)
        {
            agent.Stop();
            agent.speed = 0;
        }

    }
}

