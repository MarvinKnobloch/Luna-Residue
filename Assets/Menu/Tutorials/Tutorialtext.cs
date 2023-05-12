using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tutorialtext : MonoBehaviour
{
    private SpielerSteu controlls;
    private Tutorialmenucontroller tutorialmenucontroller;

    [SerializeField] private TextMeshProUGUI tutorialtext;
    [SerializeField] private Scrollbar scrollbar;

    private string dash;
    private string attack1action;
    private string attack2action;
    private string attack3action;

    private string characterswitch;
    private string weaponswitch;

    private string attack4;

    private string upgradeitem;

    private string targetswitch;
    private string grouptarget;
    private string support1target;
    private string support2target;

    private void Start()
    {
        tutorialmenucontroller = GetComponentInParent<Tutorialmenucontroller>();
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        tutorialtext.text = string.Empty;
    }

    private void settextvalues()
    {
        scrollbar.value = 1;
        tutorialmenucontroller.menusoundcontroller.playmenubuttonsound();
    }
    public void setdashtext()
    {
        settextvalues();
        dash = controlls.Player.Dash.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + dash + "</color>" + "\" to perform a dash. \n" +
                            "This can be usefull to dodge attacks and cross small gaps. \n" +
                            "\nIt also makes the character immune to damage for a small duration.";
    }
    public void setattacktext()
    {
        settextvalues();
        attack1action = controlls.Player.Attack1.GetBindingDisplayString();
        attack2action = controlls.Player.Attack2.GetBindingDisplayString();
        attack3action = controlls.Player.Attack3.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + attack1action + "</color>" + "\" to attack. " +
                            "Meanwhile this attack there is a small window to press \"" + "<color=green>" + attack2action + "</color>" + "\" to continue your attackchain. \n" +
                            "While performing your second attack press \"" + "<color=green>" + attack1action + "</color>" + "\" (downattack), \"" +
                            "<color=green>" + attack2action + "</color>" + "\" (midattack) or \"" + "<color=green>" +
                            attack3action + "</color>" + "\" (upattack) to finish the chainattack. \n" +
                            "\nIts possible to perform this attackchain 2 times before you have to reset.";
    }
    public void setenemysizetext()
    {
        settextvalues();
        tutorialtext.text = "On the left, of the enemy health bar, there is a letter and number. \n" +
                            "The number displays the enemy level, the letter shows the type. " +
                            "<color=green>S</color>(<color=green>small</color>), <color=green>M</color>(<color=green>medium</color>" +
                            ") or <color=green>B</color>(<color=green>Big</color>). \n" +
                            "\nDepeding on these types, your " + "<color=green>" + "down" + "</color>" + ", " + "<color=green>" + "mid " + "</color>" +
                            "or " + "<color=green>" + "up " + "</color>" + "attack at the end of your " + "<color=green>" + "attackchain" + "</color>" + " will result in different damage. \n" +
                            "\nFor example: Hitting a " + "<color=green>" + "big " + "</color>" + "enemy with an " + "<color=green>" + "down " + "</color>" +
                            "attack will deal " + "<color=green>" + "100% " + "</color>" + "damage.\n" +
                            "Hitting the weak spot with an " + "<color=green>" + "mid " + "</color>" + "attack, will deal " + "<color=green>" + "85% " + "</color>" + "damage. \n " +
                            "However, this will increase the damage dealt with an " + "<color=green>" + "up " + "</color>" + "attack to " + "<color=green>" + "150% " + "</color>" +
                            "instead of " + "<color=green>" + "50% " + "</color>" + ". \n" +
                            "\nA blue bar beneath the enemy health bar, will show how long the weak spot is exposed. \n" +
                            "The yellow bar displays, when the weak spot can be triggered again. \n" +
                            "\nAlso your " + "<color=green>" + "critchance " + "</color>" + "against this enemy is increased, meanwhile the weak spot is triggered and aslong as " +
                            "the weak point trigger refreshs.";
    }
    public void setattributetext()
    {
        settextvalues();
        tutorialtext.text = "Health increase the hitpoints and healing done by this character. \n" +
                            "\nDefense reduce the damage taken. " + Statics.defenseconvertedtoattack + "% off the character defense will converted to attackdamage- \n" +
                            "\nAttack increases damage.\n" +
                            "\nCritchance and critdmg increases the amount of critical strikes and there damage. \n" +
                            "\nWeaponswitch increases the damagebuff and buff duration after switching a weapon. \n" +
                            "\nCharswitch increases the damagebuff and buff duration after switching the character. \n" +
                            "\nBasic increases the damage done by the last attack of your attackchain and the critchance granted from hitting the enemy weak spot.";
    }
    public void setattackchaintext()
    {
        settextvalues();
        tutorialtext.text = "Hitting an enemy with a ground/mid/up attack will heal the character for a small amount. \n" +
                            "This heal amount is only effected by the group level. \n" +
                            "\nTriggering the weak spot of an enemy, will reset your attackchain counter. \n" +
                            "Also performing a up attack does not check your attackchain counter.";
    }
    public void setswitchtext()
    {
        settextvalues();
        characterswitch = controlls.Player.Charchange.GetBindingDisplayString();
        weaponswitch = controlls.Player.Weaponchange.GetBindingDisplayString();
        tutorialtext.text = "In the menu overview, leftclick on the character slot to set your main character and rightclick to set your second character. \n" +
                            "Beneath the character slots, you can choose which weapon each member of your group should use. \n" +
                            "\nWhile playing press \"" + "<color=green>" + characterswitch + "</color>" + "\" to switch your character and \"" + "<color=green>" + weaponswitch + "</color>" + "\" " +
                            "to switch the weapon. \n" +
                            "\nIf you switch your character/weapon next to an enemy, a bonus attack will be performed base on the weapontype. \n" +
                            "\nSwitching the character/weapon will grant a damage buff. The duration of the buff is displayed on the bottem left, next to the character/weapon icons.";
    }
    public void setbowgrapplingtext()
    {
        settextvalues();
        attack4 = controlls.Player.Attack4.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + attack4 + "</color>" + "\" while using a bow, to grapple to your current enemies postion. \n" +
                            "This can be usefull to close the gap between you and your enemy before switchting a weapon.";
    }
    public void setupgradetext()
    {
        settextvalues();
        upgradeitem = controlls.Equipmentmenu.Upgradeitem.GetBindingDisplayString();
        tutorialtext.text = "To upgrade your equipment, open the equipment menu.\n" +
                            "\nSelect a armor slot and hover over your collected items. (weapons can´t be upgraded)\n" +
                            "\nA window will appear which displays max level, current attributes, upgraded attributes and the material costs.\n" +
                            "\nHold \"" + "<color=green>" + upgradeitem + "</color>" + "\" to upgrade the item you hover over. \n" +
                            "\nThe number on top left of each armor piece shows the current level.";
    }
    public void settargetingtext()
    {
        settextvalues();
        targetswitch = controlls.Player.Lockonchange.GetBindingDisplayString();
        grouptarget = controlls.Player.Setalliestarget.GetBindingDisplayString();
        support1target = controlls.Player.Character3target.GetBindingDisplayString();
        support2target = controlls.Player.Character4target.GetBindingDisplayString();
        tutorialtext.text = "The icon, on the right side of the enemy health bar shows the current target of the enemy.\n" +
                            "\nThe enemy type and level display, of the players current traget, is red instead of white.\n" +
                            "Use \"" + "<color=green>" + targetswitch + "</color>" + "\" to switch your target. \n" +
                            "\nPressing \"" + "<color=green>" + grouptarget + "</color>" + "\" will force your allies, to attack your current target. \n" +
                            "Use \"" + "<color=green>" + support1target + "</color>" + "\" and \"" + "<color=green>" + support2target + "</color>" + "\" to set the target of your allies separately.";
    }
    public void setelementalmenutext()
    {
        settextvalues();
        tutorialtext.text = "Each character is able to choose spells from to different elements.\n" +
                            "The first element depends on your selected character. However the second one is based on a " + "<color=green>" + "elemental stone" + "</color>" + ".\n" +
                            "\nElemental stones have to be awaken first before you can use them, to set the second element.\n" +
                            "Awaken a stone also grants a permanent bonus for the group. \n" +
                            "\nThese stones also define the class of the character. \n" + 
                            "There are 3 different classes. " + "<color=green>" + "Fight" + "</color>" + ", " + "<color=green>" + "Heal " + "</color>" + "and " + "<color=green>" + "Guard" + "</color>" + ".\n" +
                            "\nFight: The character deals increased damage.\n" +
                            "Heal: Grants a healbonus and add the ability to cast a group heal and ressurect.\n" +
                            "Guard: Will add some bonus HP and reduce the damage taken. It also increases threat on enemies.";
    }
}
