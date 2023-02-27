using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Attacktutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Firstarea firstarea;
    private Tutorialcontroller tutorialcontroller;
    private int textindex;
    private string attack1action;
    private string attack2action;
    private string attack3action;

    private bool readinputs;
    private void Start()
    {
        tutorialcontroller = GetComponentInParent<Tutorialcontroller>();
        controlls = Keybindinputmanager.inputActions;
        readinputs = false;
        firstarea = tutorialcontroller.firstarea;
        if (firstarea.attacktutorialcomplete == true)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (readinputs == true && controlls.Player.Interaction.WasPressedThisFrame())
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
            attack1action = controlls.Player.Attack1.GetBindingDisplayString();
            attack2action = controlls.Player.Attack2.GetBindingDisplayString();
            attack3action = controlls.Player.Attack3.GetBindingDisplayString();
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if(textindex == 0) tutorialcontroller.tutorialtext.text = "Press \"" + "<color=green>" + attack1action + "</color>" + "\" to perform an attack.";
        else if(textindex == 1) tutorialcontroller.tutorialtext.text = "Meanwhile the first attack there is a small window to press \"" + "<color=green>" + attack2action + "</color>" + "\" to continue your attackchain.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "Meanwhile the second attack you can choose to perform a downattack \"" + "<color=green>" + attack1action + "</color>" + "\", a midattack \""
                                               + "<color=green>" + attack2action + "</color>" + "\" or a upattack \"" + "<color=green>" + attack3action + "</color>" + "\".";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "Its possible to perfrom this attackchain 2 times before you have to reset.";
        else if (textindex == 4 ) tutorialcontroller.tutorialtext.text = "However, you will be able to extend this attackchains later in the game.";
    }
    private void endtutorial()
    {
        readinputs = false;
        tutorialcontroller.endtutorial();
        firstarea.attacktutorialcomplete = true;
        firstarea.autosave();
        gameObject.SetActive(false);
    }
}