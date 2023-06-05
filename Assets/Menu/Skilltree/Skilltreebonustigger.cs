using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Skilltreebonustigger : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] private GameObject bonusinfoimage;
    private TextMeshProUGUI attributeinfotext;
    [SerializeField] private TextMeshProUGUI bonusinfotext;
    [SerializeField] private int attributenumber;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(PlayerPrefs.GetFloat("Skilltreeinfo") == 0)
        {
            bonusinfoimage.SetActive(true);
            if (attributenumber == 1) sethealthtext();
            else if (attributenumber == 2) setcrittext();
            else if (attributenumber == 3) setswitchtext();
            else if (attributenumber == 4) setbasictext();
            attributeinfotext.text = "Attribute Bonus Info";
        }
    }
    private void Awake()
    {
        attributeinfotext = GetComponentInParent<Skilltreebonus>().attributeinfotext;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bonusinfoimage.SetActive(false);
        attributeinfotext.text = "Attribute Bonus";
    }
    private void sethealthtext()
    {
        bonusinfotext.text = "<color=green>" + Statics.firstbonuspointsneeded + "</color> Points Health/Defense\nBonus damage after heal:\n" +
                             "Casting a heal increases the damage of your next attack.\n" +
                             "\n<color=green>" + Statics.secondbonuspointsneeded + "</color> Points Health/Defense\nHeal over time:\n" +
                             "While in combat every<color=green> " + Statics.bonushealtimer + "</color> seconds the character with lowest HP regenerates" + Statics.bonushealpercentage + "of there max HP.";
    }
    private void setcrittext()
    {
        bonusinfotext.text = "<color=green>" + Statics.firstbonuspointsneeded + "</color> Points Critical Chance/Damage\nDash Explosion:\n" +
                             "Each critcal hit gains 1 stack. At " + Statics.bonuscritstacksneeded + " stacks your next dash will cause an explosion that deals damage.\n" +
                             "\n<color=green>" + Statics.secondbonuspointsneeded + "</color> Points Critical Chance/Damage\nCritcal damage bonus:\n" +
                             "Grants a chance that your bonus critcal damage is doubled.";
    }
    private void setswitchtext()
    {
        bonusinfotext.text = "<color=green>" + Statics.firstbonuspointsneeded + "</color> Points Weapon/Character Switch\nImproved character switch:\n" +
                             "Switching your character will cause an explosion that deals damage to enemies and heals your group.\n" +
                             "\n<color=green>" + Statics.secondbonuspointsneeded + "</color> Points Weapon/Character Switch\nImproved weapon switch:\n" +
                             "Your weapon switch attack deals more damage.";
    }
    private void setbasictext()
    {
        bonusinfotext.text = "<color=green>" + Statics.firstbonuspointsneeded + "</color> Points Basic\nImproved weak spot expose:\n" +
                             "Enemy weak spot expose +3 seconds.\n" +
                             "\n<color=green>" + Statics.secondbonuspointsneeded + "</color> Points Basic\nImproved neutral attack:\n" +
                             "While the weak spot expose of the enemy refreshes your neutral attack (<color=green>100</color>% dmg attack of your chain attack) will deal <color=green>150</color>% damage.";

    }

}
