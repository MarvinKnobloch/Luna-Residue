using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Equiparmor : MonoBehaviour
{
    public Chooseitem chooseitem;
    private void noitemequipedbefore()
    {
        Statics.charmaxhealth[chooseitem.selectedchar] += chooseitem.itemvalues.stats[0];
        Statics.chardefense[chooseitem.selectedchar] += chooseitem.itemvalues.stats[1];
        Statics.charattack[chooseitem.selectedchar] += chooseitem.itemvalues.stats[2];
        Statics.charcritchance[chooseitem.selectedchar] += chooseitem.itemvalues.stats[3];
        Statics.charcritdmg[chooseitem.selectedchar] += chooseitem.itemvalues.stats[4];
        Statics.charweaponbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[5];
        Statics.charswitchbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[6];
        Statics.charbasicdmgbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[7];
    }
    public void sethead()
    {
        if (Statics.charcurrenthead[chooseitem.selectedchar] != null)
        {
            Statics.charmaxhealth[chooseitem.selectedchar] += chooseitem.itemvalues.stats[0] - Statics.charcurrenthead[chooseitem.selectedchar].stats[0];
            Statics.chardefense[chooseitem.selectedchar] += chooseitem.itemvalues.stats[1] - Statics.charcurrenthead[chooseitem.selectedchar].stats[1];
            Statics.charattack[chooseitem.selectedchar] += chooseitem.itemvalues.stats[2] - Statics.charcurrenthead[chooseitem.selectedchar].stats[2];
            Statics.charcritchance[chooseitem.selectedchar] += chooseitem.itemvalues.stats[3] - Statics.charcurrenthead[chooseitem.selectedchar].stats[3];
            Statics.charcritdmg[chooseitem.selectedchar] += chooseitem.itemvalues.stats[4] - Statics.charcurrenthead[chooseitem.selectedchar].stats[4];
            Statics.charweaponbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[5] - Statics.charcurrenthead[chooseitem.selectedchar].stats[5];
            Statics.charswitchbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[6] - Statics.charcurrenthead[chooseitem.selectedchar].stats[6];
            Statics.charbasicdmgbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[7] - Statics.charcurrenthead[chooseitem.selectedchar].stats[7];
        }
        else
        {
            noitemequipedbefore();
        }
        Statics.charcurrenthead[chooseitem.selectedchar] = chooseitem.itemvalues;
    }
    public void setchest()
    {
        if (Statics.charcurrentchest[chooseitem.selectedchar] != null)
        {
            Statics.charmaxhealth[chooseitem.selectedchar] += chooseitem.itemvalues.stats[0] - Statics.charcurrentchest[chooseitem.selectedchar].stats[0];
            Statics.chardefense[chooseitem.selectedchar] += chooseitem.itemvalues.stats[1] - Statics.charcurrentchest[chooseitem.selectedchar].stats[1];
            Statics.charattack[chooseitem.selectedchar] += chooseitem.itemvalues.stats[2] - Statics.charcurrentchest[chooseitem.selectedchar].stats[2];
            Statics.charcritchance[chooseitem.selectedchar] += chooseitem.itemvalues.stats[3] - Statics.charcurrentchest[chooseitem.selectedchar].stats[3];
            Statics.charcritdmg[chooseitem.selectedchar] += chooseitem.itemvalues.stats[4] - Statics.charcurrentchest[chooseitem.selectedchar].stats[4];
            Statics.charweaponbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[5] - Statics.charcurrentchest[chooseitem.selectedchar].stats[5];
            Statics.charswitchbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[6] - Statics.charcurrentchest[chooseitem.selectedchar].stats[6];
            Statics.charbasicdmgbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[7] - Statics.charcurrentchest[chooseitem.selectedchar].stats[7];
        }
        else
        {
            noitemequipedbefore();
        }
        Statics.charcurrentchest[chooseitem.selectedchar] = chooseitem.itemvalues;
    }
    public void setgloves()
    {
        if (Statics.charcurrentgloves[chooseitem.selectedchar] != null)
        {
            Statics.charmaxhealth[chooseitem.selectedchar] += chooseitem.itemvalues.stats[0] - Statics.charcurrentgloves[chooseitem.selectedchar].stats[0];
            Statics.chardefense[chooseitem.selectedchar] += chooseitem.itemvalues.stats[1] - Statics.charcurrentgloves[chooseitem.selectedchar].stats[1];
            Statics.charattack[chooseitem.selectedchar] += chooseitem.itemvalues.stats[2] - Statics.charcurrentgloves[chooseitem.selectedchar].stats[2];
            Statics.charcritchance[chooseitem.selectedchar] += chooseitem.itemvalues.stats[3] - Statics.charcurrentgloves[chooseitem.selectedchar].stats[3];
            Statics.charcritdmg[chooseitem.selectedchar] += chooseitem.itemvalues.stats[4] - Statics.charcurrentgloves[chooseitem.selectedchar].stats[4];
            Statics.charweaponbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[5] - Statics.charcurrentgloves[chooseitem.selectedchar].stats[5];
            Statics.charswitchbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[6] - Statics.charcurrentgloves[chooseitem.selectedchar].stats[6];
            Statics.charbasicdmgbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[7] - Statics.charcurrentgloves[chooseitem.selectedchar].stats[7];
        }
        else
        {
            noitemequipedbefore();
        }
        Statics.charcurrentgloves[chooseitem.selectedchar] = chooseitem.itemvalues;
    }
    public void setlegs()
    {
        if (Statics.charcurrentlegs[chooseitem.selectedchar] != null)
        {
            Statics.charmaxhealth[chooseitem.selectedchar] += chooseitem.itemvalues.stats[0] - Statics.charcurrentlegs[chooseitem.selectedchar].stats[0];
            Statics.chardefense[chooseitem.selectedchar] += chooseitem.itemvalues.stats[1] - Statics.charcurrentlegs[chooseitem.selectedchar].stats[1];
            Statics.charattack[chooseitem.selectedchar] += chooseitem.itemvalues.stats[2] - Statics.charcurrentlegs[chooseitem.selectedchar].stats[2];
            Statics.charcritchance[chooseitem.selectedchar] += chooseitem.itemvalues.stats[3] - Statics.charcurrentlegs[chooseitem.selectedchar].stats[3];
            Statics.charcritdmg[chooseitem.selectedchar] += chooseitem.itemvalues.stats[4] - Statics.charcurrentlegs[chooseitem.selectedchar].stats[4];
            Statics.charweaponbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[5] - Statics.charcurrentlegs[chooseitem.selectedchar].stats[5];
            Statics.charswitchbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[6] - Statics.charcurrentlegs[chooseitem.selectedchar].stats[6];
            Statics.charbasicdmgbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[7] - Statics.charcurrentlegs[chooseitem.selectedchar].stats[7];
        }
        else
        {
            noitemequipedbefore();
        }
        Statics.charcurrentlegs[chooseitem.selectedchar] = chooseitem.itemvalues;
    }
    public void setshoes()
    {
        if (Statics.charcurrentshoes[chooseitem.selectedchar] != null)
        {
            Statics.charmaxhealth[chooseitem.selectedchar] += chooseitem.itemvalues.stats[0] - Statics.charcurrentshoes[chooseitem.selectedchar].stats[0];
            Statics.chardefense[chooseitem.selectedchar] += chooseitem.itemvalues.stats[1] - Statics.charcurrentshoes[chooseitem.selectedchar].stats[1];
            Statics.charattack[chooseitem.selectedchar] += chooseitem.itemvalues.stats[2] - Statics.charcurrentshoes[chooseitem.selectedchar].stats[2];
            Statics.charcritchance[chooseitem.selectedchar] += chooseitem.itemvalues.stats[3] - Statics.charcurrentshoes[chooseitem.selectedchar].stats[3];
            Statics.charcritdmg[chooseitem.selectedchar] += chooseitem.itemvalues.stats[4] - Statics.charcurrentshoes[chooseitem.selectedchar].stats[4];
            Statics.charweaponbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[5] - Statics.charcurrentshoes[chooseitem.selectedchar].stats[5];
            Statics.charswitchbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[6] - Statics.charcurrentshoes[chooseitem.selectedchar].stats[6];
            Statics.charbasicdmgbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[7] - Statics.charcurrentshoes[chooseitem.selectedchar].stats[7];
        }
        else
        {
            noitemequipedbefore();
        }
        Statics.charcurrentshoes[chooseitem.selectedchar] = chooseitem.itemvalues;
    }
    public void setneckless()
    {
        if (Statics.charcurrentneckless[chooseitem.selectedchar] != null)
        {
            Statics.charmaxhealth[chooseitem.selectedchar] += chooseitem.itemvalues.stats[0] - Statics.charcurrentneckless[chooseitem.selectedchar].stats[0];
            Statics.chardefense[chooseitem.selectedchar] += chooseitem.itemvalues.stats[1] - Statics.charcurrentneckless[chooseitem.selectedchar].stats[1];
            Statics.charattack[chooseitem.selectedchar] += chooseitem.itemvalues.stats[2] - Statics.charcurrentneckless[chooseitem.selectedchar].stats[2];
            Statics.charcritchance[chooseitem.selectedchar] += chooseitem.itemvalues.stats[3] - Statics.charcurrentneckless[chooseitem.selectedchar].stats[3];
            Statics.charcritdmg[chooseitem.selectedchar] += chooseitem.itemvalues.stats[4] - Statics.charcurrentneckless[chooseitem.selectedchar].stats[4];
            Statics.charweaponbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[5] - Statics.charcurrentneckless[chooseitem.selectedchar].stats[5];
            Statics.charswitchbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[6] - Statics.charcurrentneckless[chooseitem.selectedchar].stats[6];
            Statics.charbasicdmgbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[7] - Statics.charcurrentneckless[chooseitem.selectedchar].stats[7];
        }
        else
        {
            noitemequipedbefore();
        }
        Statics.charcurrentneckless[chooseitem.selectedchar] = chooseitem.itemvalues;
    }
    public void setring()
    {
        if (Statics.charcurrentring[chooseitem.selectedchar] != null)
        {
            Statics.charmaxhealth[chooseitem.selectedchar] += chooseitem.itemvalues.stats[0] - Statics.charcurrentring[chooseitem.selectedchar].stats[0];
            Statics.chardefense[chooseitem.selectedchar] += chooseitem.itemvalues.stats[1] - Statics.charcurrentring[chooseitem.selectedchar].stats[1];
            Statics.charattack[chooseitem.selectedchar] += chooseitem.itemvalues.stats[2] - Statics.charcurrentring[chooseitem.selectedchar].stats[2];
            Statics.charcritchance[chooseitem.selectedchar] += chooseitem.itemvalues.stats[3] - Statics.charcurrentring[chooseitem.selectedchar].stats[3];
            Statics.charcritdmg[chooseitem.selectedchar] += chooseitem.itemvalues.stats[4] - Statics.charcurrentring[chooseitem.selectedchar].stats[4];
            Statics.charweaponbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[5] - Statics.charcurrentring[chooseitem.selectedchar].stats[5];
            Statics.charswitchbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[6] - Statics.charcurrentring[chooseitem.selectedchar].stats[6];
            Statics.charbasicdmgbuff[chooseitem.selectedchar] += chooseitem.itemvalues.stats[7] - Statics.charcurrentring[chooseitem.selectedchar].stats[7];
        }
        else
        {
            noitemequipedbefore();
        }
        Statics.charcurrentring[chooseitem.selectedchar] = chooseitem.itemvalues;
    }
}
