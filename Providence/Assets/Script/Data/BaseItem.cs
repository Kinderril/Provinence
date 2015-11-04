using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class BaseItem
{

    protected const char DELEM = '/';
    protected const char DPAR = '{';
    protected const char MDEL = '>';
    public string icon = "";
    public string name = "";
    protected bool isEquped;
    public int cost;
    public Slot Slot;


    public abstract char FirstChar();
    public abstract void Activate(Hero hero);
    public abstract string Save();
    public bool IsEquped
    {
        get { return isEquped; }
        set
        {
            isEquped = value;
            Save();
        }
    }
}

