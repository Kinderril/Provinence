using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    public Slider TImeSlider;
    public Slider HealthSlider;

    public void Init()
    {
        MainController.Instance.level.OnLeft += OnLeft;
        MainController.Instance.MainHero.OnGetHit += OnHeroHit;
    }

    private void OnHeroHit(int arg1, int arg2)
    {
        HealthSlider.value = (float) arg1/(float) arg2;
    }

    private void OnLeft(float arg1, float arg2)
    {
        var v = 1f - arg1/arg2;
        //Debug.Log(" val: " + v + "  arg1:" + arg1 + "  " + arg2);
        TImeSlider.value = v;
    }

}
