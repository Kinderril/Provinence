using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class IShopExecute : MonoBehaviour
{
    public Sprite Icon;
    public int value;

    public  virtual void Execute(int parameter)
    {
        
    }
}

