using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Singlearrow : MonoBehaviour
{
    [SerializeField] private float arrowspeed;
    [SerializeField] private float timetodestroyafterhit;
    private bool dmgonce;
    [NonSerialized] public Vector3 arrowhitpoint;
    [NonSerialized] public GameObject arrowtarget;
    private int dmgtype;

    private float overalldmg;
    private float overallcritchance;
    private float overallcritdmg;
    private bool crit;

    private float enemydebuffcrit;

    public void setarrowvalues(float dmg, int type)
    {
        dmgtype = type;
        Attributecontroller atb = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>();
        overalldmg = Globalplayercalculations.calculateplayerdmgdone(dmg, atb.attack, atb.bowattack, atb.stoneclassbonusdmg);
        float switchbuffdmg = Globalplayercalculations.calculateweaponcharbuff(dmg);
        overalldmg = Mathf.Round(overalldmg + switchbuffdmg);
        overallcritchance = Statics.playerbasiccritchance + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critchance;
        overallcritdmg = Mathf.Round(overalldmg * (LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critdmg / 100f) + switchbuffdmg);
    }

    void Update()
    {
        if (arrowtarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, arrowhitpoint, arrowspeed * Time.deltaTime);
            if (dmgonce == false && Vector3.Distance(transform.position, arrowhitpoint) < 0.1f)
            {
                Checkhitboxbasic();
                dmgonce = true;
                StartCoroutine(destroyarrow());
            }
        }
    }
    IEnumerator destroyarrow()
    {
        yield return new WaitForSeconds(timetodestroyafterhit);
        Destroy(gameObject);
    }
    private void Checkhitboxbasic()
    {
        if (arrowtarget != null)
        {
            Weaponsounds.instance.playarrowimpact();
            if (arrowtarget.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.tookdmgfrom(1, Statics.playertookdmgfromamount);
                if (enemyscript.enemydebuffcd == true)
                {
                    enemydebuffcrit = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basicattributecritbuff;
                }
                else
                {
                    enemydebuffcrit = 0;
                }
                if (UnityEngine.Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
                {
                    crit = true;
                    enemyscript.takeplayerdamage(overallcritdmg, dmgtype ,crit);
                }
                else
                {
                    crit = false;
                    enemyscript.takeplayerdamage(overalldmg, dmgtype , crit);
                }
            }
            if(dmgtype == 0)
            {
                Manamanager.manamanager.Managemana(Statics.bowbasicmanarestore);
            }
            else
            {
                Manamanager.manamanager.Managemana(Statics.bowendmanarestore);
                if (LoadCharmanager.Overallmainchar.gameObject.TryGetComponent(out Playerhp playerhp))
                {
                    float healing = Globalplayercalculations.calculateweaponheal(playerhp.maxhealth);
                    playerhp.addhealth(healing);
                }
            }
        }
    }
}
