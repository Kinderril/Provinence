using UnityEngine;
using System.Collections;

public class MainController : Singleton<MainController>
{
    public Camera MainCamera;
    public Unit MainHero;
    public UIMain uiMain;
    public TimerManager TimerManager;

	// Use this for initialization
	void Start () {
        TimerManager = new TimerManager();
        Map.Instance.Init();
        MainHero.Init();
	    uiMain.Init();
	}
	
	// Update is called once per frame
	void Update () {
        if (TimerManager != null)
	        TimerManager.Update();
	}
}
