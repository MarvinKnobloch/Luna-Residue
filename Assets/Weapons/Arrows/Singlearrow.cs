using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singlearrow : MonoBehaviour
{
    public float arrowspeed;
    public float timetodestroy;
    private bool dmgonce;
    public Vector3 arrowhitpoint;
    public GameObject arrowtarget;
    private int dmgtype;

    private float overalldmg;
    private float overallcritchance;
    private float overallcritdmg;
    private bool crit;

    private float enemydebuffcrit;

    void Start()
    {
        Destroy(gameObject, timetodestroy);   
    }
    public void setarrowvalues(float dmg, int type)
    {
        dmgtype = type;
        Attributecontroller atb = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>();
        overalldmg = Damagecalculation.calculateplayerdmgdone(dmg, atb.attack, atb.bowattack, atb.stoneclassbonusdmg);
        overalldmg = Mathf.Round(overalldmg * ((Statics.weaponswitchbuff + Statics.characterswitchbuff - 100f) / 100));
        overallcritchance = Statics.playerbasiccritchance + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critchance;
        overallcritdmg = Mathf.Round(overalldmg * (LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.characterswitchbuff - 100f) / 100));
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
            }
        }
    }
    private void Checkhitboxbasic()
    {
        if (arrowtarget != null)
        {
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
                if (Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
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
                LoadCharmanager.Overallmainchar.gameObject.GetComponent<Playerhp>().playerheal(7);
            }
        }
    }
}
