using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class Dashtutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private bool tutorialcomplete;
    private int textindex;
    private string dash;

    private bool readinputs;

    [SerializeField] private VideoClip videoclip;
    [SerializeField] private Videocontroller videocontroller;
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
            videocontroller.gameObject.SetActive(true);
            videocontroller.setnewvideo(videoclip);
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Press \"" + "<color=green>" + dash + "</color>" + "\" to perform a dash.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "This can be useful to dodge attacks and cross small gaps.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "The stamina bar is displayed in the bottom left.";
    }
    private void endtutorial()
    {
        videocontroller.gameObject.SetActive(false);
        readinputs = false;
        tutorialcontroller.endtutorial();
        gameObject.SetActive(false);
    }
}
