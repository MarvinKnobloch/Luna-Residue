using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using TMPro;
using Cinemachine;

public class Targettutorial : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private GameObject tutorialisactiv;
    [SerializeField] private Areacontroller areacontroller;
    [SerializeField] GameObject tutorialtextbox;
    [SerializeField] private TextMeshProUGUI tutorialtext;
    [SerializeField] private VideoClip videoclip;
    [SerializeField] private Videocontroller videocontroller;

    private string targetswitch;
    private string grouptarget;
    private string support1target;
    private string support2target;

    private int tutorialnumber;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void Start()
    {
        tutorialnumber = GetComponent<Areanumber>().areanumber;
        if (areacontroller.tutorialcomplete[tutorialnumber] == true)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && areacontroller.tutorialcomplete[tutorialnumber] == false)
        {
            settext();
            tutorialtextbox.SetActive(true);
            videocontroller.gameObject.SetActive(true);
            videocontroller.setnewvideo(videoclip);

            areacontroller.tutorialcomplete[tutorialnumber] = true;
            LoadCharmanager.autosave();
            tutorialisactiv.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void settext()
    {
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
}
