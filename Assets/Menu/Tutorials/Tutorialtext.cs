using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Tutorialtext : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private TextMeshProUGUI tutorialtext;

    private string dash;
    private string attack1action;
    private string attack2action;
    private string attack3action;

    private string characterswitch;
    private string weaponswitch;

    private string attack4;

    private string targetswitch;
    private string grouptarget;
    private string support1target;
    private string support2target;

    private void Start()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        tutorialtext.text = string.Empty;
    }
    public void setdashtext()
    {
        dash = controlls.Player.Dash.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + dash + "</color>" + "\" to perform a dash. \n" +
                            "\n This can be usefull to dodge attacks and cross small gaps. \n" +
                            "\n It also makes the character immune to damage for a small duration.";
    }
    public void setattacktext()
    {
        attack1action = controlls.Player.Attack1.GetBindingDisplayString();
        attack2action = controlls.Player.Attack2.GetBindingDisplayString();
        attack3action = controlls.Player.Attack3.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + attack1action + "</color>" + "\" to attack. \n" +
                            "Meanwhile this attack there is a small window to press \"" + "<color=green>" + attack2action + "</color>" + "\" to continue your attackchain. \n" +
                            "While performing your second attack press \"" + "<color=green>" + attack1action + "</color>" + "\" (downattack), \"" +
                            "<color=green>" + attack2action + "</color>" + "\" (midattack) or \"" + "<color=green>" +
                            attack3action + "</color>" + "\" (upattack) to finish the chainattack. \n" +
                            "\n Its possible to perform this attackchain 2 times before you have to reset.";
    }
    public void setenemysizetext()
    {
        tutorialtext.text = "On the left, of the enemy health bar, there is a letter and number." +
                            "The number displays the enemy level, the letter shows the type. \n" +
                            "\n <color=green>" + "S(small)" + "</color>" + ", " + "<color=green>" + "M(medium) " + "</color>" + "or " + "<color=green>" + "B(big) " + "</color> \n" +
                            "\n Depeding on these types, your " + "<color=green>" + "down" + "</color>" + ", " + "<color=green>" + "mid " + "</color>" +
                            "or " + "<color=green>" + "up " + "</color>" + "attack at the end of your " + "<color=green>" + "attackchain" + "</color>" + " will result in different damage. \n" +
                            "\n For example: Hitting a " + "<color=green> " + "big " + "</color>" + "enemy with an " + "<color=green>" + "down " + "</color>" +
                            "attack will deal " + "<color=green>" + "100% " + "</color>" + "damage.\n " +
                            "\n Hitting the weak spot with an " + "<color=green>" + "mid " + "</color>" + "attack, will deal " + "<color=green>" + "85% " + "</color>" + "damage. \n " +
                            "However, this will increase the damage dealt with an " + "<color=green>" + "up " + "</color>" + "attack to " + "<color=green>" + "150% " + "</color>" +
                            "instead of " + "<color=green>" + "50% " + "</color>" + ". \n" +
                            "\n A blue bar, beneath the enemy health bar, will show how long the weak spot is exposed. \n" +
                            "The yellow bar displays, when the weak spot can be triggered again. \n" +
                            "\n Also your " + "<color=green>" + "critchance " + "</color>" + "against this enemy is increased, meanwhile the weak spot is triggered and aslong as " +
                            "the weak point trigger refreshs.";
    }
    public void setattributetext()
    {
        tutorialtext.text = "Health increase the hitpoints and healing done by this character. \n" +
                            "\n Defense reduce the damage taken. " + Statics.defenseconvertedtoattack + "% off the character defense will converted to attackdamage \n" +
                            "\n Attack increases damage.\n" +
                            "\n Critchance and critdmg increases the amount of critical strikes and there damage. \n" +
                            "\n Weaponswitch increases the damagebuff and buff duration after switching a weapon. \n" +
                            "\n Charswitch increases the damagebuff and buff duration after switching the character. \n" +
                            "\n Basic increases the damage done by the last attack of the attackchain and the critchance granted by hitting the weak spot.";
    }
    public void setattackchaintext()
    {
        tutorialtext.text = "Hitting an enemy with a ground/mid/up attack will heal the character for a small amount \n" +
                            "This heal amount is only effected by the group level \n" +
                            "\n If you hit the enemy weak spot, your attackchain counter will reset \n" +
                            "Performing a up attack does not check your attackchain counter";
    }
    public void setswitchtext()
    {
        characterswitch = controlls.Player.Charchange.GetBindingDisplayString();
        weaponswitch = controlls.Player.Weaponchange.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + characterswitch + "</color>" + "\" to switch your character and \"" + "<color=green>" + weaponswitch + "</color>" + "\"" +
                            "to switch your weapon \n" +
                            "\n In the menuoverview, leftclick on the character slot to set your main character, rightclick to set your second character \n" +
                            " Beneath the character slots, you can choose, which weapon each member of your group will use \n" +
                            "\n If you switch your character/weapon next to an enemy, a bonus attack will be performed base on the weapontype \n" +
                            "\n Switching the character/weapon will grant a damage buff. The duration of the buff is displayed on the bottem left, next to the character/weapon icons";
    }
    public void setbowgrapplingtext()
    {
        attack4 = controlls.Player.Attack4.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + attack4 + "</color>" + "\" while using a bow, to grapple to your current enemies postion \n" +
                            "This can be usefull to close the gap between you and your enemy before switchting a weapon";
    }
    public void setupgradetext()
    {
        //tutorialtext.text = "In the equipment menu "
    }
    public void settargetingtext()
    {
        targetswitch = controlls.Player.Lockonchange.GetBindingDisplayString();
        grouptarget = controlls.Player.Setalliestarget.GetBindingDisplayString();
        support1target = controlls.Player.Character3target.GetBindingDisplayString();
        support2target = controlls.Player.Character4target.GetBindingDisplayString();
        tutorialtext.text = "The icon, on the right side of the enemy health bar, shows the current target of the enemy.\n" +
                            "\n The type and level display, of the players current traget, is red instead of white\n" +
                            "Press \"" + "<color=green>" + targetswitch + "</color>" + "\" to switch your target \n" +
                            "\n\"" + "<color=green>" + grouptarget + "</color>" + "\" will force your allies, to attack your current target \n" +
                            "Use \"" + "<color=green>" + support1target + "</color>" + "\" and \"" + "<color=green>" + support2target + "</color>" + "\" to set the target of your allies separately.";
    }
    public void setelementalmenutext()
    {
        tutorialtext.text = "Each character is able to choose spells from to different elements.\n" +
                            "The first element depends on your selected character. However the second one is based on a" + "<color=green>" + "classstone " + "</color>" + ".\n" +
                            "\n Classstones have to be awaked first. Afer awaken a stone it can be connected to a character, to set the second element and character class.\n" +
                            "\n Also awaken a stone will grant some permanent bonus for the group. \n" +
                            "\n There are 3 different classes. " + "<color=green>" + "Damagedealer " + "</color>" + ", " + "<color=green>" + "heal " + "</color>" + "and " + "<color=green>" + "guard " + "</color>" + ".\n";
        //else if (textindex == 6) tutorialcontroller.tutorialtext.text = "Damagedealer will deal more damage";
        //else if (textindex == 7) tutorialcontroller.tutorialtext.text = "Heal grants an healbonus and will be able to heal the group and ressurect";
       // else if (textindex == 8) tutorialcontroller.tutorialtext.text = "Guard gives some bonus HP and reduce the damage taken. It also increases threat on enemies.";
    }
}
