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
        fistdmgupdate();
    }
    private void fistdmgupdate()
    {
        basicdmgtodeal = Mathf.Round(basicfistdmg + attributecontroller.dmgfromallies + attributecontroller.fistattack);
        basicdmgtodeal += Mathf.Round(attributecontroller.overallstonebonusdmg * 0.01f * basicdmgtodeal);

        enddmgtodeal = Mathf.Round(endfistdmg + attributecontroller.dmgfromallies + attributecontroller.fistattack);
        enddmgtodeal += Mathf.Round(attributecontroller.overallstonebonusdmg * 0.01f * enddmgtodeal);
    }

    private void firstfistattack()
    {
        Collider[] cols = Physics.OverlapSphere(righthand.transform.position, 2f, enemylayer);

        foreach (Collider Enemyhit in cols)
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }
        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;

                    if (gameObject == LoadCharmanager.Overallthirdchar)
                    {
                        enemyscript.tookdmgfrom(3, Statics.thirdchartookdmgformamount);
                    }
                    if (gameObject == LoadCharmanager.Overallforthchar)
                    {
                        enemyscript.tookdmgfrom(4, Statics.forthchartookdmgformamount);
                    }
                    enemyscript.TakeDamage(basicdmgtodeal);
                }

            }
    }
    private void secondfistattack()
    {
        Collider[] cols = Physics.OverlapSphere(rightfoot.transform.position, 3f, enemylayer);

        foreach (Collider Enemyhit in cols)
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }

        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;

                    if (gameObject == LoadCharmanager.Overallthirdchar)
                    {
                        enemyscript.tookdmgfrom(3, Statics.thirdchartookdmgformamount);
                    }
                    if (gameObject == LoadCharmanager.Overallforthchar)
                    {
                        enemyscript.tookdmgfrom(4, Statics.forthchartookdmgformamount);
                    }
                    enemyscript.TakeDamage(basicdmgtodeal);
                }

            }
    }
    private void thirdfistattack()
    {
        hpscript.playerheal(4);
        Collider[] cols = Physics.OverlapSphere(rightfoot.transform.position, 3f, enemylayer);

        foreach (Collider Enemyhit in cols)
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }

        foreach (Collider Enemyhit in cols)

            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;

                    if (gameObject == LoadCharmanager.Overallthirdchar)
                    {
                        enemyscript.tookdmgfrom(3, Statics.thirdchartookdmgformamount);
                    }
                    if (gameObject == LoadCharmanager.Overallforthchar)
                    {
                        enemyscript.tookdmgfrom(4, Statics.forthchartookdmgformamount);
                    }
                    enemyscript.TakeDamage(enddmgtodeal);
                }
            }
            
    }
}
                
        

