using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public  class UIMain : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private Camera MainCamera;
    private int layerMask = 1;
    private Unit mainHero;
    private Vector2 startDrag;

    public void Init()
    {
        mainHero = MainController.Instance.MainHero;
        MainCamera = MainController.Instance.MainCamera;
        Debug.Log("Init UI");
    }


    private Vector3 RayCast(PointerEventData eventData)
    {

        RaycastHit hit;
        Ray ray = MainCamera.ScreenPointToRay(eventData.position);//Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 11, Color.yellow, 1);
        //Debug.Log("ray " + ray.direction);

        if (Physics.Raycast(ray, out hit, 9999999, layerMask))
        {
            return hit.point;
            /*
            var objectHit = hit.transform.GetComponent<Unit>();
            if (objectHit != null)
            {
                if (objectHit != mainHero)
                {
                    mainHero.TryAttack(objectHit);
                }
            }
            else
            {
            }*/
        }
        return Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown " + eventData);
        startDrag = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var endDrag = eventData.position;
        var sqrDist = (endDrag - startDrag).sqrMagnitude;
        var hit = RayCast(eventData);
        Debug.Log("On up " + sqrDist);
        if (sqrDist >4200)
        {
            if (hit != Vector3.zero)
                mainHero.TryAttack(hit);
        }
        else
        {
            if (hit != Vector3.zero)
                mainHero.MoveTo(hit);
            
        }
    }
}

