using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycalculatedmg
{
    public EnemyHP enemyscript;
    public void downdmg(float dmg)
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
    public void middmg(float dmg)
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
    public void updmg(float dmg)
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
        enemyscript.finaldmg = Mathf.Round(dmg * ((85 + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basicattributedmgbuff) / 100));
    }
    private void weakpointcalculation(float dmg)
    {
        if (enemyscript.enemyincreasebasicdmg == true)
        {
            enemyscript.finaldmg = Mathf.Round(dmg * ((150 + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basicattributedmgbuff) / 100));
            //LoadCharmanager.Overallmainchar.GetComponent<Movescript>().attackcombochain = 1;
        }
        else
        {
            enemyscript.finaldmg = Mathf.Round(dmg * ((50 + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basicattributedmgbuff) / 100));
        }
    }
}
