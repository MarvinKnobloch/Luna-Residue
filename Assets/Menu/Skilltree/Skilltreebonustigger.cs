using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Skilltreebonustigger : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] private GameObject bonusinfoimage;
    [SerializeField] private TextMeshProUGUI bonusinfotext;
    [SerializeField] private int attributenumber;
    public void OnPointerEnter(PointerEventData eventData)
    {
        bonusinfoimage.SetActive(true);
        if (attributenumber == 1) sethealthtext();
        else if (attributenumber == 2) setcrittext();
        else if (attributenumber == 3) setswitchtext();
        else if (attributenumber == 4) setbasictext();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bonusinfoimage.SetActive(false);
    }
    private void sethealthtext()
    {
        bonusinfotext.text = "<color=green>" + Statics.firstbonuspointsneeded + "</color> Skill points in health and/or defense\n Bonus damage after heal: Casting a heal increases the damage of your next attack.\n" +
                             "\n<color=green>" + Statics.secondbonuspointsneeded + "</color> Skill points in health and/or defense\n Heal over time: While in combat every" +
                             Statics.bonushealtimer + "seconds the character with lowest HP regenerates" + Statics.bonushealpercentage + "of there max HP.";
    }
    private void setcrittext()
    {
        bonusinfotext.text = "<color=green>" + Statics.firstbonuspointsneeded + "</color> Skill points in critical chance/Damage\n Dash Explosion: Each critcal hit " +
                             "gains 1 stack. At " + Statics.bonuscritstacksneeded + " stacks your next dash will cause an explosion that deals damage.\n" +
                             "\n<color=green>" + Statics.secondbonuspointsneeded + "</color> Skill points in critical chance/Damage \n Critcal damage bonus: Grants a chance that your bonus critcal damage is doubled.";

    }
    private void setswitchtext()
    {

    }
    private void setbasictext()
    {

    }

}
