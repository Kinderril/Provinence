using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class FlyingNumbers : MonoBehaviour
{
    private Text text;
    public void Init(float Count,Color textColor,string add = "")
    {
        if (text == null)
            text = GetComponent<Text>();
        text.text = add + Count.ToString("0");
        text.color = textColor;
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }
}
