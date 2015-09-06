using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BaseMonster : Unit
{
    protected override void Dead()
    {
        base.Dead();
        MainController.Instance.level.PowerLeft -= 1f;
    }
}

