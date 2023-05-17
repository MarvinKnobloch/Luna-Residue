using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemyspezialtutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private bool tutorialcomplete;
    private int textindex;

    private string buttonmashhotkey;

    private bool readinputs;

    // Statics.enemyspecialcd wird um sizetutorial schon umgestellt und im skillpointtutorial wieder zurückgesetzt
    private void Start()
    {
        tutorialcontroller = GetComponentInParent<Tutorialcontroller>();
        tutorialcomplete = false;
        controlls = Keybindinputmanager.inputActions;
        readinputs = false;
    }
    private void Update()
    {
        if (readinputs == true && controlls.Menusteuerung.F1.WasPressedThisFrame())
        {
            if (textindex != 3)
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
            buttonmashhotkey = controlls.Player.Attack3.GetBindingDisplayString();
            tutorialcomplete = true;
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Each enemy has a different special attack.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "The dummy will stun you and spawn a damage area on your postion.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "Button mash \"<color=green>" + buttonmashhotkey + "</color>\" to get out of the stun and then leave the damage area.";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "Avoid the dummies spezial attack to continue the tutorial.";
    }
    private void endtutorial()
    {
        readinputs = false;
        tutorialcontroller.endtutorial();
        gameObject.SetActive(false);
    }
}
