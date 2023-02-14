using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowsupport : MonoBehaviour
{
    
    public Transform Arrowlaunchposi;
    public GameObject basicarrow;
    public float basicarrowdmg = 1;
    private float basicdmgtodeal;

    public GameObject endarrow;
    public float endarrowdmg = 1;
    private float enddmgtodeal;

    private Attributecontroller attributecontroller;
    private Playerhp hpscript;
    private Supportmovement supportmovescript;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        hpscript = GetComponent<Playerhp>();
        supportmovescript = GetComponent<Supportmovement>();
    }

    private void OnEnable()
    {
        bowdmgupdate();
    }

    private void bowdmgupdate()
    {
        basicdmgtodeal = Damagecalculation.calculateplayerdmgdone(basicarrowdmg, attributecontroller.dmgfromallies, attributecontroller.bowattack, attributecontroller.stoneclassbonusdmg);
        enddmgtodeal = Damagecalculation.calculateplayerdmgdone(endarrowdmg, attributecontroller.dmgfromallies, attributecontroller.bowattack, attributecontroller.stoneclassbonusdmg);
    }

    private void shotchainarrow()
    {
        if (supportmovescript.currenttarget != null)
        {
            Vector3 arrowrotation = (supportmovescript.currenttarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(basicarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Npcbasicarrow arrowcontroller = Arrow.GetComponent<Npcbasicarrow>();
            arrowcontroller.Arrowtarget = supportmovescript.currenttarget.transform;
            arrowcontroller.basicdmgtodeal = basicdmgtodeal;

            if (gameObject == LoadCharmanager.Overallthirdchar)
            {
                supportmovescript.currenttarget.gameObject.GetComponent<EnemyHP>().tookdmgfrom(3, Statics.thirdchartookdmgformamount);
            }
            if (gameObject == LoadCharmanager.Overallforthchar)
            {
                supportmovescript.currenttarget.gameObject.GetComponent<EnemyHP>().tookdmgfrom(4, Statics.forthchartookdmgformamount);
            }
        }
    }
    private void shotendarrow()
    {
        if (supportmovescript.currenttarget != null)
        {
            Vector3 arrowrotation = (supportmovescript.currenttarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(endarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Npcendarrow arrowcontroller = Arrow.GetComponent<Npcendarrow>();
            arrowcontroller.Arrowtarget = supportmovescript.currenttarget.transform;
            arrowcontroller.basicdmgtodeal = enddmgtodeal;

            hpscript.playerheal(2);
            if (gameObject == LoadCharmanager.Overallthirdchar)
            {
                supportmovescript.currenttarget.gameObject.GetComponent<EnemyHP>().tookdmgfrom(3, Statics.thirdchartookdmgformamount);
            }
            if (gameObject == LoadCharmanager.Overallforthchar)
            {
                supportmovescript.currenttarget.gameObject.GetComponent<EnemyHP>().tookdmgfrom(4, Statics.forthchartookdmgformamount);
            }
        }
    }
}
