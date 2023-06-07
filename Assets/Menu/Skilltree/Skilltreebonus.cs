using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Skilltreebonus : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private TextMeshProUGUI bonusdmgafterhealtext;
    [SerializeField] private TextMeshProUGUI bonusovertimetext;
    [SerializeField] private TextMeshProUGUI bonuscritstackstext;
    [SerializeField] private TextMeshProUGUI bonuscritdmgtext;
    [SerializeField] private TextMeshProUGUI bonuscharexplosiontext;
    [SerializeField] private TextMeshProUGUI bonusdmgweaponswitchtext;
    [SerializeField] private TextMeshProUGUI bonusbasicdurationtext;
    [SerializeField] private TextMeshProUGUI bonusneutraldmgtext;

    [SerializeField] private GameObject bonusinfoimage;
    public TextMeshProUGUI attributeinfotext;
    [SerializeField] private GameObject attributebonusinfo;
    [SerializeField] private GameObject attributebonusrequirement;

    [SerializeField] private GameObject resetlayer;

    private int currentselectedchar;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        if (PlayerPrefs.GetFloat("Skilltreeinfo") == 0) attributebonusinfo.gameObject.SetActive(true);
        else attributebonusinfo.gameObject.SetActive(false);
        attributeinfotext.text = "Attribute Bonus";
        bonusinfoimage.SetActive(false);
    }
    private void Update()
    {
        if (controlls.Menusteuerung.F1.WasPerformedThisFrame())
        {
            changeskilltreeinfostate();
            resetlayer.SetActive(true);
        }
        if (controlls.Menusteuerung.F2.WasPerformedThisFrame())
        {
            changeattributerequirementinfo();
        }
    }
    public void bonusupdate(int currentchar)
    {
        currentselectedchar = currentchar;
        healthbonus(currentselectedchar);
        critbonus(currentselectedchar);
        switchbonus(currentselectedchar);
        basicbonus(currentselectedchar);
    }
    public void healthbonus(int currentchar)
    {
        int skillpoints = Statics.charhealthskillpoints[currentchar] + Statics.chardefenseskillpoints[currentchar];

        if (skillpoints >= Statics.firstbonuspointsneeded) bonusdmgafterhealtext.gameObject.SetActive(true);
        else bonusdmgafterhealtext.gameObject.SetActive(false);

        if (skillpoints >= Statics.secondbonuspointsneeded) bonusovertimetext.gameObject.SetActive(true);
        else bonusovertimetext.gameObject.SetActive(false);

        bonusdmgafterhealtext.text = "Bonus damage after heal";
        bonusovertimetext.text = "Heal over time";
        if (PlayerPrefs.GetFloat("Attributerequirementinfo") == 0)
        {
            bonusdmgafterhealtext.text += "\n<color=yellow><size=70%>(" + skillpoints + "/" + Statics.firstbonuspointsneeded + " Health/Defense)</color>";
            bonusovertimetext.text += "\n<color=yellow><size=70%>(" + skillpoints + "/" + Statics.secondbonuspointsneeded + " Health/Defense)</color>";
        }
    }
    public void critbonus(int currentchar)
    {
        int skillpoints = Statics.charcritchanceskillpoints[currentchar] + Statics.charcritdmgskillpoints[currentchar];

        if (skillpoints >= Statics.firstbonuspointsneeded) bonuscritstackstext.gameObject.SetActive(true);
        else bonuscritstackstext.gameObject.SetActive(false);

        if (skillpoints >= Statics.secondbonuspointsneeded) bonuscritdmgtext.gameObject.SetActive(true);
        else bonuscritdmgtext.gameObject.SetActive(false);

        bonuscritstackstext.text = "Dash explosion";
        bonuscritdmgtext.text = "Critcal damage bonus";
        if (PlayerPrefs.GetFloat("Attributerequirementinfo") == 0)
        {
            bonuscritstackstext.text += "\n<color=yellow><size=70%>(" + skillpoints + "/" + Statics.firstbonuspointsneeded + " Crit Chance/Damage)</color>";
            bonuscritdmgtext.text += "\n<color=yellow><size=70%>(" + skillpoints + "/" + Statics.secondbonuspointsneeded + " Crit Chance/Damage)</color>";
        }
    } 
    public void switchbonus(int currentchar)
    {
        int skillpoints = Statics.charweaponskillpoints[currentchar] + Statics.charcharswitchskillpoints[currentchar];

        if (skillpoints >= Statics.firstbonuspointsneeded) bonuscharexplosiontext.gameObject.SetActive(true);
        else bonuscharexplosiontext.gameObject.SetActive(false);

        if (skillpoints >= Statics.secondbonuspointsneeded) bonusdmgweaponswitchtext.gameObject.SetActive(true);
        else bonusdmgweaponswitchtext.gameObject.SetActive(false);

        bonuscharexplosiontext.text = "Improved character switch";
        bonusdmgweaponswitchtext.text = "Improved weapon switch";

        if (PlayerPrefs.GetFloat("Attributerequirementinfo") == 0)
        {
            bonuscharexplosiontext.text += "\n<color=yellow><size=70%>(" + skillpoints + "/" + Statics.firstbonuspointsneeded + " Weapon/Char Switch)</color>";
            bonusdmgweaponswitchtext.text += "\n<color=yellow><size=70%>(" + skillpoints + "/" + Statics.secondbonuspointsneeded + " Weapon/Char Switch)</color>";
        }
    }
    public void basicbonus(int currentchar)
    {
        int skillpoints = Statics.charbasicskillpoints[currentchar];

        if (skillpoints >= Statics.firstbonuspointsneeded) bonusbasicdurationtext.gameObject.SetActive(true);
        else bonusbasicdurationtext.gameObject.SetActive(false);
        
        if (skillpoints >= Statics.secondbonuspointsneeded) bonusneutraldmgtext.gameObject.SetActive(true);
        else bonusneutraldmgtext.gameObject.SetActive(false);

        bonusbasicdurationtext.text = "Improved weak spot expose";
        bonusneutraldmgtext.text = "Improved neutral attack";

        if (PlayerPrefs.GetFloat("Attributerequirementinfo") == 0)
        {
            bonusbasicdurationtext.text += "\n<color=yellow><size=70%>(" + skillpoints + "/" + Statics.firstbonuspointsneeded + " Basic)</color>";
            bonusneutraldmgtext.text += "\n<color=yellow><size=70%>(" + skillpoints + "/" + Statics.secondbonuspointsneeded + " Basic)</color>";
        }
    }

    public void changeskilltreeinfostate()
    {
        if (PlayerPrefs.GetFloat("Skilltreeinfo") == 0)
        {
            PlayerPrefs.SetFloat("Skilltreeinfo", 1);
            attributebonusinfo.gameObject.SetActive(false);
        }

        else
        {
            PlayerPrefs.SetFloat("Skilltreeinfo", 0);
            attributebonusinfo.gameObject.SetActive(true);
        }
    }
    public void changeattributerequirementinfo()
    {
        if (PlayerPrefs.GetFloat("Attributerequirementinfo") == 0)
        {
            PlayerPrefs.SetFloat("Attributerequirementinfo", 1);
            attributebonusrequirement.gameObject.SetActive(false);
            bonusupdate(currentselectedchar);
        }

        else
        {
            PlayerPrefs.SetFloat("Attributerequirementinfo", 0);
            attributebonusrequirement.gameObject.SetActive(true);
            bonusupdate(currentselectedchar);
        }
    }
}
