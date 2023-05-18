using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Setvsync : MonoBehaviour
{
    [SerializeField] private GameObject selection;
    [SerializeField] private TextMeshProUGUI vsynctext;

    [SerializeField] private Menusoundcontroller menusoundcontroller;
    private void OnEnable()
    {
        if(PlayerPrefs.GetInt("Vsyncvalue") == 0)
        {
            vsynctext.text = "V-Sync off";
        }
        else
        {
            vsynctext.text = "V-Sync on";
        }
        selection.SetActive(false);
    }
    public void openselection()
    {
        if(selection.activeSelf == false)
        {
            selection.SetActive(true);
        }
        else
        {
            selection.SetActive(false);
        }
    }
    public void vsyncon()
    {
        PlayerPrefs.SetInt("Vsyncvalue", 1);
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("Vsyncvalue");
        vsynctext.text = "V-Sync on";
        selection.SetActive(false);
        menusoundcontroller.playmenubuttonsound();
    }
    public void vsyncoff()
    {
        PlayerPrefs.SetInt("Vsyncvalue", 0);
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("Vsyncvalue");
        vsynctext.text = "V-Sync off";
        selection.SetActive(false);
        menusoundcontroller.playmenubuttonsound();
    }
}
