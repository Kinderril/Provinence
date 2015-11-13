﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    private Camera MainCamera;
    private int layerMask = 1;
    private Hero mainHero;
    private Vector2 startDrag;
    public SubUIMain subUI;
    private bool enable;
    public Vector3 keybordDir;
    private bool isPressed;
    private Vector3 startClick;
    private bool isOver;
    public void Init()
    {
        mainHero = MainController.Instance.level.MainHero;
        MainCamera = MainController.Instance.MainCamera;
        if (subUI != null)
        {
            subUI.Init(this);
        }
        else
        {
            Debug.LogWarning("no sub UI");
        }
    }

    void LateUpdate()
    {
        //        Debug.Log(">>>> " + Input.touchCount + "   "  + Input.touches.Length + "   " + Input.GetMouseButton(0));
        if (Input.GetMouseButtonDown(0))
        {
            startClick = Input.mousePosition;
            if (!isPressed)
            {
                isOver = EventSystem.current.IsPointerOverGameObject();
                if (!isOver)
                {
                    isPressed = true;
                }
            }
        }

        if (isPressed)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isOver = EventSystem.current.IsPointerOverGameObject();
                var dir = Input.mousePosition - startClick;
                isPressed = false;
                var sqrDist = dir.sqrMagnitude;
                if (sqrDist > 4200)
                {
                    if (enable)
                        mainHero.TryAttackByDirection(new Vector3(dir.x, 0, dir.y));
                }
            }
        }
    }

    public void OnChangeWeapon()
    {
        mainHero.SwitchWeapon();
    }

    public void OnCrouch()
    {
        if (enable)
            mainHero.DoCrouch();
    }

    void Update()
    {
#if UNITY_EDITOR
        var w = Input.GetKey(KeyCode.W);
        var s = Input.GetKey(KeyCode.S);
        var d = Input.GetKey(KeyCode.D);
        var a = Input.GetKey(KeyCode.A);
        int x = 0;
        int y = 0;
        if (w)
        {
            x = 1;
        }
        else if (s)
        {
            x = -1;
        }

        if (d)
        {
            y = 1;
        }
        else if (a)
        {
            y = -1;
        }
        keybordDir = new Vector3(y, 0, x);
        mainHero.MoveToDirection(keybordDir);
#endif
    }

    private Vector3 RayCast(PointerEventData eventData)
    {
        RaycastHit hit;
        Ray ray = MainCamera.ScreenPointToRay(eventData.position);//Input.mousePosition);
#if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * 11, Color.yellow, 1);
#endif
        if (Physics.Raycast(ray, out hit, 9999999, layerMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    //    public void OnPointerDown(PointerEventData eventData)
    //    {
    //        startDrag = eventData.position;
    //    }
    //
    //    public void OnPointerUp(PointerEventData eventData)
    //    {
    //        var endDrag = eventData.position;
    //        var dir = (endDrag - startDrag);
    //        var sqrDist = dir.sqrMagnitude;
    //
    //        if (sqrDist >4200)
    //        {
    //            if (enable)
    //                mainHero.TryAttackByDirection(new Vector3(dir.x,0,dir.y));
    //        }
    //    }

    public void UpdateMoveArrow(Vector3 dir)
    {
        if (enable)
            mainHero.MoveToDirection(dir);
    }

    public void Enable(bool val)
    {
        enable = val;
    }
}

