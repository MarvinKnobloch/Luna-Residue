using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class Openworldtutorials : MonoBehaviour
{
    private SpielerSteu controlls;

    private GameObject tutorialtextbox;
    private TextMeshProUGUI tutorialtext;
    private GameObject videobackground;
    private Tutorialcontroller tutorialcontroller;
    private TextMeshProUGUI requierementtext;

    [SerializeField] private UnityEvent tutorialfunction;
    [SerializeField] private VideoClip tutorialvideo;

    private string movement;
    private string jump;

    private string dash;

    private string heal;
    private string player1target;
    private string player2target;
    private string player3target;

    private string attack1action;
    private string attack2action;
    private string attack3action;

    private string buttonmashhotkey;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void Start()
    {
        tutorialtextbox = GetComponentInParent<Gettutorialvalues>().tutorialtextbox;
        tutorialtext = GetComponentInParent<Gettutorialvalues>().tutorialtext;
        videobackground = GetComponentInParent<Gettutorialvalues>().videobackground;
        tutorialcontroller = GetComponentInParent<Gettutorialvalues>().tutorialcontroller;
        requierementtext = GetComponentInParent<Gettutorialvalues>().requierementtext;
    }
    private void OnEnable()
    {
        controlls.Enable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            tutorialtext.text = string.Empty;
            tutorialfunction.Invoke();
            tutorialcontroller.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void movetutorial()
    {
        tutorialcontroller.opentutorialbox = true;
        tutorialcontroller.openvideobackground = false;
        requierementtext.text = string.Empty;

        tutorialtextbox.SetActive(true);
        movement = controlls.Player.Movement.GetBindingDisplayString();
        jump = controlls.Player.Jump.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + movement + "</color>" + "\" to move your character.\n" +
                            "\nUse \"" + "<color=green>" + jump + "</color>" + "\" to jump.";
    }
    public void dashtutorial()
    {
        tutorialcontroller.opentutorialbox = true;
        tutorialcontroller.openvideobackground = true;
        requierementtext.text = string.Empty;

        tutorialtextbox.SetActive(true);
        videobackground.SetActive(true);
        tutorialcontroller.newvideo(tutorialvideo);

        dash = controlls.Player.Dash.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + dash + "</color>" + "\" to perform a dash. \n" +
                            "This can be useful to dodge attacks and cross small gaps. \n" +
                            "\nThe stamina bar is displayed in the bottom left.";
    }
    public void healtutorial()
    {
        tutorialcontroller.opentutorialbox = true;
        tutorialcontroller.openvideobackground = true;
        requierementtext.text = "Heal your group to full life to continue the tutorial.";

        tutorialcontroller.healtutorialstart();
        tutorialtextbox.SetActive(true);
        videobackground.SetActive(true);
        tutorialcontroller.newvideo(tutorialvideo);

        heal = controlls.Player.Heal.GetBindingDisplayString();
        player1target = controlls.SpielerHeal.Target1.GetBindingDisplayString();
        player2target = controlls.SpielerHeal.Target2.GetBindingDisplayString();
        player3target = controlls.SpielerHeal.Target3.GetBindingDisplayString();
        tutorialtext.text = "Press and hold \"" + "<color=green>" + heal + "</color>" + "\" to enter the heal state.\n" +
                    "While holding \"" + "<color=green>" + heal + "</color>" + "\" press the buttons appearing on the screen in the right order.\n" +
                    "After succesfully hitting the buttons, choose the target you want to heal with \"" + "<color=green>" + player1target + "</color>" + "\", \"" +
                    "<color=green>" + player2target + "</color>" + "\" or \"" + "<color=green>" + player3target + "</color>" + "\".\n" +
                    "\nThe healing cooldown is displayed in the bottom left.";
    }
    public void attacktutorial()
    {
        tutorialcontroller.opentutorialbox = true;
        tutorialcontroller.openvideobackground = true;
        requierementtext.text = "Complete a chain attack to continue the tutorial.";

        tutorialcontroller.attacktutorialstart();
        tutorialtextbox.SetActive(true);
        videobackground.SetActive(true);
        tutorialcontroller.newvideo(tutorialvideo);

        attack1action = controlls.Player.Attack1.GetBindingDisplayString();
        attack2action = controlls.Player.Attack2.GetBindingDisplayString();
        attack3action = controlls.Player.Attack3.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + attack1action + "</color>" + "\" to attack. " +
                    "While this attack there is a small window to press \"" + "<color=green>" + attack2action + "</color>" + "\" to continue your attack combo. \n" +
                    "While performing your second attack press \"" + "<color=green>" + attack1action + "</color>" + "\" (downward attack), \"" +
                    "<color=green>" + attack2action + "</color>" + "\" (center attack) or \"" + "<color=green>" +
                    attack3action + "</color>" + "\" (upward attack) to finish the chain attack. \n" +
                    "\nIts possible to perform this chain attack 2 times before you have to reset.";
    }

    public void enemysizetutorial()
    {
        tutorialcontroller.opentutorialbox = true;
        tutorialcontroller.openvideobackground = true;
        requierementtext.text = "Trigger the weak spot of the dummy to continue the tutorial.\n(1-2-2 chain attack)";

        tutorialcontroller.enemysizetutorialstart();
        tutorialtextbox.SetActive(true);
        videobackground.SetActive(true);
        tutorialcontroller.newvideo(tutorialvideo);

        tutorialtext.text = "On the left, of the enemy health bar, there is a letter and number. \n" +
                    "The number displays the enemy level, the letter shows the type. " +
                    "<color=green>S</color>(<color=green>small</color>), <color=green>M</color>(<color=green>medium</color>" +
                    ") or <color=green>B</color>(<color=green>Big</color>).\n" +
                    "\nDepeding on these types, your " + "<color=green>" + "downward" + "</color>" + ", " + "<color=green>" + "center " + "</color>" +
                    "or " + "<color=green>" + "upward " + "</color>" + "attacks at the end of your " + "<color=green>" + "chain attack" + "</color>" + " will result in different damage.\n" +
                    "\nFor example: Hitting a " + "<color=green>" + "big " + "</color>" + "enemy with an " + "<color=green>" + "downward " + "</color>" +
                    "attack will deal " + "<color=green>" + "100% " + "</color>" + "damage.\n" +
                    "Hitting the weak spot with an " + "<color=green>" + "center " + "</color>" + "attack, will deal " + "<color=green>" + "85% " + "</color>" + "damage.\n" +
                    "However, this will increase the damage dealt with an " + "<color=green>" + "upward " + "</color>" + "attack to " + "<color=green>" + "150% " + "</color>" +
                    "instead of " + "<color=green>" + "50% " + "</color>" + ". \n" +
                    "\nA blue bar beneath the enemy health bar will show how long the weak spot is exposed.\n" +
                    "The yellow bar displays when the weak spot can be triggered again.\n" +
                    "\nAlso your " + "<color=green>" + "critical chance " + "</color>" + "against this enemy is increased while the weak spot is triggered and as long as " +
                    "the weak point trigger refreshs.";
    }
    public void enemyspecialtutorial()
    {
        // Statics.enemyspecialcd wird im tutorialcontroller schon im startenemysizetutorial umgestellt und im skillpointtutorial wieder zurückgesetzt
        // tutorial wird mit der spezial vom dummy getrigger(Dummyspezialcontroller)
        tutorialcontroller.opentutorialbox = true;
        tutorialcontroller.openvideobackground = true;
        requierementtext.text = "Avoid the dummies spezial attack to continue the tutorial.";

        tutorialtextbox.SetActive(true);
        videobackground.SetActive(true);
        tutorialcontroller.newvideo(tutorialvideo);

        buttonmashhotkey = controlls.Player.Attack3.GetBindingDisplayString();

        tutorialtext.text = "Each enemy has a different special attack.\n" +
                            "\nThe dummy will stun you and spawn a damage area on your postion.\n" +
                            "Button mash \"<color=green>" + buttonmashhotkey + "</color>\" to get out of the stun and then leave the damage area.";
    }
    public void skillpointtutorial()
    {
        if (Statics.enemyspecialcd == tutorialcontroller.newspecialcd) Statics.enemyspecialcd = tutorialcontroller.savetutroialenemyspecialcd;
        tutorialcontroller.currenttutorialdisable();
        requierementtext.text = string.Empty;

        tutorialcontroller.opentutorialbox = true;
        tutorialcontroller.openvideobackground = false;
        tutorialtextbox.SetActive(true);
        videobackground.SetActive(false);

        tutorialtext.text = "Each levelup you gain 1 skill point. To use these skill points, open the menu(" + "<color=green>" + "ESC" + "</color>" + ") and click on skill tree." +
                            "\nEach Character got his own skill tree and there is no limitation on switching these points.\n" +
                            "\n\"Health\" increase the hit points and healing done by this character.\n" +
                            "\n\"Defense\" reduce the damage taken. " + Statics.defenseconvertedtoattack + "% off the character defense will converted to attack damage.\n" +
                            "\n\"Critical chance\" and \"Critical damage\" increases the amount of critical strikes and there damage.\n" +
                            "\n\"Weaponswitch\" increases the damage buff and buff duration after switching a weapon.\n" +
                            "\n\"Charswitch\" increases the damage buff and buff duration after switching the character.\n" +
                            "\n\"Basic\" increases the damage done by the last attack of your chain attack and the critical chance granted from hitting the enemy weak spot.\n" +
                            "\nFor more turotials click on \"<color=green>Tutorial</color>\" in the menu overview.";
    }
}

