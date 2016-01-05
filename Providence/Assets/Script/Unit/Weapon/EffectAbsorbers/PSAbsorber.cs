using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PSAbsorber : BaseEffectAbsorber
{
    public ParticleSystem PSystem;
    public override void Play()
    {
        PSystem.Play();
    }

    public override void Stop()
    {
        PSystem.Stop();
    }
}

