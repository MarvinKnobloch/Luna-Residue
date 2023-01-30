using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Commitfasttravel : MonoBehaviour
{
    public Vector3 fasttravelpoint;
    [SerializeField] private GameObject loadcharmananger;
    [SerializeField] private GameObject enemyhealthbars;

    private SpielerSteu steuerung;
    private void Awake()
    {
        steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        steuerung.Enable();
    }
    private void Update()
    {
        if (steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            closecommit();
        }
    }
    public void fasttravel()
    {
        LoadCharmanager.disableattackbuttons = false;
        LoadCharmanager.savemainposi = fasttravelpoint;
        LoadCharmanager.Overallmainchar.gameObject.GetComponent<Movescript>().state = Movescript.State.Air;
        foreach(Transform enemys in enemyhealthbars.transform)
        {
            enemys.GetComponent<Enemyhealthbar>().removehealthbar();
        }

        gameObject.SetActive(false);
        loadcharmananger.GetComponent<LoadCharmanager>().maingamevalues();
    }
    public void closecommit()
    {
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
