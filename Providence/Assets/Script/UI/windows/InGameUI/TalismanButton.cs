using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TalismanButton : MonoBehaviour
{
    private TalismanItem TalismanItem;
    private Talisman talicLogic;
    public Slider sliderReady;
    public Button button;
    public Image icon;
    
    public void Init(TalismanItem talic)
    {
        this.TalismanItem = talic;
        talicLogic = new Talisman(TalismanItem);
        talicLogic.OnReady += OnReady;
    }

    private void OnReady(bool isReady, float percent)
    {
        sliderReady.gameObject.SetActive(!isReady);
        sliderReady.value = percent;
        button.interactable = isReady;
    }


    public void OnClick()
    {
        talicLogic.Use();
    }

    void OnDestroy()
    {
        talicLogic.OnReady -= OnReady;
    }
}

