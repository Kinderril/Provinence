using UnityEngine;
using System.Collections;

public class MainController : Singleton<MainController>
{
    public Camera MainCamera;
    public Unit MainHero;
    public UIMain uiMain;
    public TimerManager TimerManager;
    public Level level;
    public InGameUI InGameUi;

	// Use this for initialization
	void Start () {
        TimerManager = new TimerManager();
        level = new Level();
        level.Init();
        InGameUi.Init();
        Map.Instance.Init();
        MainHero.Init();
	    uiMain.Init();
	}
	
	// Update is called once per frame
	void Update () {
        if (TimerManager != null)
	        TimerManager.Update();
	    if (level != null)
	        level.Update();
	}

}
