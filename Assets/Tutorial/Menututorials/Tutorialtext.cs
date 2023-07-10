using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Tutorialtext : MonoBehaviour, ISelectHandler
{
    private SpielerSteu controlls;
    private Tutorialmenucontroller tutorialmenucontroller;
    private Videomenucontroller videocontroller;

    [SerializeField] private TextMeshProUGUI tutorialtext;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private ScrollRect scrollrect;
    [SerializeField] private RectTransform scrollrecttransform;
    private RectTransform recttransfrom;

    [SerializeField] private VideoClip videoclip;
    [SerializeField] private UnityEvent function;

    private string dash;
    private string attack1action;
    private string attack2action;
    private string attack3action;

    private string heal;
    private string player1target;
    private string player2target;
    private string player3target;

    private string characterswitch;
    private string weaponswitch;

    private string attack4;

    private string upgradeitem;

    private string targetswitch;
    private string grouptarget;
    private string support1target;
    private string support2target;

    private string groupheal;

    private void Awake()
    {
        recttransfrom = GetComponent<RectTransform>();
        tutorialmenucontroller = GetComponentInParent<Tutorialmenucontroller>();
        videocontroller = GetComponentInParent<Videomenucontroller>();
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        tutorialtext.text = string.Empty;
        StartCoroutine("waitoneframe");
    }
    IEnumerator waitoneframe()
    {
        yield return null;
        tutorialmenucontroller.checkforhighstrec(transform, recttransfrom.rect.height);
    }

    public void OnSelect(BaseEventData eventData)
    {
        function.Invoke();
    }
    private void settextvalues()
    {
        checkforposi();
        tutorialmenucontroller.menusoundcontroller.playmenubuttonsound();
        videocontroller.newvideo(videoclip);
    }
    private void checkforposi()           //passt noch nicht 100%. Was ich eingentlich machen müsste ist currenthighestposi/lowestposi mit scrollrect.normalizedPosition + transform.localPosition.y bestimmen
    {
        if (transform.localPosition.y > tutorialmenucontroller.currenthighestposi)
        {
            float pullupamount = transform.localPosition.y - tutorialmenucontroller.currenthighestposi;
            float pullpct = pullupamount / tutorialmenucontroller.scrollsize;
            float newposi = scrollrect.normalizedPosition.y + pullpct;
            if (newposi > 1) newposi = 1;
            scrollrect.normalizedPosition = new Vector2(0, newposi);
            tutorialmenucontroller.upperrectupdate(transform.localPosition.y);
        }
        else if (transform.localPosition.y - recttransfrom.rect.height < tutorialmenucontroller.currentlowestposi)
        {
            float pulldownamount = (transform.localPosition.y - recttransfrom.rect.height - tutorialmenucontroller.currentlowestposi) * -1;
            float pullpct = pulldownamount / tutorialmenucontroller.scrollsize;
            float newposi = scrollrect.normalizedPosition.y - pullpct;
            if (newposi < 0) newposi = 0;
            scrollrect.normalizedPosition = new Vector2(0, newposi);
            tutorialmenucontroller.lowerrectupdate(transform.localPosition.y, recttransfrom.rect.height);
        }
    }
    public void setdashtext()
    {
        settextvalues();
        dash = controlls.Player.Dash.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + dash + "</color>" + "\" to perform a dash. \n" +
                            "This can be useful to dodge attacks and cross small gaps. \n" +
                            "\nThe stamina bar is displayed in the bottom left.";
    }
    public void sethealtext()
    {
        settextvalues();
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
    public void setattacktext()
    {
        settextvalues();
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
    public void setenemysizetext()
    {
        settextvalues();
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
    public void setenemyhitboxtext()
    {
        settextvalues();
        tutorialtext.text = "<color=green>Small</color> enemy:\n" +
                            "Center attack = 100% damage.\n" +
                            "Upward attack = 85% damage + trigger weak spot.\n" +
                            "Downward attack = 50% damage or 150% if weak spot is triggered.\n" +
                            "\n<color=green>Medium</color> enemy:\n" +
                            "Upward attack = 100% damage.\n" +
                            "Downward attack = 85% damage + trigger weak spot.\n" +
                            "Center attack = 50% damage or 150% if weak spot is triggered.\n" +
                            "\n<color=green>Big</color> enemy:\n" +
                            "Downward attack = 100% damage.\n" +
                            "Center attack = 85% damage + trigger weak spot.\n" +
                            "Upward attack = 50% damage or 150% if weak spot is triggered.\n";
    }
    public void setattributetext()
    {
        settextvalues();
        tutorialtext.text = "\"Health\" increase the hit points and healing done by this character. \n" +
                            "\n\"Defense\" reduce the damage taken. " + Statics.defenseconvertedtoattack + "% off the character defense will converted to attack damage. \n" +
                            "\n\"Critical chance\" and \"Critical damage\" increases the amount of critical strikes and there damage. \n" +
                            "\n\"Weaponswitch\" increases the damage buff and buff duration after switching a weapon. \n" +
                            "\n\"Charswitch\" increases the damage buff and buff duration after switching the character. \n" +
                            "\n\"Basic\" increases the damage done by the last attack of your chain attack and the critical chance granted from hitting the enemy weak spot.";
    }
    public void setattackchaintext()
    {
        settextvalues();
        tutorialtext.text = "Triggering the weak spot of an enemy will reset your attack combo counter. \n" +
                            "Also switching from ground chain attack into air chain attack does not check your attack combo counter.";
    }
    public void setswitchtext()
    {
        settextvalues();
        characterswitch = controlls.Player.Charchange.GetBindingDisplayString();
        weaponswitch = controlls.Player.Weaponchange.GetBindingDisplayString();
        tutorialtext.text = "In the menu overview, leftclick on the character slot to set your main character and rightclick to set your second character. \n" +
                            "Beneath the character slots you can choose which weapon each member of your group should use. \n" +
                            "\nWhile playing, press \"" + "<color=green>" + characterswitch + "</color>" + "\" to switch your character and \"" + "<color=green>" + weaponswitch + "</color>" + "\" " +
                            "to switch the weapon. \n" +
                            "\nIf you switch your character/weapon next to an enemy a bonus attack will be performed base on the weapon type.\n" +
                            "\nSwitching the character/weapon will grant a damage buff. The duration of the buff is displayed on the bottem left next to the character/weapon icons.\n" +
                            "Switching your character will also heal each member in your group for <color=green>8</color>% off there max HP.";
    }
    public void setbowgrapplingtext()
    {
        settextvalues();
        attack4 = controlls.Player.Attack4.GetBindingDisplayString();
        tutorialtext.text = "Press \"" + "<color=green>" + attack4 + "</color>" + "\" while using a bow to grapple to your current enemies postion. \n" +
                            "This can be useful to close the gap between you and your enemy before switchting a weapon.";
    }
    public void setupgradetext()
    {
        settextvalues();
        upgradeitem = controlls.Equipmentmenu.Upgradeitem.GetBindingDisplayString();
        tutorialtext.text = "To upgrade your equipment open the equipment menu.\n" +
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
                            "\nThe enemy type and level display of the players current target is red instead of white.\n" +
                            "Use \"" + "<color=green>" + targetswitch + "</color>" + "\" to switch your target. \n" +
                            "\nPressing \"" + "<color=green>" + grouptarget + "</color>" + "\" will force your allies to attack your current target. \n" +
                            "Use \"" + "<color=green>" + support1target + "</color>" + "\" and \"" + "<color=green>" + support2target + "</color>" + "\" to set the target of your allies separately.";
    }
    public void setelementalmenutext()
    {
        settextvalues();
        tutorialtext.text = "Each character is able to choose spells from to different elements.\n" +
                            "The first element depends on your selected character. However the second one is based on a " + "<color=green>" + "elemental stone" + "</color>" + ".\n" +
                            "\nElemental stones have to be awaken first before you can use them.\n" +
                            "Awaken a stone also grants a permanent bonus for the group. \n" +
                            "\nThese stones also define the class of the character. \n" + 
                            "There are 3 different classes. " + "<color=green>" + "Fight" + "</color>" + ", " + "<color=green>" + "Heal " + "</color>" + "and " + "<color=green>" + "Guard" + "</color>" + ".\n" +
                            "\nFight: The character deals increased damage.\n" +
                            "Heal: Grants a healbonus and add the ability to cast a group heal and resurrect.\n" +
                            "Guard: Will add some bonus health and reduce the damage taken. It also increases threat on enemies.";
    }
    public void setgrouphealrestext()
    {
        settextvalues();
        groupheal = controlls.SpielerHeal.Groupheal.GetBindingDisplayString();
        tutorialtext.text = "If you choose the heal class with your character, while in heal state, there will be <color=green>seven</color> buttons displayed instead of <color=green>three</color>.\n" +
                            "After successfully hitting <color=green>five</color> buttons you can press <color=green>" + groupheal + "</color> to heal all active members in your group.\n" +
                            "Hitting all <color=green>seven</color> buttons will change your heal into a more powerful spell that can resurrect players.\n" +
                            "However, this spell has a higher cooldown.\n" +
                            "\nAlly Heal Class: Your ally will spawn heal potions which heals your group on picking it up.\n" +
                            "Your teammate also can resurrect.\n" +
                            "\nEverytime someone is resurrected the cooldown of your and your teamates resurrection spell will increase by 1 second for the rest of the combat.\n";

    }
    /*float pos = 1 - ((1 - transform.localPosition.y) / tutorialmenucontroller.highestrect);
    //Debug.Log("pos " + pos);
    float difference;
        if (scrollrect.verticalNormalizedPosition > pos) difference = scrollbar.value - pos;
        else difference = pos - scrollrect.verticalNormalizedPosition;
        //Debug.Log("diff" + difference);
        if (difference > 0.3f) scrollrect.normalizedPosition = new Vector2(0, pos);
    //Debug.Log("srollbar" + scrollrect.verticalNormalizedPosition);*/
}
