using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum UnderUi
{
    none,
    delete,
    equip
}

public class PlayerItemElement : MonoBehaviour ,IPointerClickHandler,IDragHandler ,IPointerDownHandler , IPointerUpHandler, IEndDragHandler
{
    public Image rareImage;
    public Image iconImage;
    public Image equpedImage;
    public PlayerItem PlayerItem;
    private Transform oldTransforml;
    private Action<PlayerItemElement> OnClicked;
    private Func<Vector2, UnderUi> IsOnWhat;
    private float startTakeTime = 0;
    private bool isDrag = false;
        
    public void Init(PlayerItem item,Action<PlayerItemElement> OnClicked, Func<Vector2, UnderUi> IsOnWhat)
    {
        PlayerItem = item;
        this.IsOnWhat = IsOnWhat;
        this.OnClicked = OnClicked;
        rareImage.gameObject.SetActive(item.isRare);
        equpedImage.gameObject.SetActive(item.IsEquped);
        iconImage.sprite = Resources.Load<Sprite>(item.icon);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //OnClicked(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startTakeTime = Time.time;
        if (!isDrag)
        {
            StartCoroutine(Wait());
            OnClicked(this);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        oldTransforml = transform.parent;
        transform.SetParent(transform.parent.parent.parent);
        isDrag = true;
    } 

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
        var deltaTime = Time.time - startTakeTime; 
        var res = IsOnWhat(eventData.position);
        transform.SetParent(oldTransforml);
        switch (res)
        {
            case UnderUi.delete:
                MainController.Instance.PlayerData.Sell(PlayerItem);
                break;
            case UnderUi.equip:
                MainController.Instance.PlayerData.EquipItem(PlayerItem);
                break;
        }
    }

    public void Equip(bool val)
    {
        equpedImage.gameObject.SetActive(val);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag)
            transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
    }
}

