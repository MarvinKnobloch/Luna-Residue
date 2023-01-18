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
    private SpielerHP hpscript;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        hpscript = GetComponent<SpielerHP>();
    }
    private void OnEnable()
    {
        sworddmgupdate();
    }
    private void sworddmgupdate()
    {
        basicdmgtodeal = Mathf.Round(basicsworddmg + attributecontroller.dmgfromallies + attributecontroller.swordattack);
        basicdmgtodeal += Mathf.Round(attributecontroller.overallstonebonusdmg * 0.01f * basicdmgtodeal);

        enddmgtodeal = Mathf.Round(endsworddmg + attributecontroller.dmgfromallies + attributecontroller.swordattack);
        enddmgtodeal += Mathf.Round(attributecontroller.overallstonebonusdmg * 0.01f * enddmgtodeal);
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
                    enemyscript.TakeDamage(basicdmgtodeal, 0, false);
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
                    enemyscript.TakeDamage(enddmgtodeal, 0, false);
                }
            }
    }
}
