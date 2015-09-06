using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    public Slider TImeSlider;

    public void Init()
    {
        MainController.Instance.level.OnLeft += OnLeft;
    }

    private void OnLeft(float arg1, float arg2)
    {
        var v = 1f - arg1/arg2;
        //Debug.Log(" val: " + v + "  arg1:" + arg1 + "  " + arg2);
        TImeSlider.value = v;
    }

}
