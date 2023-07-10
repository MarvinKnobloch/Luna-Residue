using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class Elementaltutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private GameObject tutorialisactiv;
    [SerializeField] GameObject tutorialtextbox;
    [SerializeField] private TextMeshProUGUI tutorialtext;
    [SerializeField] private VideoClip videoclip;
    [SerializeField] private Videocontroller videocontroller;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        if (Statics.elementalmenuunlocked == true)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if (Statics.elementalmenuunlocked == false)
        {
            Statics.elementalmenuunlocked = true;
            settext();
            tutorialtextbox.SetActive(true);
            videocontroller.gameObject.SetActive(true);
            videocontroller.setnewvideo(videoclip);

            LoadCharmanager.autosave();
            tutorialisactiv.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void settext()
    {
        tutorialtext.text = "<color=green>" + "Elemental Menu " + "</color>" + "unlocked. It now can be selected in the menu.\n" +
                            "The Elemental Menu enables " + "<color=green>" + "spells " + "</color>" + "and " + "<color=green>" + "classes" + "</color>" + ". " +
                            "Each character is able to choose spells from to different elements.\n" +
                            "\nThe first element depends on your selected character. However the second one is based on a " + "<color=green>" + "elemental stone" + "</color>" + ".\n" +
                            "\nElemental stones have to be awaken first before you can use them.\n" +
                            "Awaken a stone also grants a permanent bonus for the group. \n" +
                            "\nThese stones also define the class of the character. \n" +
                            "There are 3 different classes. " + "<color=green>" + "Fight" + "</color>" + ", " + "<color=green>" + "Heal " + "</color>" + "and " + "<color=green>" + "Guard" + "</color>" + ".\n" +
                            "\nFight: The character deals increased damage.\n" +
                            "Heal: Grants a healbonus and add the ability to cast a group heal and resurrect.\n" +
                            "Guard: Will add some bonus health and reduce the damage taken. It also increases threat on enemies.\n" +
                            "\nDevelopment Note: The spell system is in a really early state so its unfinished and may cause bugs";
    }
}

