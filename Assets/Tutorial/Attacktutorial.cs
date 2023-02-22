using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Attacktutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private CinemachineFreeLook Cam1;
    [SerializeField] private CinemachineVirtualCamera Cam2;
    [SerializeField] private Firstarea firstarea;
    private Tutorialcontroller tutorialcontroller;
    private int textindex;
    private string attack1action;
    private string attack2action;
    private string attack3action;

    private void Awake()
    {
        tutorialcontroller = GetComponentInParent<Tutorialcontroller>();
        controlls = Keybindinputmanager.inputActions;
    }
    private void Update()
    {
        if (controlls.Player.Interaction.WasPressedThisFrame())
        {
            if (textindex != 4)
            {
                tutorialcontroller.tutorialtext.text = string.Empty;
                textindex++;
                showtext();
            }
            else
            {
                endtutorial();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar && firstarea.attacktutorialcomplete == false)
        {
            Cam1.gameObject.SetActive(false);
            if(Cam2.gameObject.activeSelf == true)
            {
                LoadCharmanager.Overallmainchar.GetComponent<Movescript>().disableaim();
                Cam2.gameObject.SetActive(false);
            }
            attack1action = controlls.Player.Attack1.GetBindingDisplayString();
            attack2action = controlls.Player.Attack2.GetBindingDisplayString();
            attack3action = controlls.Player.Attack3.GetBindingDisplayString();
            Time.timeScale = 0;
            LoadCharmanager.disableattackbuttons = true;
            LoadCharmanager.interaction = true;
            tutorialcontroller.tutorialtext.text = string.Empty;
            textindex = 0;
            tutorialcontroller.tutorialtextbox.SetActive(true);
            showtext();
        }
    }
    private void showtext()
    {
        if(textindex == 0) tutorialcontroller.tutorialtext.text = "Press \"" + "<color=green>" + attack1action + "</color>" + "\" to perform an attack";
        else if(textindex == 1) tutorialcontroller.tutorialtext.text = "Meanwhile the first attack there is a small window to press \"" + "<color=green>" + attack2action + "</color>" + "\" to continue your attackchain";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "Meanwhile the second attack you can choose to perform a downattack \"" + "<color=green>" + attack1action + "</color>" + "\", a midattack \""
                                               + "<color=green>" + attack2action + "</color>" + "\" or a upattack \"" + "<color=green>" + attack3action + "</color>" + "\"";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "Its possible to perfrom this attackchain 2 times before you have to reset";
        else if (textindex == 4 ) tutorialcontroller.tutorialtext.text = "However, you will learn to extend this attackchains later in the game";
    }
    private void endtutorial()
    {
        Cam1.gameObject.SetActive(true);
        firstarea.attacktutorialcomplete = true;
        Time.timeScale = Statics.normalgamespeed;
        LoadCharmanager.disableattackbuttons = false;
        LoadCharmanager.interaction = false;
        tutorialcontroller.tutorialtextbox.SetActive(false);
        enabled = false;
    }

}
