using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movetutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private bool tutorialcomplete;
    private int textindex;
    private string movement;
    private string jump;

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
        if (readinputs == true && controlls.Player.Interaction.WasPressedThisFrame())
        {
            if (textindex != 1)
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
            movement = controlls.Player.Movement.GetBindingDisplayString();
            jump = controlls.Player.Jump.GetBindingDisplayString();
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Press \"" + "<color=green>" + movement + "</color>" + "\" to move your character.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "Use \"" + "<color=green>" + jump + "</color>" + "\" to jump.";
    }
    private void endtutorial()
    {
        readinputs = false;
        tutorialcontroller.endtutorial();
        gameObject.SetActive(false);
    }
}
