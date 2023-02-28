using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemysizetutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private Areacontroller areacontroller;
    private int textindex;

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
        if (readinputs == true && controlls.Player.Interaction.WasPressedThisFrame())
        {
            if (textindex != 5)
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
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Next to the enemyhealthbar, there is a letter, which shows the type of the enemy. " +
                                                                   "<color=green>" + "S(small)" + "</color>" + ", " + "<color=green>" + "M(medium) " + "</color>" + "or " + "<color=green>" + "B(big) " + "</color>";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "Depeding on these types, your " + "<color=green>" + "down" + "</color>" + ", " + "<color=green>" + "mid " + "</color>" +
                                                                         "or " + "<color=green>" + "up " + "</color>" + "attack at the end of your " + "<color=green>" + "attackchain" + "</color>" +
                                                                         ", will result in different damage.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "For example: Hitting a " + "<color=green> " + "big " + "</color>" + "enemy with an " + "<color=green>" + "down " + "</color>" +
                                                                        "attack will deal " + "<color=green>" + "100% " + "</color>" + "damage.";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "Hitting the weakspot with a " + "<color=green>" + "mid " + "</color>" + "attack, will deal " + "<color=green>" + "85% " + "</color>" + "damage. " +
                                                                        "However, this will increase the damage dealt with a " + "<color=green>" + "up " + "</color>" + "attack to " + "<color=green>" + "150% " + "</color>" +
                                                                        "instead of " + "<color=green>" + "50% " + "</color>" + ".";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "The bluebar beneath the enemyhealthbar, shows how long the weakspot is exposed. " +
                                                                        "After that the yellowbar will show, when the weakspot can be triggered again.";
        else if (textindex == 5) tutorialcontroller.tutorialtext.text = "Also your " + "<color=green>" + "critchance " + "</color>" + "against this enemy is increased, meanwhile the weakspot is triggered and aslong as " +
                                                                        "the weakpointtrigger refreshs.";

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
