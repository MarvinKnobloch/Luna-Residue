using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycalculatedmg
{
    public EnemyHP enemyscript;
    public void calculatedmg(float dmg, int type)
    {
        if (type == 0) normaldmg(dmg);
        else if (type == 1) downdmg(dmg);
        else if (type == 2) middmg(dmg);
        else if (type == 3) updmg(dmg);
    }
    private void normaldmg(float dmg)
    {
        enemyscript.finaldmg = dmg;
    }
    private void downdmg(float dmg)
    {
        if (enemyscript.sizeofenemy == 0)
        {
            weakpointcalculation(dmg);
        }
        else if (enemyscript.sizeofenemy == 1)
        {
            armorbreakcalculation(dmg);
        }
        else if (enemyscript.sizeofenemy == 2)
        {
            nodmgcalculation(dmg);
        }
    }
    private void middmg(float dmg)
    {
        if (enemyscript.sizeofenemy == 0)
        {
            nodmgcalculation(dmg);
        }
        else if (enemyscript.sizeofenemy == 1)
        {
            weakpointcalculation(dmg);
        }
        else if (enemyscript.sizeofenemy == 2)
        {
            armorbreakcalculation(dmg);
        }
    }
    private void updmg(float dmg)
    {
        if (enemyscript.sizeofenemy == 0)
        {
            armorbreakcalculation(dmg);
        }
        else if (enemyscript.sizeofenemy == 1)
        {
            nodmgcalculation(dmg);
        }
        else if (enemyscript.sizeofenemy == 2)
        {
            weakpointcalculation(dmg);
        }
    }
    private void nodmgcalculation(float dmg)
    {
        enemyscript.finaldmg = Mathf.Round(dmg);
    }
    private void armorbreakcalculation(float dmg)
    {
        if (enemyscript.enemydebuffcd == false)
        {
            enemyscript.enemydebuffstart();
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().attackcombochain = 0;
        }
        enemyscript.finaldmg = Mathf.Round(dmg * 85 / 100);
    }
    private void weakpointcalculation(float dmg)
    {
        if (enemyscript.enemyincreasebasicdmg == true)
        {
            enemyscript.finaldmg = Mathf.Round(dmg * (LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basicattributedmgbuff / 100));
        }
        else
        {
            enemyscript.finaldmg = Mathf.Round(dmg * 50 / 100);
        }
    }
}
