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
    private string supoort2target;

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
            if (textindex != 8)
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
            supoort2target = controlls.Player.Character4target.GetBindingDisplayString();
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Every level up you gain 1 skillpoint. To use these skillpoints, open the menu and click on skilltree.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "Each Character got his own skilltree and there is no limitation on switching these points.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "Health increase the hitpoints and healing done by this character.";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "Defense reduce the damage taken. " + Statics.defenseconvertedtoattack + "% off the defense will converted to attackdamage";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "Attack increases damage.";
        else if (textindex == 5) tutorialcontroller.tutorialtext.text = "Critchance and Critdmg increases the amount of critical strikes and there damage.";
        else if (textindex == 6) tutorialcontroller.tutorialtext.text = "Weaponswitch increases the damagebuff and buff duration after switching a weapon.";
        else if (textindex == 7) tutorialcontroller.tutorialtext.text = "Charswitch increases the damagebuff and buff duration after switching the character.";
        else if (textindex == 8) tutorialcontroller.tutorialtext.text = "Basic increases the damage done by the last attack of the attackchain and the critchance granted by hitting the weakpoint.";

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
