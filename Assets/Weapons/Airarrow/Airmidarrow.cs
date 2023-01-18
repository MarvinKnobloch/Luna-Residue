using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Airmidarrow : MonoBehaviour
{
    public Vector3 arrowziel { get; set; }
    public bool hit { get; set; }

    public float arrowspeed;
    public float timetodestroy;
    public bool dmgonce;
    public float basicdmg;
    public GameObject damagetext;
    public Transform Arrowtarget;
    public LayerMask Layerhitbox;
    private float endheal = 7f;
    private float enddmg;

    private bool crit;
    private bool resetcombochain;

    private float enemydebuffcrit;

    private float overalldmg;
    private float overallcritchance;
    private float overallcritdmg;

    void Start()
    {
        Destroy(gameObject, timetodestroy);

        overalldmg = Mathf.Round((basicdmg + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().attack + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().bowattack) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
        overallcritchance = Statics.playerbasiccritchance + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critchance;
        overallcritdmg = Mathf.Round(overalldmg * (LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, arrowziel, arrowspeed * Time.deltaTime);
        if (hit == false && Vector3.Distance(transform.position, arrowziel) < 0.1f)
        {
            Destroy(gameObject);
        }
        if (dmgonce == false && hit == true && Vector3.Distance(transform.position, arrowziel) < 0.1f)
        {
            //this.transform.parent = Arrowtarget.transform;
            //Checkhitbox();
            dmgonce = true;
        }
    }
}
    /*private void Checkhitbox()
    {
        if (Arrowtarget != null)
        {
            Collider[] cols = Physics.OverlapSphere(Arrowtarget.transform.position, 3f, Layerhitbox);
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
                        float calculatedmg;
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
                            calculatedmg = overallcritdmg;
                        }
                        else
                        {
                            crit = false;
                            calculatedmg = overalldmg;
                        }
                        if (enemyscript.sizeofenemy == 0)
                        {
                            enddmg = Mathf.Round(calculatedmg);
                            enemyscript.TakeDamage(enddmg);
                        }
                        else if (enemyscript.sizeofenemy == 1)
                        {
                            if (enemyscript.enemyincreasebasicdmg == true)
                            {
                                enddmg = Mathf.Round(calculatedmg * LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basicattributedmgbuff / 100);
                                resetcombochain = true;
                                enemyscript.TakeDamage(enddmg);
                            }
                            else
                            {
                                enddmg = Mathf.Round(calculatedmg * 50 / 100);
                                enemyscript.TakeDamage(enddmg);
                            }
                        }
                        else if (enemyscript.sizeofenemy == 2)
                        {
                            if (enemyscript.enemydebuffcd == false)
                            {
                                enemyscript.enemydebuffstart();
                                resetcombochain = true;
                            }
                            enddmg = Mathf.Round(calculatedmg * 85 / 100);
                            enemyscript.TakeDamage(enddmg);
                        }
                        var showtext = Instantiate(damagetext, Arrowtarget.transform.position, Quaternion.identity);
                        showtext.GetComponent<TextMeshPro>().text = enddmg.ToString();
                        if (crit == true)
                        {
                            showtext.GetComponent<TextMeshPro>().color = Color.red;
                        }
                    }
                }
            }
            if (resetcombochain == true)
            {
                resetcombochain = false;
                LoadCharmanager.Overallmainchar.GetComponent<Bowattack>().combochain--;
            }
            if (cols.Length > 0)
            {
                Manamanager.manamanager.Managemana(Statics.bowendmanarestore);
                LoadCharmanager.Overallmainchar.gameObject.GetComponent<SpielerHP>().playerheal(endheal);
            }
        }
    }
}*/

/*if (Arrowtarget != null)
{
    Collider[] cols = Physics.OverlapSphere(Arrowtarget.transform.position, 5f, Layerhitbox);

    foreach (Collider Enemyhit in cols)
        if (Enemyhit.gameObject.GetComponentInChildren<EnemyHP>())
        {
            Enemyhit.gameObject.GetComponentInChildren<EnemyHP>().tookdmgfrom(1);
        }

    foreach (Collider Enemyhit in cols)*/


