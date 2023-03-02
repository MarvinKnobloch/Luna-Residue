using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globalplayercalculations
{
    public static float calculateplayerdmgdone(float basicdmg, float attackdmg, float weapondmg, float stoneclassbonus)
    {
        float dmg = basicdmg + attackdmg + weapondmg;
        dmg += Mathf.Round((Statics.groupstonedmgbonus + stoneclassbonus) * 0.01f * dmg);
        return dmg;
    }
    public static float calculateweaponheal(float health)
    {
        float healing = Statics.basicweaponheal + (health * Statics.weaponbonushealhealthpercentage * 0.01f);
        return healing;
    }
}
