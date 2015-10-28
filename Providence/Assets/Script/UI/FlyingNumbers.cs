using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class FlyingNumbers : MonoBehaviour
{
    private Text text;
    private Action OnDead;
    public void Init(float Count, Color textColor, string add = "", Action OnDead = null)
    {
        this.OnDead = OnDead;
        if (text == null)
        {
            text = GetComponent<Text>();
            if (text == null)
            {
                text = GetComponentInChildren<Text>();
            }
        }
        text.text = add + (Mathf.Abs(Count)).ToString("0");
        text.color = textColor;
    }

    public void EndAnimation()
    {
        if (OnDead != null)
        {
            OnDead();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
