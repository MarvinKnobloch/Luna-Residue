using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menucontroller : MonoBehaviour
{
    private SpielerSteu controlls;

    public static bool inoverview;
    public GameObject fasttravelmenu;
    public TextMeshProUGUI elementalelmenutext;

    public bool somethinginmenuisopen;
    [SerializeField] private GameObject[] closegameobjectswithesc;

    public Color selectedcolor;
    public Color notselectedcolor;

    [SerializeField] private GameObject uimessage;
    public Menusoundcontroller menusoundcontroller;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        uimessage.SetActive(false);
        inoverview = true;

        somethinginmenuisopen = false;
        foreach (GameObject obj in closegameobjectswithesc)
        {
            obj.SetActive(false);
        }

        fasttravelmenu.SetActive(false);                                                         //weil das Ui beim fasttravel commit nicht mehr geschlossen wird
        if (Statics.elementalmenuisactiv == false) elementalelmenutext.color = Color.gray;
        else elementalelmenutext.color = Color.white;
    }

    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame() && somethinginmenuisopen == true)
        {
            somethinginmenuisopen = false;
            foreach (GameObject obj in closegameobjectswithesc)
            {
                obj.SetActive(false);
            }
            menusoundcontroller.playmenubuttonsound();
        }
    }
    private void OnDisable()
    {
        inoverview = false;
    }
    public void cantopenuimessage(string message)
    {
        if (uimessage.gameObject.activeSelf == true)
        {
            uimessage.GetComponent<Menuuimessage>().resettimer();
            uimessage.GetComponentInChildren<TextMeshProUGUI>().text = message;
        }
        else
        {
            uimessage.GetComponentInChildren<TextMeshProUGUI>().text = message;
            uimessage.SetActive(true);
        }

    }
    public void closeallselections()
    {
        foreach (GameObject obj in closegameobjectswithesc)
        {
            obj.SetActive(false);
        }
    }
}
