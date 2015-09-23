using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;


public class WeaponChooser : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public Transform animatedElement;
    private bool isOpen = false;
    private Animator animator;
    private const string p_animator = "isOpen";
    private List<WeaponButton> weaponButons = new List<WeaponButton>(); 

    public void Init(List<Weapon> weapons)
    {
        int i = 0;
        foreach (var weapon in weapons)
        {
            weaponButons[i].Init(weapon);
            i++;
        }

        foreach (var weaponButton in weaponButons)
        {
            if (weapons.Count < i)
            {
                weaponButton.Init(weapons[i]);
            }
            else
            {
                weaponButton.Lock();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isOpen)
        {
            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isOpen)
        {
            CheckPosition(eventData.hovered);
        }
    }

    private void CheckPosition(List<GameObject> hovered)
    {
        var weaponChooser = hovered.FirstOrDefault(x => x.GetComponent<WeaponButton>() != null);
        if (weaponChooser != null)
        {
            var weap = weaponChooser.GetComponent<WeaponButton>();
            weap.Select();
        }
    }

    public void AnimationOpenEnd()
    {
        isOpen = true;
    }

    public void AnimationCloseEnd()
    {
        isOpen = false;
    }
}

