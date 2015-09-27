using System;
using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    private Action action;

    public void EndPlayAttack()
    {
        action();
    }

    public void StartPlayAttack(Action action)
    {
        this.action = action;
    }
}
