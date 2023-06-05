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
    [SerializeField] private GameObject toggleskilltreeinfo;
    public TextMeshProUGUI attributeinfotext;

    [SerializeField] private GameObject resetlayer;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        if (PlayerPrefs.GetFloat("Skilltreeinfo") == 0) toggleskilltreeinfo.gameObject.SetActive(true);
        else toggleskilltreeinfo.gameObject.SetActive(false);
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
    }
    public void bonusupdate(int currentchar)
    {
        healthbonus(currentchar);
        critbonus(currentchar);
        switchbonus(currentchar);
        basicbonus(currentchar);
    }
    public void healthbonus(int currentchar)
    {
        int skillpoints = Statics.charhealthskillpoints[currentchar] + Statics.chardefenseskillpoints[currentchar];

        if (skillpoints >= Statics.firstbonuspointsneeded) bonusdmgafterhealtext.gameObject.SetActive(true);
        else bonusdmgafterhealtext.gameObject.SetActive(false);

        if (skillpoints >= Statics.secondbonuspointsneeded) bonusovertimetext.gameObject.SetActive(true);
        else bonusovertimetext.gameObject.SetActive(false);
    }
    public void critbonus(int currentchar)
    {
        int skillpoints = Statics.charcritchanceskillpoints[currentchar] + Statics.charcritdmgskillpoints[currentchar];

        if (skillpoints >= Statics.firstbonuspointsneeded) bonuscritstackstext.gameObject.SetActive(true);
        else bonuscritstackstext.gameObject.SetActive(false);

        if (skillpoints >= Statics.secondbonuspointsneeded) bonuscritdmgtext.gameObject.SetActive(true);
        else bonuscritdmgtext.gameObject.SetActive(false);
    }
    public void switchbonus(int currentchar)
    {
        int skillpoints = Statics.charweaponskillpoints[currentchar] + Statics.charcharswitchskillpoints[currentchar];

        if (skillpoints >= Statics.firstbonuspointsneeded) bonuscharexplosiontext.gameObject.SetActive(true);
        else bonuscharexplosiontext.gameObject.SetActive(false);

        if (skillpoints >= Statics.secondbonuspointsneeded) bonusdmgweaponswitchtext.gameObject.SetActive(true);
        else bonusdmgweaponswitchtext.gameObject.SetActive(false);
    }
    public void basicbonus(int currentchar)
    {
        int skillpoints = Statics.charbasicskillpoints[currentchar];

        if (skillpoints >= Statics.firstbonuspointsneeded) bonusbasicdurationtext.gameObject.SetActive(true);
        else bonusbasicdurationtext.gameObject.SetActive(false);

        if (skillpoints >= Statics.secondbonuspointsneeded) bonusneutraldmgtext.gameObject.SetActive(true);
        else bonusneutraldmgtext.gameObject.SetActive(false);
    }

    public void changeskilltreeinfostate()
    {
        if (PlayerPrefs.GetFloat("Skilltreeinfo") == 0)
        {
            PlayerPrefs.SetFloat("Skilltreeinfo", 1);
            toggleskilltreeinfo.gameObject.SetActive(false);
        }

        else
        {
            PlayerPrefs.SetFloat("Skilltreeinfo", 0);
            toggleskilltreeinfo.gameObject.SetActive(true);
        }
    }

}
