using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aoearrow : MonoBehaviour
{
    public float arrowspeed;
    public float timetodestroy;
    private bool dmgonce;
    public GameObject Arrowtarget;
    public LayerMask Layerhitbox;
    private float aoeradius;
    private int dmgtype;

    private bool resetcombochain;

    private float overalldmg;
    private float overallcritchance;
    private float overallcritdmg;
    private bool crit;

    private float enemydebuffcrit;

    void Start()
    {
        Destroy(gameObject, timetodestroy);
    }

    public void setarrowvalues(float dmg, float radius, int type)
    {
        aoeradius = radius;
        dmgtype = type;
        overalldmg = Mathf.Round((dmg + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().attack + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().bowattack) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
        overallcritchance = Statics.playerbasiccritchance + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critchance;
        overallcritdmg = Mathf.Round(overalldmg * (LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
    }

    void Update()
    {
        if (Arrowtarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Arrowtarget.transform.position + Vector3.up, arrowspeed * Time.deltaTime);
            if (dmgonce == false && Vector3.Distance(transform.position, Arrowtarget.transform.position + Vector3.up) < 0.1f)
            {
                Checkhitbox();
                dmgonce = true;
            }
        }
    }
    private void Checkhitbox()
    {
        if (Arrowtarget != null)
        {
            Collider[] cols = Physics.OverlapSphere(Arrowtarget.transform.position, aoeradius, Layerhitbox);
            foreach (Collider Enemyhit in cols)
            {
                if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                {
                    enemyscript.dmgonce = false;
                }
            }
            foreach (Collider Enemyhit in cols)
            {
                if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                {
                    if (enemyscript.dmgonce == false)
                    {
                        enemyscript.dmgonce = true;
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
                            enemyscript.takeplayerdamage(overallcritdmg, dmgtype, crit);
                        }
                        else
                        {
                            crit = false;
                            enemyscript.takeplayerdamage(overalldmg, dmgtype, crit);
                        }
                    }
                }
            }
            /*if (resetcombochain == true)
            {
                resetcombochain = false;
                LoadCharmanager.Overallmainchar.GetComponent<Bowattack>().combochain--;
            }*/
            if (cols.Length > 0)
            {
                if (dmgtype == 0)
                {
                    Manamanager.manamanager.Managemana(Statics.bowbasicmanarestore);
                }
                else
                {
                    Manamanager.manamanager.Managemana(Statics.bowendmanarestore);
                    LoadCharmanager.Overallmainchar.gameObject.GetComponent<SpielerHP>().playerheal(7);
                }
            }
        }
    }
}