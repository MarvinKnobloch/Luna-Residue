using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleAirarrow : MonoBehaviour
{
    /*public float arrowspeed;
    public float timetodestroy;
    public Vector3 arrowziel { get; set; }
    public bool hit { get; set; }
    public bool dmgonce;
    public float basicdmg;
    public GameObject damagetext;
    public Transform Arrowtarget;

    private float enemydebuffcrit;

    private float overalldmg;
    private float overallcritchance;
    private float overallcritdmg;



    void Awake()
    {
        Destroy(gameObject, timetodestroy);
        dmgonce = false;

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
            arrowdealdmg();
            dmgonce = true;
        }
    }
    private void arrowdealdmg()
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
                    enemyscript.TakeDamage(overallcritdmg);
                    var showtext = Instantiate(damagetext, Arrowtarget.transform.position, Quaternion.identity);
                    showtext.GetComponent<TextMeshPro>().text = overallcritdmg.ToString();
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
                else
                {
                    enemyscript.TakeDamage(overalldmg);
                    var showtext = Instantiate(damagetext, Arrowtarget.transform.position, Quaternion.identity);
                    showtext.GetComponent<TextMeshPro>().text = overalldmg.ToString();
                }
                Manamanager.manamanager.Managemana(Statics.bowbasicmanarestore);
            }
        }
    }*/
}
