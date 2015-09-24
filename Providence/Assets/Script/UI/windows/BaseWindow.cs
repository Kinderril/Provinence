using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BaseWindow : MonoBehaviour
{
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    public virtual void Init()
    {
        gameObject.SetActive(true);
    }
}

