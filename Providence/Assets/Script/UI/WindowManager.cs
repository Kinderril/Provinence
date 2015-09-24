using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct WindowT
{
    public MainState state;
    public BaseWindow window;
}

public class WindowManager : Singleton<WindowManager>
{
    public WindowT[] windows;
    private BaseWindow currentWindow;

    public void OpenWindow(MainState state)
    {
        if (currentWindow != null)
        {
            currentWindow.Close();
        }
        var window = windows.FirstOrDefault(x => x.state == state).window;
        window.Init();
        currentWindow = window;

    }
}

