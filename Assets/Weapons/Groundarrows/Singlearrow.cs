using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Singlearrow : MonoBehaviour
{
    public float arrowspeed;
    public float timetodestroy;
    public bool dmgonce;
    public GameObject Arrowtarget;
    public int dmgtype;

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
        overalldmg = Mathf.Round((dmg + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().attack + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().bowattack) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
        overallcritchance = Statics.playerbasiccritchance + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critchance;
        overallcritdmg = Mathf.Round(overalldmg * (LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
    }

    void Update()
    {
        if (Arrowtarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Arrowtarget.transform.position, arrowspeed * Time.deltaTime);
            if (dmgonce == false && Vector3.Distance(transform.position, Arrowtarget.transform.position) < 0.1f)
            {
                Checkhitboxbasic();
                dmgonce = true;
            }
        }
    }
    private void Checkhitboxbasic()
    {
        if (Arrowtarget != null)
        {
            if (Arrowtarget.TryGetComponent(out EnemyHP enemyscript))
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
                    enemyscript.TakeDamage(overallcritdmg, dmgtype ,crit);
                }
                else
                {
                    crit = false;
                    enemyscript.TakeDamage(overalldmg, dmgtype , crit);
                }
            }
            if(dmgtype == 0)
            {
                Manamanager.manamanager.Managemana(Statics.bowbasicmanarestore);
            }
            else
            {
                Manamanager.manamanager.Managemana(Statics.bowendmanarestore);
                LoadCharmanager.Overallmainchar.gameObject.GetComponent<SpielerHP>().playerheal(7);
            }
            /*if (resetcombochain == true)
            {
                resetcombochain = false;
                LoadCharmanager.Overallmainchar.GetComponent<Bowattack>().combochain--;
            }*/
        }
    }
}
