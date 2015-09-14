using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Hero : Unit
{
    void FixedUpdate()
    {
        if (action != null)
            action.Update();
    }

    public void GetChest(Chest chest)
    {

    }
}
