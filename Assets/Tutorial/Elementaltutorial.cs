using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elementaltutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    private Tutorialcontroller tutorialcontroller;
    private int textindex;

    private bool readinputs;

    [SerializeField] private GameObject elementalstonechest;
    private void Start()
    {
        tutorialcontroller = GetComponentInParent<Tutorialcontroller>();
        controlls = Keybindinputmanager.inputActions;
        readinputs = false;
        if (Statics.elementalmenuunlocked == true)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (readinputs == true && controlls.Player.Interaction.WasPressedThisFrame())
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
        if (other.gameObject == LoadCharmanager.Overallmainchar && Statics.elementalmenuunlocked == false)
        {
            tutorialcontroller.onenter();
            textindex = 0;
            readinputs = true;
            showtext();
        }
    }
    private void showtext()
    {
        if (textindex == 0) tutorialcontroller.tutorialtext.text = "<color=green>" + "Elementalmenu " + "</color>" + "unlocked. It can be open up in the menu. " +
                                                                   "The elementalmenu enables " + "<color=green>" + "classes " + "</color>" + "and " + "<color=green>" + "spells" + "</color>" + ".";
        else if (textindex == 1) tutorialcontroller.tutorialtext.text = "Each character is able to choose spells from to different elements.";
        else if (textindex == 2) tutorialcontroller.tutorialtext.text = "The first element depends on your selected character. However the second one is base on a "
                                                                        + "<color=green>" + "classstone " + "</color>" +".";
        else if (textindex == 3) tutorialcontroller.tutorialtext.text = "Classstones have to be awaked first. Afer awaken a stone it can be connected to a character, " +
                                                                        "to set the second element and character class.";
        else if (textindex == 4) tutorialcontroller.tutorialtext.text = "Also awaken a stone will grant some permanent bonus for the group.";
        else if (textindex == 5) tutorialcontroller.tutorialtext.text = "There are 3 different classes. " + "<color=green>" + "Damagedealer " + "</color>" + ", " + "<color=green>" + "heal " + "</color>" +
                                                                        "and " + "<color=green>" + "guard " + "</color>" + ".";
        else if (textindex == 6) tutorialcontroller.tutorialtext.text = "Damagedealer will deal more damage";
        else if (textindex == 7) tutorialcontroller.tutorialtext.text = "Heal grants an healbonus and will be able to heal the group and ressurect";
        else if (textindex == 8) tutorialcontroller.tutorialtext.text = "Guard gives some bonus HP and reduce the damage taken. It also increases threat on enemies.";       
   
    }
    private void endtutorial()
    {
        Statics.elementalmenuunlocked = true;
        readinputs = false;
        elementalstonechest.GetComponent<Rewardinterface>().addrewardcount();
        tutorialcontroller.endtutorial();
        gameObject.SetActive(false);
    }
}
