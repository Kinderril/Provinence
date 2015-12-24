using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BossUnit : BaseMonster
{
    protected override void Dead()
    {
        base.Dead();
        MainController.Instance.EndLevel(true);
    }
}

