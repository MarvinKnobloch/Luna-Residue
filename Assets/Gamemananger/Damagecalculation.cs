using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagecalculation
{
    public static float calculateplayerdmgdone(float basicdmg, float attackdmg, float weapondmg, float stoneclassbonus)
    {
        float dmg = basicdmg + attackdmg + weapondmg;
        dmg += Mathf.Round((Statics.groupstonedmgbonus + stoneclassbonus) * 0.01f * dmg);
        return dmg;
    }
}
