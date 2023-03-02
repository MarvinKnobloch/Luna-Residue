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

    private Attributecontroller attributecontroller;
    private Playerhp hpscript;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        hpscript = GetComponent<Playerhp>();
    }

    private void OnEnable()
    {
        fistdmgupdate();
    }
    private void fistdmgupdate()
    {
        basicdmgtodeal = Globalplayercalculations.calculateplayerdmgdone(basicfistdmg, attributecontroller.dmgfromallies, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);
        enddmgtodeal = Globalplayercalculations.calculateplayerdmgdone(endfistdmg, attributecontroller.dmgfromallies, attributecontroller.fistattack, attributecontroller.stoneclassbonusdmg);

        weaponhealing = Globalplayercalculations.calculateweaponheal(attributecontroller.maxhealth);
    }

    private void firstfistattack()
    {
        dealdmg(righthand, 2f, basicdmgtodeal);
    }
    private void secondfistattack()
    {
        dealdmg(rightfoot, 3f, basicdmgtodeal);
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
                        enemyscript.tookdmgfrom(3, Statics.thirdchartookdmgformamount);
                    }
                    else if (gameObject == LoadCharmanager.Overallforthchar)
                    {
                        enemyscript.tookdmgfrom(4, Statics.forthchartookdmgformamount);
                    }
                }
            }
        }
    }
}
                
        

