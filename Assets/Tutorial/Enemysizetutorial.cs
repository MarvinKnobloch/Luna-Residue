using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemysizetutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private bool tutorialcomplete;
    private int textindex;

    private bool tutorialstarted;
    [SerializeField] private GameObject gate;
    private Vector3 gatestartposi;
    private Vector3 gateendposi;
    private float movetime = 6f;
    private float movetimer;

    private bool readinputs;

    [SerializeField] private EnemyHP enemyHP;
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
            if (textindex != 6)
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
        if (other.gameObject == LoadCharmanager.Overallmainchar && tutorialcomplete == false)
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
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "Hitting the weakspot with an " + "<color=green>" + "mid " + "</color>" + "attack, will deal " + "<color=green>" + "85% " + "</color>" + "damage. " +
                                                                        "However, this will increase the damage dealt with an " + "<color=green>" + "up " + "</color>" + "attack to " + "<color=green>" + "150% " + "</color>" +
                                                                        "instead of " + "<color=green>" + "50% " + "</color>" + ".";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "The bluebar beneath the enemyhealthbar, shows how long the weakspot is exposed. " +
                                                                        "After that the yellowbar will show, when the weakspot can be triggered again.";
        else if (textindex == 5) tutorialcontroller.tutorialtext.text = "Also your " + "<color=green>" + "critchance " + "</color>" + "against this enemy is increased, meanwhile the weakspot is triggered and aslong as " +
                                                                        "the weakpointtrigger refreshs.";
        else if (textindex == 6) tutorialcontroller.tutorialtext.text = "Try to trigger the weakspot of the dummy to continue the tutorial";

    }
    private void endtutorial()
    {
        tutorialstarted = true;
        readinputs = false;
        tutorialcontroller.endtutorial();
    }
    private void checkgate()
    {
        if (enemyHP.enemyincreasebasicdmg == true)
        {
            tutorialstarted = false;
            StartCoroutine(killenemy());
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
    IEnumerator killenemy()
    {
        yield return new WaitForSeconds(2);
        enemyHP.takesupportdmg(11000);
    }
}
