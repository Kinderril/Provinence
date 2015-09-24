using UnityEngine;
using System.Collections;

public enum MainState
{
    start,
    play,
    mission,
    pause,
    shop,
    end
}

public class MainController : Singleton<MainController>
{
    public Camera MainCamera;
    //public UIMain uiMain;
    public TimerManager TimerManager;
    public Level level;
    //public WindowInGame WindowInGame;
    public MainState MainState;
    public PlayerData PlayerData;
    

	// Use this for initialization
	void Start () {
        TimerManager = new TimerManager();
        WindowManager.Instance.OpenWindow(MainState.start);
	}

    public void StartLevel()
    {
        level = new Level();
        WindowManager.Instance.OpenWindow(MainState.play);
    }

    public void EndLevel()
    {
        level.EndLevel(PlayerData);
        WindowManager.Instance.OpenWindow(MainState.end);
    }
    
    // Update is called once per frame
	void Update () {
        if (TimerManager != null)
	        TimerManager.Update();
	    if (level != null)
	        level.Update();
	}

}
