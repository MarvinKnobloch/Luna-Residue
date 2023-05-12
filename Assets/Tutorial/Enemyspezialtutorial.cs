using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspezialtutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private bool tutorialcomplete;
    private int textindex;

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
        if (readinputs == true && controlls.Menusteuerung.F1.WasPressedThisFrame())
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
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Each enemy has a different spezialattack.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "Avoid the dummies spezialattack to continue the tutorial.";
    }
    private void endtutorial()
    {
        readinputs = false;
        tutorialcontroller.endtutorial();
        gameObject.SetActive(false);
    }
}
