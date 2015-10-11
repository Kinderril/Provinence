using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ParameterElement : MonoBehaviour
{
    public Text label;

    public void Init(ParamType param,float val)
    {
        label.text = param + "   " + val.ToString("0");
    }
}

