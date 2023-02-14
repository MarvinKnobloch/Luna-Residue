using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsupport : MonoBehaviour
{
    public GameObject swordmid;
    public LayerMask enemylayer;

    private float basicsworddmg = 1;
    private float endsworddmg = 3;

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
        sworddmgupdate();
    }
    private void sworddmgupdate()
    {
        basicdmgtodeal = Damagecalculation.calculateplayerdmgdone(basicsworddmg, attributecontroller.dmgfromallies, attributecontroller.swordattack, attributecontroller.stoneclassbonusdmg);
        enddmgtodeal = Damagecalculation.calculateplayerdmgdone(endsworddmg, attributecontroller.dmgfromallies, attributecontroller.swordattack, attributecontroller.stoneclassbonusdmg);
    }

    private void basicswordswing()
    {

        Collider[] cols = Physics.OverlapSphere(swordmid.transform.position, 3f, enemylayer);
        foreach (Collider Enemyhit in cols)
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
                if (gameObject == LoadCharmanager.Overallthirdchar)
                {
                    enemyscript.tookdmgfrom(3, Statics.thirdchartookdmgformamount);
                }
                if (gameObject == LoadCharmanager.Overallforthchar)
                {
                    enemyscript.tookdmgfrom(4, Statics.forthchartookdmgformamount);
                }
            }

        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;
                    enemyscript.takesupportdmg(basicdmgtodeal);
                }
            }
    }
    private void endswordswing()
    {
        hpscript.playerheal(4);
        Collider[] cols = Physics.OverlapSphere(swordmid.transform.position, 3f, enemylayer);

        foreach (Collider Enemyhit in cols)
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
                if (gameObject == LoadCharmanager.Overallthirdchar)
                {
                    enemyscript.tookdmgfrom(3, Statics.thirdchartookdmgformamount);
                }
                if (gameObject == LoadCharmanager.Overallforthchar)
                {
                    enemyscript.tookdmgfrom(4, Statics.forthchartookdmgformamount);
                }
            }

        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;
                    enemyscript.takesupportdmg(enddmgtodeal);
                }
            }
    }
}
