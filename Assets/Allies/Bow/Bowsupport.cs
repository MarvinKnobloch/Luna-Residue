using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowsupport : MonoBehaviour
{
    
    public Transform Arrowlaunchposi;
    public GameObject basicarrow;
    private float basicarrowdmg = 1;
    private float basicdmgtodeal;

    private float weaponhealing;

    public GameObject endarrow;
    private float endarrowdmg = 3;
    private float enddmgtodeal;

    private bool preventdoublehealandthreat;

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
        basicdmgtodeal = Globalplayercalculations.calculatesupportdmg(basicarrowdmg, attributecontroller.dmgfromallies, attributecontroller.bowattack, attributecontroller.stoneclassbonusdmg);
        enddmgtodeal = Globalplayercalculations.calculatesupportdmg(endarrowdmg, attributecontroller.dmgfromallies, attributecontroller.bowattack, attributecontroller.stoneclassbonusdmg);

        weaponhealing = Globalplayercalculations.calculateweaponheal();
    }

    private void shotchainarrow()
    {
        if (supportmovescript.currenttarget != null)
        {
            Vector3 arrowrotation = (supportmovescript.currenttarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(basicarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Npcbasicarrow arrowcontroller = Arrow.GetComponent<Npcbasicarrow>();
            float height = supportmovescript.currenttarget.GetComponent<CapsuleCollider>().height - 0.2f;
            arrowcontroller.hitposi = supportmovescript.currenttarget.transform.position + Vector3.up * height;
            arrowcontroller.Arrowtarget = supportmovescript.currenttarget.transform;
            arrowcontroller.basicdmgtodeal = basicdmgtodeal;

            if (gameObject == LoadCharmanager.Overallthirdchar)
            {
                supportmovescript.currenttarget.gameObject.GetComponent<EnemyHP>().tookdmgfrom(3, Statics.tookdmgfromamount[2]);
            }
            if (gameObject == LoadCharmanager.Overallforthchar)
            {
                supportmovescript.currenttarget.gameObject.GetComponent<EnemyHP>().tookdmgfrom(4, Statics.tookdmgfromamount[3]);
            }
            preventdoublehealandthreat = true;
        }
    }
    private void shotendarrow()
    {
        if (supportmovescript.currenttarget != null)
        {
            Vector3 arrowrotation = (supportmovescript.currenttarget.transform.position - Arrowlaunchposi.position).normalized;
            GameObject Arrow = GameObject.Instantiate(endarrow, Arrowlaunchposi.position, Quaternion.LookRotation(arrowrotation, Vector3.up));
            Npcendarrow arrowcontroller = Arrow.GetComponent<Npcendarrow>();
            float height = supportmovescript.currenttarget.GetComponent<CapsuleCollider>().height - 0.2f;
            arrowcontroller.hitposi = supportmovescript.currenttarget.transform.position + Vector3.up * height;
            arrowcontroller.Arrowtarget = supportmovescript.currenttarget.transform;
            arrowcontroller.basicdmgtodeal = Mathf.Round(enddmgtodeal * 0.5f);

            if(preventdoublehealandthreat == true)
            {
                preventdoublehealandthreat = false;
                hpscript.addhealth(weaponhealing);
                if (gameObject == LoadCharmanager.Overallthirdchar)
                {
                    supportmovescript.currenttarget.gameObject.GetComponent<EnemyHP>().tookdmgfrom(3, Statics.tookdmgfromamount[2]);
                }
                if (gameObject == LoadCharmanager.Overallforthchar)
                {
                    supportmovescript.currenttarget.gameObject.GetComponent<EnemyHP>().tookdmgfrom(4, Statics.tookdmgfromamount[3]);
                }
            }
        }
    }
}
