using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WindowStart : BaseWindow
{

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnClearAllClick()
    {
        PlayerPrefs.DeleteAll();
    }
}

