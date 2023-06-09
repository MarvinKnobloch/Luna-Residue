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
        dmg = Mathf.Round(dmg * 0.25f);      //durch 3 teilen sonst zu viel dmg
        return dmg;
    }
    public static float calculatenoncritdmg(float dmg, float switchbuffdmg, bool maintarget)
    {
        if(Statics.bonuscritdmg == true && maintarget == true) Statics.bonusnoncrit += Statics.bonuscritfromnoncrit;
        return Mathf.Round(dmg + switchbuffdmg);
    }

    public static float calculatecritdmg(float dmg, float critchance, float critdmg, float switchbuffdmg, bool maintarget)
    {
        if(maintarget == true) Statics.bonusnoncrit = 0;
        float finaldmg;
        if (Statics.bonuscritdmg == true)
        {
            if (Random.Range(0, 100) < (100 - critchance) * Statics.bonuscritchancemultipler)
            { 
                finaldmg = Mathf.Round(dmg * ((critdmg + critdmg - 150) / 100f) + switchbuffdmg); 
            }
            else finaldmg = Mathf.Round(dmg * (critdmg / 100f) + switchbuffdmg);
        }
        else finaldmg = Mathf.Round(dmg * (critdmg / 100f) + switchbuffdmg);
        return finaldmg;
    }
    public static float calculateweaponcharbuff(float dmg)
    {
        float enddmg = (Statics.weaponswitchbuff + Statics.characterswitchbuff) / 100 * dmg;
        return enddmg;
    }
    public static float calculateweaponheal()
    {
        float healing = 0;//Mathf.Round(Statics.basicweaponheal + (Statics.charcurrentlvl / 3));
        return healing;
    }
    public static float calculatecasthealing(float basicheal, float health, float playerstonebonus)         //player casts und support healingpotion
    {
        float healthbonusheal = health * Statics.healhealthbonuspercentage * 0.01f;
        float healing = Mathf.Round(basicheal + healthbonusheal + (Statics.groupstonehealbonus + playerstonebonus) * 0.01f * basicheal);
        return healing;
    }

    public static float dashexplosion(float critdmg, float critchance)
    {
        float critdmgvalue = (critdmg - 150) * 0.5f;
        float dmg = (critchance + critdmgvalue) * 0.8f;
        return Mathf.Round(dmg + ((critchance + critdmgvalue) * 0.01f * dmg));
    }
    public static float charexplison(int charnumber)
    {
        float explosiondmg = 20 + Statics.charweaponbuff[charnumber] + Statics.charswitchbuff[charnumber];
        return Mathf.Round(explosiondmg + ((Statics.charweaponbuff[charnumber] + Statics.charswitchbuff[charnumber]) * 0.01f * explosiondmg));
    }
    public static float calculateenemyspezialdmg(float basedmg, float dmglevel, int amountreduction)
    {
        float dmg = dmglevel * Statics.enemyspezialdmgbonus / amountreduction + (basedmg - Statics.enemydifficultyminusdmg);
        return dmg;
    }
    public static float calculateenemydmg(float basedmg, int enemylvl)
    {
        float dmg;
        dmg = Mathf.Round(basedmg - Statics.enemydifficultyminusdmg + (enemylvl * Statics.enemydmgmultiplier));
        return dmg;
    }
    public static void addplayerhealth(GameObject player, float amount, bool withtext)
    {
        if (player != null)
        {
            if (player.TryGetComponent(out Playerhp playerhp))
            {
                if (withtext == true) playerhp.addhealthwithtext(amount);
                else playerhp.addhealth(amount);
            }
        }
    }
}

/*public static float calculateenemyspezialdmgtest(float dmglevel)
{
    float percentage = Statics.enemyspezialdmgbonus + (dmglevel - Statics.charcurrentlvl);
    if (percentage < 0) percentage = 0;
    float finalpercentage = percentage * 0.01f;
    float dmg = Mathf.Round(LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().maxhealth * finalpercentage);
    return dmg;
}*/