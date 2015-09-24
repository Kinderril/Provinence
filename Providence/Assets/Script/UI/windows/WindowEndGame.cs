using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class WindowEndGame : BaseWindow
{
    public void OnOkClick()
    {
        WindowManager.Instance.OpenWindow(MainState.mission);
    }
    public void OnStartClick()
    {
        WindowManager.Instance.OpenWindow(MainState.start);
    }
}

