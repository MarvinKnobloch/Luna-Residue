using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Npcshop : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject npcshopui;
    [SerializeField] private CinemachineFreeLook cam;
    [SerializeField] private List<Itemcontroller> shopitems = new List<Itemcontroller>();
    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
#if !UNITY_EDITOR
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
#endif
        cam.gameObject.SetActive(false);
        Time.timeScale = 0f;
        npcshopui.GetComponent<Npcshopcontroller>().npcshopitems.Clear();
        for (int i = 0; i < shopitems.Count; i++)
        {
            npcshopui.GetComponent<Npcshopcontroller>().npcshopitems.Add(shopitems[i]);
        }
        npcshopui.SetActive(true);
        
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            npcshopui.GetComponent<Npcshopcontroller>().removeitemswhenclose();
            npcshopui.SetActive(false);

            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
            LoadCharmanager.disableattackbuttons = false;
            LoadCharmanager.interaction = false;
            cam.gameObject.SetActive(true);
            Time.timeScale = Statics.normalgamespeed;
            enabled = false;
        }
    }
}
