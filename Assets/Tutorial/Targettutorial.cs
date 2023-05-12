using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Targettutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private Areacontroller areacontroller;
    private int textindex;

    private string targetswitch;
    private string grouptarget;
    private string support1target;
    private string support2target;

    private int tutorialnumber;

    private bool readinputs;
    private void Start()
    {
        tutorialcontroller = GetComponentInParent<Tutorialcontroller>();
        controlls = Keybindinputmanager.inputActions;
        readinputs = false;
        areacontroller = tutorialcontroller.areacontroller;
        tutorialnumber = GetComponent<Areanumber>().areanumber;
        if (areacontroller.tutorialcomplete[tutorialnumber] == true)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (readinputs == true && controlls.Menusteuerung.F1.WasPressedThisFrame())
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
        if (other.gameObject == LoadCharmanager.Overallmainchar && areacontroller.tutorialcomplete[tutorialnumber] == false)
        {
            targetswitch = controlls.Player.Lockonchange.GetBindingDisplayString();
            grouptarget = controlls.Player.Setalliestarget.GetBindingDisplayString();
            support1target = controlls.Player.Character3target.GetBindingDisplayString();
            support2target = controlls.Player.Character4target.GetBindingDisplayString();
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "The icon, on the right side of the enemy health bar shows the current target of the enemy.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "The enemy type and level display, of the players current traget, is red instead of white.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "Use \"" + "<color=green>" + targetswitch + "</color>" + "\" to switch your target." ;
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "Pressing \"" + "<color=green>" + grouptarget + "</color>" + "\" will force your allies, to attack your current target.";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "Use \"" + "<color=green>" + support1target + "</color>" + "\" and \"" + "<color=green>" + support2target + "</color>" + "\"" +
                                                                        " to set the target of your allies separately.";
    }
    private void endtutorial()
    {
        readinputs = false;
        tutorialcontroller.endtutorial();
        areacontroller.tutorialcomplete[tutorialnumber] = true;
        areacontroller.autosave();
        gameObject.SetActive(false);
    }
}
