using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fistsupport : MonoBehaviour
{
    public GameObject righthand;
    public GameObject rightfoot;
    public LayerMask enemylayer;

    private float basicfistdmg = 1;
    private float endfistdmg = 3;

    private float weaponhealing;

    private float enddmgtodeal;
    private float basicdmgtodeal;

    private Supportmovement supportmovement;
    private Attributecontroller attributecontroller;
    private Playerhp hpscript;

    private void Awake()
    {
        supportmovement = GetComponent<Supportmovement>();
        attributecontroller = GetComponent<Attributecontroller>();
        hpscript = GetComponent<Playerhp>();
    }

    private void OnEnable()
    {
        fistdmgupdate();
    }
    private void fistdmgupdate()
    {
        basicdmgtodeal = Globalplayercalculations.calculatesupportdmg(basicfistdmg, attributecontroller.dmgfromallies, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);
        enddmgtodeal = Globalplayercalculations.calculatesupportdmg(endfistdmg, attributecontroller.dmgfromallies, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);

        weaponhealing = Globalplayercalculations.calculateweaponheal();
    }

    private void firstfistattack()
    {
        if (supportmovement.currenttarget != null)
        {
            if (supportmovement.currenttarget.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.takesupportdmg(basicdmgtodeal);
                if (gameObject == LoadCharmanager.Overallthirdchar)
                {
                    enemyscript.tookdmgfrom(3, Statics.tookdmgfromamount[2]);
                }
                else if (gameObject == LoadCharmanager.Overallforthchar)
                {
                    enemyscript.tookdmgfrom(4, Statics.tookdmgfromamount[3]);
                }
            }
        }
        //dealdmg(righthand, 2f, basicdmgtodeal);
    }
    private void secondfistattack()
    {
        if (supportmovement.currenttarget != null)
        {
            if (supportmovement.currenttarget.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.takesupportdmg(basicdmgtodeal);
                if (gameObject == LoadCharmanager.Overallthirdchar)
                {
                    enemyscript.tookdmgfrom(3, Statics.tookdmgfromamount[2]);
                }
                else if (gameObject == LoadCharmanager.Overallforthchar)
                {
                    enemyscript.tookdmgfrom(4, Statics.tookdmgfromamount[3]);
                }
            }
        }
        //dealdmg(rightfoot, 3f, basicdmgtodeal);
    }
    private void thirdfistattack()
    {
        dealdmg(rightfoot, 3f, enddmgtodeal);
        hpscript.addhealth(weaponhealing);
    }
    private void dealdmg(GameObject dmgposi, float raduis, float dmg)
    {
        Collider[] cols = Physics.OverlapSphere(dmgposi.transform.position, raduis, enemylayer);
        foreach (Collider enemyhit in cols)
        {
            if (enemyhit.isTrigger)
            {
                if (enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                {
                    enemyscript.takesupportdmg(dmg);
                    if (gameObject == LoadCharmanager.Overallthirdchar)
                    {
                        enemyscript.tookdmgfrom(3, Statics.tookdmgfromamount[2]);
                    }
                    else if (gameObject == LoadCharmanager.Overallforthchar)
                    {
                        enemyscript.tookdmgfrom(4, Statics.tookdmgfromamount[3]);
                    }
                }
            }
        }
    }
}
                
        

