﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ProfileControllerInspector : EditorWindow
{
    [MenuItem("Window/ProfileController")]
    public void OnGUI()
    {
        if (!Application.isPlaying)
        {
            if (GUILayout.Button("ADD 1 point"))
            {
               var  AllocatedPoints = PlayerPrefs.GetInt(PlayerData.ALLOCATED, 0);
                AllocatedPoints++;
                PlayerPrefs.SetInt(PlayerData.ALLOCATED, AllocatedPoints);
            }
            if (GUILayout.Button("GIVE 1000 money"))
            {
                var str = PlayerData.INVENTORY + ItemId.money.ToString();
                var cur = PlayerPrefs.GetInt(str, 0);
                cur += 1000;
                PlayerPrefs.SetInt(str, cur);
            }
            if (GUILayout.Button("CLEAR ALL"))
            {
                PlayerPrefs.DeleteAll();
            }
            EditorGUILayout.LabelField(">>>");
            Repaint();
        }
    }
}

