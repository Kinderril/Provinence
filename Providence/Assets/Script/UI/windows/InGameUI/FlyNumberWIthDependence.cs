using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class FlyNumberWIthDependence : MonoBehaviour
{
    public FlyingNumbers incFlyingNumbers;
    private Transform dependence;
    private Camera cam;
    public void Init(Transform dependence, float Count, Color textColor, string add = "")
    {
        cam = MainController.Instance.MainCamera;
        this.dependence = dependence;
        incFlyingNumbers.Init(Count, textColor, add, OnDead);
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (dependence != null)
            transform.position = cam.WorldToScreenPoint(dependence.position);
    }
}

