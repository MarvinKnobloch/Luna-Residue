using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hookshotarrow : MonoBehaviour
{
    public float arrowspeed;
    public float timetodestroy;
    public bool dmgonce;
    public float basicdmg;
    public GameObject damagetext;
    public Transform Arrowtarget;

    private float hookduration = 1f;
    private float missinghooktime = 0f;

    private float enemydebuffcrit;

    private float basicdmgtodeal;
    private float overallcritchance;
    private float overallbasiccritdmg;

    void Start()
    {
        Destroy(gameObject, timetodestroy);

        basicdmgtodeal = Mathf.Round((basicdmg + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().attack + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().bowattack) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
        overallcritchance = Statics.playerbasiccritchance + LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critchance;
        overallbasiccritdmg = Mathf.Round(basicdmgtodeal * (LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
    }

    void Update()
    {
        if (Arrowtarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Arrowtarget.position, arrowspeed * Time.deltaTime);
            if (dmgonce == false && Vector3.Distance(transform.position, Arrowtarget.position) < 0.1f)
            {
                Checkhitboxbasic();
                hooktoenemy();
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
                    enemydebuffcrit = LoadCharmanager.Overallmainchar.GetComponent<Attributecontroller>().basiccrit;
                }
                else
                {
                    enemydebuffcrit = 0;
                }
                if (Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
                {
                    enemyscript.TakeDamage(overallbasiccritdmg);
                    var showtext = Instantiate(damagetext, Arrowtarget.transform.position, Quaternion.identity);
                    showtext.GetComponent<TextMeshPro>().text = overallbasiccritdmg.ToString();
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
                else
                {
                    enemyscript.TakeDamage(basicdmgtodeal);
                    var showtext = Instantiate(damagetext, Arrowtarget.transform.position, Quaternion.identity);
                    showtext.GetComponent<TextMeshPro>().text = basicdmgtodeal.ToString();
                }
                Manamanager.manamanager.Managemana(Statics.bowbasicmanarestore);
            }
        }
    }
    private void hooktoenemy()
    {
        if (Arrowtarget != null)
        {
            //Vector3 hookrichtung = (Arrowtarget.transform.position - LoadCharmanager.Overallmainchar.transform.position).normalized;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().hook = true;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().gravitation = 0f;
            //LoadCharmanager.Overallmainchar.GetComponent<Movescript>().controller.Move(hookrichtung * 10 * Time.deltaTime);
            missinghooktime += Time.deltaTime;
            float lerptime = missinghooktime / hookduration;
            LoadCharmanager.Overallmainchar.transform.position = Vector3.Lerp(LoadCharmanager.Overallmainchar.transform.position, Arrowtarget.position, lerptime);

        }
        /*if (Arrowtarget != null)
        {
            missinghooktime += Time.deltaTime;
            float lerptime = missinghooktime / hookduration;
            LoadCharmanager.Overallmainchar.transform.position = Vector3.Lerp(LoadCharmanager.Overallmainchar.transform.position, Arrowtarget.position, lerptime);
        }*/
    }
}