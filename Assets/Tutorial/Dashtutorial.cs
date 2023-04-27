using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dashtutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private bool tutorialcomplete;
    private int textindex;
    private string dash;

    private bool readinputs;
    private void Start()
    {
        tutorialcontroller = GetComponentInParent<Tutorialcontroller>();
        tutorialcomplete = false;
        controlls = Keybindinputmanager.inputActions;
        readinputs = false;
    }
    private void Update()
    {
        if (readinputs == true && controlls.Menusteuerung.Leftclick.WasPressedThisFrame())
        {
            if (textindex != 2)
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
        if (other.gameObject == LoadCharmanager.Overallmainchar && tutorialcomplete == false)
        {
            tutorialcomplete = true;
            dash = controlls.Player.Dash.GetBindingDisplayString();
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Press \"" + "<color=green>" + dash + "</color>" + "\" to perform an dash.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "This can be usefull to dodge attacks and cross small gaps";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "It also makes the character immune to damage for a small duration";
    }
    private void endtutorial()
    {
        readinputs = false;
        tutorialcontroller.endtutorial();
        gameObject.SetActive(false);
    }
}
