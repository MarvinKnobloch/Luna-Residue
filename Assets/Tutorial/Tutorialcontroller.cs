using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Tutorialcontroller : MonoBehaviour
{
    public GameObject tutorialtextbox;
    public TextMeshProUGUI tutorialtext;
    public CinemachineFreeLook Cam1;
    public CinemachineVirtualCamera Cam2;
    public Areacontroller areacontroller;

    public void onenter()
    {
        Cam1.m_YAxis.m_MaxSpeed = 0;
        Cam1.m_XAxis.m_MaxSpeed = 0;
        Cam2.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
        Cam2.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
        if (Cam2.gameObject.activeSelf == true)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().disableaimcam();
        }
        Time.timeScale = 0;
        LoadCharmanager.disableattackbuttons = true;
        LoadCharmanager.interaction = true;
        tutorialtext.text = string.Empty;
        tutorialtextbox.SetActive(true);
    }
    public void endtutorial()
    {
        Cam1.m_YAxis.m_MaxSpeed = 0.008f * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        Cam1.m_XAxis.m_MaxSpeed = 0.6f * PlayerPrefs.GetFloat("mousesensitivity") / 50;
        Cam2.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0.2f * PlayerPrefs.GetFloat("rangeweaponaimsensitivity") / 50;
        Cam2.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0.2f * PlayerPrefs.GetFloat("rangeweaponaimsensitivity") / 50;
        Time.timeScale = Statics.normalgamespeed;
        LoadCharmanager.disableattackbuttons = false;
        LoadCharmanager.interaction = false;
        tutorialtextbox.SetActive(false);
    }
}
