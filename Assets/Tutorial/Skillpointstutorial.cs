using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillpointstutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private Enemysizetutorial enemysizetutorial;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && tutorialcomplete == false)
        {
            Statics.enemyspecialcd = enemysizetutorial.savetutroialenemyspezialcd;
            tutorialcomplete = true;
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "Each levelup you gain 1 skill point. To use these skill points, open the menu(" + "<color=green>" + "ESC" + "</color>" + ") and click on skill tree.";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "Each Character got his own skill tree and there is no limitation on switching these points.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "\"Health\" increases the hit points and healing done by this character.";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "\"Defense\" reduces the damage taken. " + Statics.defenseconvertedtoattack + "% of the characters defense will be converted to attack damage.";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "\"Critical chance\" and \"Critical damage\" increases the amount of critical strikes and their damage.";
        else if (textindex == 5) tutorialcontroller.tutorialtext.text = "\"Weaponswitch\" increases the damage buff and buff duration after switching a weapon.";
        else if (textindex == 6) tutorialcontroller.tutorialtext.text = "\"Charswitch\" increases the damage buff and buff duration after switching the character.";
        else if (textindex == 7) tutorialcontroller.tutorialtext.text = "\"Basic\" increases the damage done by the last attack of your chain attack and the critical chance granted from hitting the enemy weak spot.";
        else if (textindex == 8) tutorialcontroller.tutorialtext.text = "For more turotials click on \"<color=green>Tutorial</color>\" in the menu overview.";

    }
    private void endtutorial()
    {
        readinputs = false;
        tutorialcontroller.endtutorial();
        gameObject.SetActive(false);
    }
}
