using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Equipmentmenucontroller : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject equipmentmenu;
    [SerializeField] private GameObject[] itemgrids;
    [SerializeField] private GameObject swordgrid;
    [SerializeField] private GameObject swordbutton;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        Statics.currentequipmentbutton = 0;
        foreach (GameObject grids in itemgrids)
        {
            grids.SetActive(false);
        }
        swordgrid.SetActive(true);
        EventSystem.current.SetSelectedGameObject(swordbutton);
        swordbutton.gameObject.GetComponent<Setcolorcurrentweapon>().triggerbutton();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            overview.SetActive(true);
            equipmentmenu.SetActive(false);
        }
    }
    public void switchgrid(GameObject selectedgrid)
    {
        foreach (GameObject grids in itemgrids)
        {
            grids.SetActive(false);
        }
        selectedgrid.SetActive(true);
    }
}
