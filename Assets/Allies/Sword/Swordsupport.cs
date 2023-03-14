using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsupport : MonoBehaviour
{
    public GameObject swordmid;
    public LayerMask enemylayer;

    private float basicsworddmg = 1;
    private float endsworddmg = 3;

    private float weaponhealing;

    private float enddmgtodeal;
    private float basicdmgtodeal;

    private Supportmovement supportmovement;
    private Attributecontroller attributecontroller;
    private Playerhp playerhp;

    private void Awake()
    {
        supportmovement = GetComponent<Supportmovement>();
        attributecontroller = GetComponent<Attributecontroller>();
        playerhp = GetComponent<Playerhp>();
    }
    private void OnEnable()
    {
        sworddmgupdate();
    }
    private void sworddmgupdate()
    {
        basicdmgtodeal = Globalplayercalculations.calculatesupportdmg(basicsworddmg, attributecontroller.dmgfromallies, attributecontroller.swordattack, attributecontroller.stoneclassbonusdmg);
        enddmgtodeal = Globalplayercalculations.calculatesupportdmg(endsworddmg, attributecontroller.dmgfromallies, attributecontroller.swordattack, attributecontroller.stoneclassbonusdmg);

        weaponhealing = Globalplayercalculations.calculateweaponheal(attributecontroller.maxhealth);
    }

    private void basicswordswing()
    {
        supportmovement.currenttarget.GetComponent<EnemyHP>().takesupportdmg(basicdmgtodeal);
        //dealdmg(swordmid, 3f, basicdmgtodeal);
    }
    private void endswordswing()
    {
        dealdmg(swordmid, 3f, enddmgtodeal);
        playerhp.addhealth(weaponhealing);
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
