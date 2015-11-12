using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class VisualEffect : MonoBehaviour
{
    public ParticleSystem pssystem;
    public void Action()
    {
        pssystem.enableEmission = true;
    }

    public void End()
    {
        Destroy(gameObject);
    }
}

