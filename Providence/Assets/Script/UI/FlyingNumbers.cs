using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class FlyingNumbers : PoolElement
{
    public Text text;
    public Image image;
    private Action OnDead;

    public void Init(float Count, Color textColor, string add = "", Action OnDead = null)
    {
        base.Init();
        this.OnDead = OnDead;
        text.text = add + (Mathf.Abs(Count)).ToString("0");
        text.color = textColor;
    }
    public void Init(string txt, Color textColor,  Sprite spr,Action OnDead = null)
    {
        base.Init();
        this.OnDead = OnDead;
        text.text = txt;
        text.color = textColor;
        image.sprite = spr;
    }

    public void EndAnimation()
    {
        if (OnDead != null)
        {
            OnDead();
        }
        else
        {
            EndUse();
        }
    }
}
