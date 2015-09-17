using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class StayAction : BaseAction
{
    public StayAction(Unit owner, Action endCallback) 
        : base(owner, endCallback)
    {
        var timer = MainController.Instance.TimerManager.MakeTimer(TimeSpan.FromSeconds(UnityEngine.Random.Range(2, 10)));
        timer.OnTimer += endCallback;
    }
}

