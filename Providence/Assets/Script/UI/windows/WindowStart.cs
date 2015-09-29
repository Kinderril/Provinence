using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WindowStart : BaseWindow
{
    public void OnStartClick()
    {
        WindowManager.Instance.OpenWindow(MainState.mission);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}

