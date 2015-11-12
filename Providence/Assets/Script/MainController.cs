﻿using UnityEngine;
using System.Collections;

public enum MainState
{
    start,
    play,
    mission,
    parameters,
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
    //public WindowInGame WindowInGame;st 
    public MainState MainState;
    public PlayerData PlayerData;
    
	void Start () {
        WindowManager.Instance.Init();
        ShopController.Instance.Init();
        TimerManager = new TimerManager();
        WindowManager.Instance.OpenWindow(MainState.start);
        PlayerData = new PlayerData();
        PlayerData.Load();
	}

    public void StartLevel()
    {
        level = new Level();
        WindowManager.Instance.OpenWindow(MainState.play);
    }
    private IEnumerator w4death()
    {
        yield return new WaitForSeconds(1);
        WindowManager.Instance.OpenWindow(MainState.end);
    }

    public void EndLevel()
    {
        Debug.Log("EndLevel>>");
        level.EndLevel(PlayerData);
        Map.Instance.EndLevel();
        StartCoroutine(w4death());
    }
    
    // Update is called once per frame
	void Update () {
        if (TimerManager != null)
	        TimerManager.Update();
	    if (level != null)
	        level.Update();
	}

}
