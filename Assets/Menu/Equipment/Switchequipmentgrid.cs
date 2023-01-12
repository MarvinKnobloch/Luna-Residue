using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Switchequipmentgrid : MonoBehaviour, ISelectHandler
{
    [SerializeField] private GameObject grid;
    [SerializeField] Equipmentmenucontroller equipmentcontroller;

    public void OnSelect(BaseEventData eventData)
    {
        equipmentcontroller.switchgrid(grid);      
        Equipcharselection.currentbuttonslot= this.gameObject;
    }   
}

