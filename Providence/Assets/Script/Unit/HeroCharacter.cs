using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Effects;

public class HeroCharacter : Character
{
    private const float CONST_SEC = 1.4f;
    private float RemainBackWalkTimeSec = 0;
    private bool isBackDir;


    private void SetBackDir(bool value)
    {
        if (value)
        {
            RemainBackWalkTimeSec = CONST_SEC;
            isBackDir = value;
        }
        else
        {
            isBackDir = value;
        }
    }

    protected override void UpdateCharacter()
    {
        base.UpdateCharacter();
        if (RemainBackWalkTimeSec > 0)
        {
            RemainBackWalkTimeSec -= Time.deltaTime;
            if (RemainBackWalkTimeSec < 0)
            {
                SetBackDir(false);
            }
        }
        UpdateRotation(dir);
    }

    protected override void UpdateRotation(Vector3 dir)
    {
        base.UpdateRotation(isBackDir ? -dir : dir);
    }

    public void SetDir(Vector3 nDir)
    {
        var angel = Vector3.Angle(nDir, dir);
        if (angel > 90)
        {
            SetBackDir(true);
        }
        else
        {
            SetBackDir(false);
        }
        
    }
}

