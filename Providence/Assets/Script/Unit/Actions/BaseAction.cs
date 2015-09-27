using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BaseAction
{
    protected Unit owner;
    protected Action endCallback;

    public BaseAction(Unit owner,Action endCallback)
    {
        this.endCallback = endCallback;
        this.owner = owner;
    }
    public virtual void Update()
    {
        
    }

    public virtual void End(string msg = " end action ")
    {
        //Debug.Log(msg);
        endCallback();
    }
}

