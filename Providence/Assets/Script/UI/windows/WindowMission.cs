using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class WindowMission : BaseWindow
{
    public Text moneyField;
    public Text crystalField;
    private int currentSelectedRespawnPoint;
    private int currentSelectedMission;
    public List<RespawnPointToggle> MissionsToggles;
    public Transform Layout;
    private List<RespawnPointToggle> RespawnToggles;
    public RespawnPointToggle PrefabRespawnPointToggle;

    public override void Init()
    {
        base.Init();
        var items = MainController.Instance.PlayerData.playerInv;
        foreach (var item in items)
        {
            Text t = null;
            switch (item.Key)
            {
                case ItemId.money:
                    t = moneyField;
                    break;
                case ItemId.crystal:
                    t = crystalField;
                    break;
            }
            if (t != null)
                t.text = item.Value.ToString();
        }

        foreach (var t in MissionsToggles)
        {
            t.Toggle.onValueChanged.RemoveAllListeners();
            t.Toggle.onValueChanged.AddListener(arg0 =>
            {
                if (arg0)
                {
                    MissionSelected(t.ID);
                }
            });
            if (t.ID == 1)
            {
                t.Toggle.isOn = true;
            }
        }
        MissionSelected(1);
    }

    private void MissionSelected(int mission)
    {
        var count = DataBaseController.Instance.DataStructs.GetRespawnPointsCountByMission(mission);
        List<int> opensRespawnPoints = MainController.Instance.PlayerData.GetAllBornPositions(mission);
        RespawnToggles.Clear();
        Utils.ClearTransform(Layout);
        for (int i = 1; i < count; i++)
        {
            var rpToggle = DataBaseController.Instance.GetItem<RespawnPointToggle>(PrefabRespawnPointToggle, Vector3.zero);
            RespawnToggles.Add(rpToggle);
            rpToggle.ID = i;
        }

        currentSelectedRespawnPoint = mission;
        foreach (var respawnToggle in RespawnToggles)
        {
            respawnToggle.Toggle.onValueChanged.RemoveAllListeners();
            respawnToggle.Toggle.onValueChanged.AddListener(arg0 =>
            {
                if (arg0)
                {
                    currentSelectedRespawnPoint = respawnToggle.ID;
                }
            });
            if (respawnToggle.ID == 1)
            {
                respawnToggle.Toggle.isOn = true;
            }
        }

    }

    public void OnPlayClick()
    {
        int GetCurrentBornPosIndex = currentSelectedRespawnPoint;
        MainController.Instance.StartLevel(GetCurrentBornPosIndex);
    }

}

