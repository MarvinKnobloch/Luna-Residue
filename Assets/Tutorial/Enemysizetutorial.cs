using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    [NonSerialized] public float savetutroialenemyspezialcd;

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
        if (tutorialstarted == true)
        {
            checkgate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && tutorialcomplete == false)
        {
            savetutroialenemyspezialcd = Statics.enemyspecialcd;
            Statics.enemyspecialcd = 7;
            tutorialcomplete = true;
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "On the left of the enemy health bar there is a letter and number.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "The number displays the enemy level and the letter shows the type of size. " +
                                                                        "<color=green>S</color>(<color=green>small</color>), <color=green>M</color>(<color=green>medium</color>" +
                                                                        ") or <color=green>B</color>(<color=green>Big</color>).";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "Depending on these types, your " + "<color=green>" + "down" + "</color>" + ", " + "<color=green>" + "mid " + "</color>" +
                                                                         "or " + "<color=green>" + "up " + "</color>" + "attack at the end of your " + "<color=green>" + "attackchain" + "</color>" +
                                                                         " will result in different damage.";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "For example: Hitting a " + "<color=green>" + "big " + "</color>" + "enemy with an " + "<color=green>" + "down " + "</color>" +
                                                                        "attack will deal " + "<color=green>" + "100% " + "</color>" + "damage.";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "Hitting the weak spot with an " + "<color=green>" + "mid " + "</color>" + "attack, will deal " + "<color=green>" + "85% " + "</color>" + "damage. " +
                                                                        "However, this will increase the damage dealt with an " + "<color=green>" + "up " + "</color>" + "attack to " + "<color=green>" + "150% " + "</color>" +
                                                                        "instead of " + "<color=green>" + "50% " + "</color>" + ".";
        else if (textindex == 5) tutorialcontroller.tutorialtext.text = "A blue bar beneath the enemy health bar will show how long the weak spot is exposed.";
        else if (textindex == 6) tutorialcontroller.tutorialtext.text = "The yellow bar will display whether the weak spot can be triggered again.";
        else if (textindex == 7) tutorialcontroller.tutorialtext.text = "Also your " + "<color=green>" + "criticalchance " + "</color>" + "against this enemy is increased while the weak spot is exposed and as long as " +
                                                                        "the weak point trigger refreshes.";
        else if (textindex == 8) tutorialcontroller.tutorialtext.text = "Try to trigger the weakspot of the dummy to continue the tutorial.";
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
