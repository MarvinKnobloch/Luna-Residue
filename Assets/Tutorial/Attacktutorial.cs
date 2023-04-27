using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Attacktutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private bool tutorialcomplete;
    private int textindex;
    private string attack1action;
    private string attack2action;
    private string attack3action;

    private bool tutorialstarted;
    [SerializeField] private GameObject gate;
    private Vector3 gatestartposi;
    private Vector3 gateendposi;
    private float movetime = 4f;
    private float movetimer;

    private bool readinputs;
    private void Start()
    {
        tutorialcomplete = false;
        tutorialstarted = false;
        gatestartposi = gate.transform.position;
        gateendposi = gatestartposi + new Vector3(0, -10, 0);
        tutorialcontroller = GetComponentInParent<Tutorialcontroller>();
        controlls = Keybindinputmanager.inputActions;
        readinputs = false;
    }
    private void Update()
    {
        if (readinputs == true && controlls.Menusteuerung.Leftclick.WasPressedThisFrame())
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
        if (tutorialstarted == true)
        {
            checkgate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar && tutorialcomplete == false)
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
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Press \"" + "<color=green>" + attack1action + "</color>" + "\" to attack.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "Meanwhile this attack there is a small window to press \"" + "<color=green>" + attack2action + "</color>" + "\" to continue your attackchain.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "While performing your second attack press \"" + "<color=green>" + attack1action + "</color>" + "\" (downattack), \""
                                                                      + "<color=green>" + attack2action + "</color>" + "\" (midattack) or \"" + "<color=green>"
                                                                      + attack3action + "</color>" + "\" (upattack) to finish the chainattack.";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "Its possible to perform this attackchain 2 times before you have to reset.";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "Complete a attackchain to continue the tutorial";
    }
    private void endtutorial()
    {
        tutorialstarted = true;
        readinputs = false;
        tutorialcontroller.endtutorial();
    }
    private void checkgate()
    {
        if (LoadCharmanager.Overallmainchar.GetComponent<Movescript>().attackcombochain > 0)
        {
            tutorialstarted = false;
            StartCoroutine(opengate());
        }
    }
    IEnumerator opengate()
    {
        while (true)
        {
            movetimer += Time.deltaTime;
            float gateopenpercantage = movetimer / movetime;
            gate.transform.position = Vector3.Lerp(gatestartposi, gateendposi, gateopenpercantage);

            if (movetimer >= movetime)
            {
                movetimer = 0;
                StopCoroutine("opengate");
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}