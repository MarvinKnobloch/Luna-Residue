using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Healtutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private bool tutorialcomplete;
    private int textindex;

    private string heal;
    private string player1target;
    private string player2target;
    private string player3target;

    private bool readinputs;

    [SerializeField] private Healthuimanager healthuimanager;
    private bool tutorialstarted;
    [SerializeField] private GameObject gate;
    private Vector3 gatestartposi;
    private Vector3 gateendposi;
    private float movetime = 4f;
    private float movetimer;

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
        if(tutorialstarted == true)
        {
            checkgate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && tutorialcomplete == false)
        {
            tutorialcomplete = true;
            Statics.charswitchbool = true;
            LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().takedamageignoreiframes(20, false);
            LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().takedamageignoreiframes(20, false);
            healthuimanager.sethealthbars();
            heal = controlls.Player.Heal.GetBindingDisplayString();
            player1target = controlls.SpielerHeal.Target1.GetBindingDisplayString();
            player2target = controlls.SpielerHeal.Target2.GetBindingDisplayString();
            player3target = controlls.SpielerHeal.Target3.GetBindingDisplayString();
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Press and hold \"" + "<color=green>" + heal + "</color>" + "\" to enter the heal state.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "While holding \"" + "<color=green>" + heal + "</color>" + "\" press the buttons appearing on the screen in the right order.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "After succesfully hitting the buttons, choose the target you want to heal with \"" + "<color=green>" + player1target + "</color>" + "\", \""
                                               + "<color=green>" + player2target + "</color>" + "\" or \"" + "<color=green>" + player3target + "</color>" + "\".";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "The healing cooldown is displayed in the bottom left.";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "Heal your group to full life to continue the tutorial.";
    }
    private void endtutorial()
    {
        tutorialstarted = true;
        readinputs = false;
        tutorialcontroller.endtutorial();
    }
    private void checkgate()
    {
        if (Statics.charcurrenthealth[Statics.currentfirstchar] >= Statics.charmaxhealth[Statics.currentfirstchar] && 
            Statics.charcurrenthealth[Statics.currentthirdchar] >= Statics.charmaxhealth[Statics.currentthirdchar] && 
            Statics.charcurrenthealth[Statics.currentforthchar] >= Statics.charmaxhealth[Statics.currentforthchar])
        {
            Statics.charswitchbool = false;
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
