using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Skilltreebonustigger : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    private Skilltreescript skilltreescript;

    private int currentchar;

    [SerializeField] private GameObject bonusinfoimage;
    private TextMeshProUGUI attributeinfotext;
    [SerializeField] private TextMeshProUGUI bonusinfotext;
    [SerializeField] private int attributenumber;

    private float bonusdmgafterheal;

    private float dashexplosiondmg;
    private float bonuscritchance;
    private float bonuscritdmg;

    private float charexplosiondmg;
    private float bonusweaponswitchdmg;
    private void Awake()
    {
        skilltreescript = GetComponentInParent<Skilltreescript>();
        attributeinfotext = GetComponentInParent<Skilltreebonus>().attributeinfotext;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<Skilltreescript>().skilltreebonustigger = this;
        textupdate();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        bonusinfoimage.SetActive(false);
        attributeinfotext.text = "Attribute Bonus";
    }
    public void textupdate()
    {
        if (PlayerPrefs.GetFloat("Skilltreeinfo") == 0)
        {
            currentchar = skilltreescript.currentchar;
            bonusinfoimage.SetActive(true);
            if (attributenumber == 1)
            {
                healthstatsupdate();
                sethealthtext();
            }
            else if (attributenumber == 2)
            {
                critstatsupdate();
                setcrittext();
            }
            else if (attributenumber == 3) 
            {
                switchstatsupdate();
                setswitchtext();
            }
            else if (attributenumber == 4) setbasictext();
            attributeinfotext.text = "Attribute Bonus Info";
        }
    }
    private void healthstatsupdate()
    {
        if (Statics.characterclassroll[currentchar] == 1)                      //damit die guardbonushp nicht zum healingbonus beeinflussen
        {
            float playerhealth = Statics.charmaxhealth[currentchar] - (Statics.charcurrentlvl * Statics.guardbonushpeachlvl);
            bonusdmgafterheal = Globalplayercalculations.calculatecasthealing(30, playerhealth, 0);
        }
        else if (Statics.characterclassroll[currentchar] == 2) bonusdmgafterheal = Globalplayercalculations.calculatecasthealing(Statics.basesingleheal, Statics.charmaxhealth[currentchar], Statics.groupstonehealbonus);
        else bonusdmgafterheal = Globalplayercalculations.calculatecasthealing(Statics.basesingleheal, Statics.charmaxhealth[currentchar], 0);
        bonusdmgafterheal = Mathf.Round(bonusdmgafterheal * Statics.bonusdmgafterhealreduction);
    }
    private void sethealthtext()
    {
        int skillpoints = Statics.charhealthskillpoints[currentchar] + Statics.chardefenseskillpoints[currentchar];

        bonusinfotext.text = "<u>Bonus damage after heal</u>\n<size=70%><color=yellow>(" + skillpoints + "/" + Statics.firstbonuspointsneeded + ") Points Health/Defense</color><size=100%>\n" +
                             "Casting a heal increases the damage of your next attack(<color=green>+" + bonusdmgafterheal + "</color>).\n" +
                             "\n<u>Heal over time</u>\n<size=70%><color=yellow>(" + skillpoints + "/" + Statics.secondbonuspointsneeded + ") Points Health/Defense</color><size=100%>\n" +
                             "While in combat every<color=green> " + Statics.bonushealtimer + "</color> seconds the character with lowest HP regenerates <color=green>" + 
                             Statics.bonushealpercentage * 100 + "</color>% of there max HP.";
    }

    private void critstatsupdate()
    {
        dashexplosiondmg = Globalplayercalculations.dashexplosion(Statics.charcritdmg[currentchar], Statics.charcritchance[currentchar]);
        bonuscritchance = Mathf.Round((100 - (Statics.charcritchance[currentchar] + 0.2f)) * Statics.bonuscritchancemultipler);  // +0.2f damit gescheit gerundet wird,sonst ändert sich der wert 2 mal und dann 3mal nicht
        bonuscritdmg = Mathf.Round(Statics.charcritdmg[currentchar] - 150);       
    }
    private void setcrittext()
    {
        int skillpoints = Statics.charcritchanceskillpoints[currentchar] + Statics.charcritdmgskillpoints[currentchar];

        bonusinfotext.text = "<u>Dash Explosion</u>\n<size=70%><color=yellow>(" + skillpoints + "/" + Statics.firstbonuspointsneeded + ") Points Critical Chance/Damage</color><size=100%>\n" +
                             "Your dash will cause an explosion that deals damage. The damage is base on stacks you gain for each critcal hit(max <color=green>" + Statics.bonuscritmaxstacks + "</color> stacks)." +
                             "For each stack you will deal<color=green> " + dashexplosiondmg + "</color> damage.\n" +
                             "\n<u>Critcal damage bonus</u>\n<size=70%><color=yellow>(" + skillpoints + "/" + Statics.secondbonuspointsneeded + ") Points Critical Chance/Damage</color><size=100%>\n" +
                             "Your critcal hits have a chance(<color=green>" + bonuscritchance + "</color>%) to deal more damage(<color=green>" + bonuscritdmg + "</color>%)." +
                             "\nAlso your critcal chance will increase by<color=green> " + Statics.bonuscritfromnoncrit + "</color>% for each non critcal hit you cause with your weapon." +
                             " A critcal hit will reset the bonus chance.";
    }
    private void switchstatsupdate()
    {
        charexplosiondmg = Globalplayercalculations.charexplison(currentchar);
        bonusweaponswitchdmg = Mathf.Round(Statics.bonusdmgweaponswitchmultipler * 100 - 100);
    }
    private void setswitchtext()
    {
        int skillpoints = Statics.charweaponskillpoints[currentchar] + Statics.charcharswitchskillpoints[currentchar];
        float heal = Mathf.Round(charexplosiondmg * Statics.bonuscharexplosionhealmultipler);

        bonusinfotext.text = "<u>Improved character switch</u>\n<size=70%><color=yellow>(" + skillpoints + "/" + Statics.firstbonuspointsneeded + ") Points Weapon/Character Switch</color><size=100%>\n" +
                             "Switching your character will cause an explosion that damages enemies(<color=green>" + charexplosiondmg + "</color>).\n" +
                             "\n<u>Improved weapon switch</u>\n<size=70%><color=yellow>(" + skillpoints + "/" + Statics.secondbonuspointsneeded + ") Points Weapon/Character Switch</color><size=100%>\n" +
                             "Your weapon switch attack deals more damage(<color=green>" + bonusweaponswitchdmg + "</color>%).";
    }
    private void setbasictext()
    {
        bonusinfotext.text = "<u>Improved weak spot expose</u>\n<size=70%><color=yellow>(" + Statics.charbasicskillpoints[currentchar] + "/" + Statics.firstbonuspointsneeded + ") Points Basic</color><size=100%>\n" +
                             "Enemy weak spot expose <color=green>+3</color> seconds.\n" +
                             "\n<u>Improved neutral attack</u>\n<size=70%><color=yellow>(" + Statics.charbasicskillpoints[currentchar] + "/" + Statics.secondbonuspointsneeded + ") Points Basic</color><size=100%>\n" +
                             "While the weak spot expose of the enemy refreshes your neutral attack (<color=green>100</color>% dmg attack of your chain attack) will deal <color=green>150</color>% damage.";

    }
    /*bonusinfotext.text = "<size=80%><color=green>" + Statics.firstbonuspointsneeded + "</color> Points Health/Defense<size=100%>\nBonus damage after heal:\n" +
                             "Casting a heal increases the damage of your next attack.\n" +
                             "\n<size=80%><color=green>" + Statics.secondbonuspointsneeded + "</color> Points Health/Defense<size=100%>\nHeal over time:\n" +
                             "While in combat every<color=green> " + Statics.bonushealtimer + "</color> seconds the character with lowest HP regenerates" + Statics.bonushealpercentage + "% of there max HP.";*/

}


/*if (skillpoints >= Statics.firstbonuspointsneeded) bonusinfotext.text += "<color=yellow>";
else bonusinfotext.text += "<color=red>";
bonusinfotext.text += Statics.firstbonuspointsneeded + " Points Health/Defense</color><size=100%>\n" +
                     "Casting a heal increases the damage of your next attack(<color=green>+" + bonusdmgafterheal + "</color>).\n" +
                     "\n<u>Heal over time</u>\n<size=70%>";
if (skillpoints >= Statics.secondbonuspointsneeded) bonusinfotext.text += "<color=yellow>";
else bonusinfotext.text += "<color=red>";
bonusinfotext.text += Statics.secondbonuspointsneeded + " Points Health/Defense</color><size=100%>\n" +
                     "While in combat every<color=green> " + Statics.bonushealtimer + "</color> seconds the character with lowest HP regenerates <color=green>" + Statics.bonushealpercentage + "</color>% of there max HP.";*/
