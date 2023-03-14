using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globalplayercalculations
{
    public static float calculateplayerdmgdone(float basicdmg, float attackdmg, float weapondmg, float stoneclassbonus)
    {
        float dmg = Mathf.Round(basicdmg + attackdmg + weapondmg);
        dmg += Mathf.Round((Statics.groupstonedmgbonus + stoneclassbonus) * 0.01f * dmg);
        return dmg;
    }
    public static float calculatesupportdmg(float basicdmg, float attackdmg, float weapondmg, float stoneclassbonus)
    {
        float dmg = Mathf.Round(basicdmg + attackdmg + weapondmg);
        dmg += Mathf.Round((Statics.groupstonedmgbonus + stoneclassbonus) * 0.01f * dmg);
        dmg = Mathf.Round(dmg * 0.33f);      //durch 3 teilen sonst zu viel dmg
        return dmg;
    }
    public static float calculateweaponcharbuff(float dmg)
    {
        float enddmg = (Statics.weaponswitchbuff + Statics.characterswitchbuff) / 100 * dmg;
        return enddmg;
    }
    public static float calculateweaponheal(float health)
    {
        float healing = Mathf.Round(Statics.basicweaponheal + (health * Statics.weaponbonushealhealthpercentage * 0.01f));
        return healing;
    }
    public static float calculatecasthealing(float basicheal, float health, float playerstonebonus)         //player casts und support healingpotion
    {
        float healthbonusheal = health * Statics.healhealthbonuspercentage * 0.01f;
        float healing = Mathf.Round(basicheal + healthbonusheal + (Statics.groupstonehealbonus + playerstonebonus) * 0.01f * basicheal);
        return healing;
    }
    public static float calculateenemyspezialdmg()
    {
        float dmg = Statics.charcurrentlvl;
        return dmg;
    }
    public static float calculateenemydmg(float basedmg, int enemylvl)
    {
        float dmg;
        dmg = basedmg + (enemylvl * Statics.enemydmgmultiplier);
        /*if(enemylvl >= 6)
        {
            dmg = basedmg + (enemylvl * Statics.enemydmgmultiplier);
        }
        else dmg = basedmg + enemylvl;*/
        return dmg;
    }
}