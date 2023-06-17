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

    private bool effectisactive;
    private float starteffectrange = 4;
    private float disableeffectrange = 2;

    private float overalldmg;
    private float overallcritchance;
    private float critdmg;
    private bool crit;

    private float switchbuffdmg;

    private float healreduction;
    private float enemydebuffcrit;

    public void setarrowvalues(float dmg, int type, float reducehealing)
    {
        dmgtype = type;
        Attributecontroller atb = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>();
        overalldmg = Globalplayercalculations.calculateplayerdmgdone(dmg, atb.attack, atb.bowattack, atb.stoneclassbonusdmg);
        switchbuffdmg = Globalplayercalculations.calculateweaponcharbuff(overalldmg);
        overallcritchance = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critchance;

        healreduction = reducehealing;
    }

    private void Start()
    {
        if (Vector3.Distance(transform.position, arrowhitpoint) > starteffectrange)
        {
            effectisactive = true;
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else effectisactive = false;
    }
    void Update()
    {
        if (arrowtarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, arrowhitpoint, arrowspeed * Time.deltaTime);
            if(effectisactive == true)
            {
                if (Vector3.Distance(transform.position, arrowhitpoint) < disableeffectrange)
                {
                    effectisactive = false;
                    transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else
            {
                if (dmgonce == false && Vector3.Distance(transform.position, arrowhitpoint) < 0.1f)
                {
                    Checkhitboxbasic();
                    dmgonce = true;
                    StartCoroutine(destroyarrow());
                }
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
                if (UnityEngine.Random.Range(0, 100) < overallcritchance + enemydebuffcrit + Statics.bonusnoncrit)
                {
                    crit = true;
                    critdmg = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critdmg;
                    overalldmg = Globalplayercalculations.calculatecritdmg(overalldmg, overallcritchance, critdmg, switchbuffdmg, true);
                    enemyscript.takeplayerdamage(overalldmg, dmgtype, crit);
                }
                else
                {
                    crit = false;
                    overalldmg = Globalplayercalculations.calculatenoncritdmg(overalldmg, switchbuffdmg, true);
                    enemyscript.takeplayerdamage(overalldmg, dmgtype , crit);
                }
            }
            if (dmgtype == 0)
            {
                Manamanager.manamanager.Managemana(Statics.bowbasicmanarestore);
            }
            else
            {
                Manamanager.manamanager.Managemana(Statics.bowendmanarestore);
                if (LoadCharmanager.Overallmainchar.gameObject.TryGetComponent(out Playerhp playerhp))
                {
                    float healing = Globalplayercalculations.calculateweaponheal() * healreduction;
                    playerhp.addhealth(healing);
                }
            }
        }
    }
}
