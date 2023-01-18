using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Airdownarrow : MonoBehaviour
{
    /*public float arrowspeed;
    public float timetodestroy;
    public bool dmgonce;
    public float basicdmg;
    public GameObject damagetext;
    public Transform Arrowtarget;
    private float endheal = 7f;
    private float enddmg;

    private bool crit;

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
        if (Arrowtarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Arrowtarget.position, arrowspeed * Time.deltaTime);
            if (dmgonce == false && Vector3.Distance(transform.position, Arrowtarget.position) < 0.1f)
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
            if (Arrowtarget.TryGetComponent(out EnemyHP enemyscript))
            {
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
                    if (enemyscript.enemyincreasebasicdmg == true)
                    {
                        enddmg = Mathf.Round(calculatedmg * LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basicattributedmgbuff / 100);
                        LoadCharmanager.Overallmainchar.GetComponent<Bowattack>().combochain--;
                        enemyscript.TakeDamage(enddmg);
                    }
                    else
                    {
                        enddmg = Mathf.Round(calculatedmg * 50 / 100);
                        enemyscript.TakeDamage(enddmg);
                    }
                }
                else if (enemyscript.sizeofenemy == 1)
                {
                    if (enemyscript.enemydebuffcd == false)
                    {
                        enemyscript.enemydebuffstart();
                        LoadCharmanager.Overallmainchar.GetComponent<Bowattack>().combochain--;
                    }
                    enddmg = Mathf.Round(calculatedmg * 85 / 100);
                    enemyscript.TakeDamage(enddmg);
                }
                else if (enemyscript.sizeofenemy == 2)
                {
                    enddmg = Mathf.Round(calculatedmg);
                    enemyscript.TakeDamage(enddmg);
                }
                var showtext = Instantiate(damagetext, Arrowtarget.transform.position, Quaternion.identity);
                showtext.GetComponent<TextMeshPro>().text = enddmg.ToString();
                if (crit == true)
                {
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
                Manamanager.manamanager.Managemana(Statics.bowendmanarestore);
                LoadCharmanager.Overallmainchar.gameObject.GetComponent<SpielerHP>().playerheal(endheal);
            }

        }
    }*/
}
