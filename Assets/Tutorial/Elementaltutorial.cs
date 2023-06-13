using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elementaltutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private int textindex;

    private bool readinputs;
    private void Awake()
    {
        tutorialcontroller = GetComponentInParent<Tutorialcontroller>();
        controlls = Keybindinputmanager.inputActions;
        readinputs = false;
        if (Statics.elementalmenuunlocked == true)
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
    private void OnEnable()
    {
        controlls.Enable();
        if (Statics.elementalmenuunlocked == false)
        {
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "<color=green>" + "Elemental Menu " + "</color>" + "unlocked. It now can be selected in the menu.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "The Elemental Menu enables " + "<color=green>" + "classes " + "</color>" + "and " + "<color=green>" + "spells" + "</color>" + ".";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "Each character is able to choose spells from two different elements.";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "The first element depends on your selected character. However the second one is based on a "
                                                                        + "<color=green>" + "elemental stone" + "</color>" + ".";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "Elemental stones have to be awaken first before you can use them.";
        else if (textindex == 5) tutorialcontroller.tutorialtext.text = "Awaken a stone also grants a permanent bonus for the group.";
        else if (textindex == 6) tutorialcontroller.tutorialtext.text = "These stones also define the class of the character.\n" +
                                                                        "There are 3 different classes. " + "<color=green>" + "Fight" + "</color>" + ", " + "<color=green>" + "Heal " + "</color>" +
                                                                        "and " + "<color=green>" + "Guard" + "</color>" + ".";
        else if (textindex == 7) tutorialcontroller.tutorialtext.text = "Fight: The character deals increased damage.";
        else if (textindex == 8) tutorialcontroller.tutorialtext.text = "Heal: Grants a heal bonus and add the ability to cast a group heal and resurrect (Check out <color=green>Tutorials</color> in the menu for more information).";
        else if (textindex == 9) tutorialcontroller.tutorialtext.text = "Guard: Will add some bonus health and reduce the damage taken. It also increases threat on enemies.";       
   
    }
    private void endtutorial()
    {
        Statics.elementalmenuunlocked = true;
        readinputs = false;
        tutorialcontroller.endtutorial();
        gameObject.SetActive(false);
    }
}
