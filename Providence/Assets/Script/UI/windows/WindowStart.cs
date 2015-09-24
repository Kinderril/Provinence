using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WindowStart : BaseWindow
{
    public void OnStartClick()
    {
        MainController.Instance.StartLevel();
    }

    public void OnExitClick()
    {
        
    }
}

