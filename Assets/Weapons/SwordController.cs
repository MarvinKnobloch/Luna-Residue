using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SwordController : MonoBehaviour
{
    public Transform midofsword;
    public GameObject weaponswitchboxposi;
    private float chainbasicdmg = 5;
    private float chainenddmg = 7;
    private float weaponswitchdmg = 10f;
    public GameObject damagetext;
    public LayerMask Layerhitbox;

    private float overallbasicdmg;
    private float overallenddmg;
    private float basicendheal = 5;
    private float overallcritchance;
    private float basicweaponswitchdmg;

    private float basicmanarestore = 2;
    private float endmanarestore = 5;
    private float enddmg;

    private bool crit;
    private bool resetcombochain;

    private float enemydebuffcrit;

    public GameObject charmanager;
    private Manamanager manacontroller;

    private Attributecontroller attributecontroller;
    private SpielerHP spielerhp;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        manacontroller = charmanager.GetComponent<Manamanager>();
        spielerhp = GetComponent<SpielerHP>();
    }
    private void OnEnable()
    {
        LoadCharmanager.swordcontrollerupdate += sworddmgupdate;

        sworddmgupdate();
    }
    private void OnDisable()
    {
        LoadCharmanager.swordcontrollerupdate -= sworddmgupdate;
    }
    private void sworddmgupdate()
    {
        overallbasicdmg = chainbasicdmg + attributecontroller.attack + attributecontroller.swordattack;
        overallbasicdmg += attributecontroller.overallstonebonusdmg * 0.01f * overallbasicdmg;

        overallenddmg = chainenddmg + attributecontroller.attack + attributecontroller.swordattack;
        overallenddmg += attributecontroller.overallstonebonusdmg * 0.01f * overallenddmg;

        basicweaponswitchdmg = weaponswitchdmg + attributecontroller.attack + attributecontroller.swordattack;
        basicweaponswitchdmg += attributecontroller.overallstonebonusdmg * 0.01f * basicweaponswitchdmg;

        overallcritchance = Statics.playerbasiccritchance + attributecontroller.critchance;
    }

    public void Checkswordhitbox()
    {
        Collider[] cols = Physics.OverlapSphere(midofsword.transform.position, 3f, Layerhitbox);
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }
        }
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;
                    enemyscript.tookdmgfrom(1, Statics.playertookdmgfromamount);
                    float calculatedmg;
                    if(enemyscript.enemydebuffcd == true)
                    {
                        enemydebuffcrit = attributecontroller.basiccrit;
                    }
                    else
                    {
                        enemydebuffcrit = 0;
                    }
                    if (Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
                    {
                        crit = true;
                        calculatedmg = Mathf.Round(overallbasicdmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
                    }
                    else
                    {
                        crit = false;
                        calculatedmg = Mathf.Round(overallbasicdmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100));
                    }
                    enemyscript.TakeDamage(calculatedmg);
                    var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                    if (crit == true)
                    {
                        showtext.GetComponent<TextMeshPro>().color = Color.red;
                    }
                    showtext.GetComponent<TextMeshPro>().text = calculatedmg.ToString();
                }
            }
        }
        if (cols.Length > 0)
        {
            manacontroller.Managemana(basicmanarestore);
        }
    }
    public void Checksworddownhitbox()
    {
        Collider[] cols = Physics.OverlapSphere(midofsword.transform.position, 3f, Layerhitbox);
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }
        }
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;
                    enemyscript.tookdmgfrom(1, Statics.playertookdmgfromamount);
                    downattack(enemyscript, Enemyhit.gameObject);
                }
            }
        }
        if (cols.Length > 0)
        {
            comboresetandheal();
        }
    }
    public void Checkswordmidhitbox()
    {
        Collider[] cols = Physics.OverlapSphere(midofsword.transform.position, 3f, Layerhitbox);
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }
        }
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;
                    enemyscript.tookdmgfrom(1, Statics.playertookdmgfromamount);
                    midattack(enemyscript, Enemyhit.gameObject);
                }
            }
        }
        if (cols.Length > 0)
        {
            comboresetandheal();
        }
    }
    public void Checksworduphitbox()
    {
        Collider[] cols = Physics.OverlapSphere(midofsword.transform.position, 3f, Layerhitbox);
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }
        }
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;
                    enemyscript.tookdmgfrom(1, Statics.playertookdmgfromamount);
                    upattack(enemyscript, Enemyhit.gameObject);
                }
            }
        }
        if(cols.Length > 0)
        {
            comboresetandheal();
        }
    }
    public void Swordweaponswitchhitbox()
    {
        Collider[] cols = Physics.OverlapSphere(weaponswitchboxposi.transform.position, 3f, Layerhitbox);
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                enemyscript.dmgonce = false;
            }
        }
        foreach (Collider Enemyhit in cols)
        {
            if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
            {
                if (enemyscript.dmgonce == false)
                {
                    enemyscript.dmgonce = true;
                    enemyscript.tookdmgfrom(1, Statics.playertookdmgfromamount);
                    float calculatedmg;
                    if (enemyscript.enemydebuffcd == true)
                    {
                        enemydebuffcrit = attributecontroller.basiccrit;
                    }
                    else
                    {
                        enemydebuffcrit = 0;
                    }
                    if (Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
                    {
                        crit = true;
                        calculatedmg = Mathf.Round(basicweaponswitchdmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100));
                    }
                    else
                    {
                        crit = false;
                        calculatedmg = Mathf.Round(basicweaponswitchdmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100));
                    }
                    enemyscript.TakeDamage(calculatedmg);
                    var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                    showtext.GetComponent<TextMeshPro>().text = calculatedmg.ToString();
                    if (crit == true)
                    {
                        showtext.GetComponent<TextMeshPro>().color = Color.red;
                    }
                }
            }
        }
        if (cols.Length > 0)
        {
            manacontroller.Managemana(endmanarestore);
        }
    }

    private void comboresetandheal()
    {
        manacontroller.Managemana(endmanarestore);
        if (resetcombochain == true)
        {
            resetcombochain = false;
            GetComponent<Swordattack>().combochain--;
        }
        spielerhp.playerheal(basicendheal);
    }
    private void downattack(EnemyHP enemyscript, GameObject enemyhit)
    {
        float calculatedmg;
        if (enemyscript.enemydebuffcd == true)
        {
            enemydebuffcrit = attributecontroller.basiccrit;
        }
        else
        {
            enemydebuffcrit = 0;
        }
        if (Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
        {
            crit = true;
            calculatedmg = overallenddmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
        }
        else
        {
            crit = false;
            calculatedmg = overallenddmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
        }
        if (enemyscript.sizeofenemy == 0)
        {
            if (enemyscript.enemyincreasebasicdmg == true)
            {
                enddmg = Mathf.Round(calculatedmg * attributecontroller.basicdmgbuff / 100);
                resetcombochain = true;
                enemyscript.TakeDamage(enddmg);
            }
            else
            {
                enddmg = Mathf.Round(calculatedmg * 50 / 100);
                enemyscript.TakeDamage(enddmg);
            }
        }
        else if (enemyscript.sizeofenemy == 1)
        {
            if (enemyscript.enemydebuffcd == false)
            {
                enemyscript.enemydebuffstart();
                resetcombochain = true;
            }
            enddmg = Mathf.Round(calculatedmg * 85 / 100);
            enemyscript.TakeDamage(enddmg);
        }
        else if (enemyscript.sizeofenemy == 2)
        {
            enddmg = Mathf.Round(calculatedmg);
            enemyscript.TakeDamage(enddmg);
        }
        var showtext = Instantiate(damagetext, enemyhit.transform.position, Quaternion.identity);
        showtext.GetComponent<TextMeshPro>().text = enddmg.ToString();
        if (crit == true)
        {
            showtext.GetComponent<TextMeshPro>().color = Color.red;
        }
    }


    private void midattack(EnemyHP enemyscript, GameObject enemyhit)
    {
        float calculatedmg;
        if (enemyscript.enemydebuffcd == true)
        {
            enemydebuffcrit = attributecontroller.basiccrit;
        }
        else
        {
            enemydebuffcrit = 0;
        }
        if (Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
        {
            crit = true;
            calculatedmg = overallenddmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
        }
        else
        {
            crit = false;
            calculatedmg = overallenddmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
        }
        if (enemyscript.sizeofenemy == 0)
        {
            enddmg = Mathf.Round(calculatedmg);
            enemyscript.TakeDamage(enddmg);
        }
        else if (enemyscript.sizeofenemy == 1)
        {
            if (enemyscript.enemyincreasebasicdmg == true)
            {
                enddmg = Mathf.Round(calculatedmg * attributecontroller.basicdmgbuff / 100);
                resetcombochain = true;
                enemyscript.TakeDamage(enddmg);
            }
            else
            {
                enddmg = Mathf.Round(calculatedmg * 50 / 100);
                enemyscript.TakeDamage(enddmg);
            }
        }
        else if (enemyscript.sizeofenemy == 2)
        {
            if (enemyscript.enemydebuffcd == false)
            {
                enemyscript.enemydebuffstart();
                resetcombochain = true;
            }
            enddmg = Mathf.Round(calculatedmg * 85 / 100);
            enemyscript.TakeDamage(enddmg);
        }
        var showtext = Instantiate(damagetext, enemyhit.transform.position, Quaternion.identity);
        showtext.GetComponent<TextMeshPro>().text = enddmg.ToString();
        if (crit == true)
        {
            showtext.GetComponent<TextMeshPro>().color = Color.red;
        }
    }


    private void upattack(EnemyHP enemyscript, GameObject enemyhit)
    {
        float calculatedmg;
        if (enemyscript.enemydebuffcd == true)
        {
            enemydebuffcrit = attributecontroller.basiccrit;
        }
        else
        {
            enemydebuffcrit = 0;
        }
        if (Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
        {
            crit = true;
            calculatedmg = overallenddmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
        }
        else
        {
            crit = false;
            calculatedmg = overallenddmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
        }

        if (enemyscript.sizeofenemy == 0)
        {
            if (enemyscript.enemydebuffcd == false)
            {
                enemyscript.enemydebuffstart();
                resetcombochain = true;
            }
            enddmg = Mathf.Round(calculatedmg * 85 / 100);
            enemyscript.TakeDamage(enddmg);
        }
        else if (enemyscript.sizeofenemy == 1)
        {
            enddmg = Mathf.Round(calculatedmg);
            enemyscript.TakeDamage(enddmg);
        }
        else if (enemyscript.sizeofenemy == 2)
        {
            if (enemyscript.enemyincreasebasicdmg == true)
            {
                enddmg = Mathf.Round(calculatedmg * attributecontroller.basicdmgbuff / 100);
                resetcombochain = true;
                enemyscript.TakeDamage(enddmg);
            }
            else
            {
                enddmg = Mathf.Round(calculatedmg * 50 / 100);
                enemyscript.TakeDamage(enddmg);
            }
        }
        var showtext = Instantiate(damagetext, enemyhit.transform.position, Quaternion.identity);
        showtext.GetComponent<TextMeshPro>().text = enddmg.ToString();
        if (crit == true)
        {
            showtext.GetComponent<TextMeshPro>().color = Color.red;
        }
    }
}


/*foreach (Collider Enemyhit in cols)
    if (Enemyhit.gameObject.GetComponentInParent<Enemymovement>())
    {
        Enemyhit.gameObject.GetComponentInParent<Enemymovement>().gethit = true;
    }*/
//Collider[] cols = Physics.OverlapBox(Schwerthitbox.bounds.center, Schwerthitbox.bounds.extents, Schwerthitbox.transform.rotation, LayerMask.GetMask("Enemyhitbox"));               // colliderpos, größe(muss nur im programm angegeben werden wenn keine hitbox erstellt worden ist, rotation, layer

/*public void Checksworddownhitbox()
{
    manacontroller.Managemana(endmanarestore);
    Collider[] cols = Physics.OverlapSphere(midofsword.transform.position, 3f, Layerhitbox);
    foreach (Collider Enemyhit in cols)
    {
        if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
        {
            enemyscript.dmgonce = false;
        }
    }
    foreach (Collider Enemyhit in cols)
    {
        if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
        {
            if (enemyscript.dmgonce == false)
            {
                enemyscript.dmgonce = true;
                enemyscript.tookdmgfrom(1);
                float calculatedmg;
                if (Random.Range(0, 100) < basiccritchance + attributecontroller.critchance + Enemyhit.gameObject.GetComponent<Enemydebuff>().enemycritdebuff)
                {
                    crit = true;
                    calculatedmg = overallenddmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
                }
                else
                {
                    crit = false;
                    calculatedmg = overallenddmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
                }
                if (Enemyhit.gameObject.GetComponent<smallenemy>())
                {
                    if (Enemyhit.gameObject.GetComponent<Enemydebuff>().takendmgdebuff == true)
                    {
                        enddmg = Mathf.Round(calculatedmg * attributecontroller.basicdmgbuff / 100);
                        GetComponent<Schwertattack>().combochain--;
                        enemyscript.TakeDamage(enddmg);
                    }
                    else
                    {
                        enddmg = Mathf.Round(calculatedmg * 50 / 100);
                        enemyscript.TakeDamage(enddmg);
                    }
                }
                if (Enemyhit.gameObject.GetComponent<midenemy>())
                {
                    if (Enemyhit.gameObject.GetComponent<Enemydebuff>().takendmgdebuffcd == false)
                    {
                        Enemyhit.gameObject.GetComponent<Enemydebuff>().starttakendmgdebuff();
                    }
                    enddmg = Mathf.Round(calculatedmg * 85 / 100);
                    enemyscript.TakeDamage(enddmg);
                }
                if (Enemyhit.gameObject.GetComponent<bigenemy>())
                {
                    enddmg = Mathf.Round(calculatedmg);
                    enemyscript.TakeDamage(enddmg);
                }

                var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                showtext.GetComponent<TextMeshPro>().text = enddmg.ToString();
                if (crit == true)
                {
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }

                spielerhp.TakeDamage(-basicendheal);
            }
        }
    }
}
public void Checkswordmidhitbox()
{
    manacontroller.Managemana(endmanarestore);
    Collider[] cols = Physics.OverlapSphere(midofsword.transform.position, 3f, Layerhitbox);
    foreach (Collider Enemyhit in cols)
    {
        if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
        {
            enemyscript.dmgonce = false;
        }
    }
    foreach (Collider Enemyhit in cols)
    {
        if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
        {
            if (enemyscript.dmgonce == false)
            {
                enemyscript.dmgonce = true;
                enemyscript.tookdmgfrom(1);
                float calculatedmg;
                if (Random.Range(0, 100) < basiccritchance + attributecontroller.critchance + Enemyhit.gameObject.GetComponent<Enemydebuff>().enemycritdebuff)
                {
                    crit = true;
                    calculatedmg = overallenddmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
                }
                else
                {
                    crit = false;
                    calculatedmg = overallenddmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
                }
                if (Enemyhit.gameObject.GetComponent<smallenemy>())
                {
                    enddmg = Mathf.Round(calculatedmg);
                    enemyscript.TakeDamage(enddmg);
                }
                if (Enemyhit.gameObject.GetComponent<midenemy>())
                {
                    if (Enemyhit.gameObject.GetComponent<Enemydebuff>().takendmgdebuff == true)
                    {
                        enddmg = Mathf.Round(calculatedmg * attributecontroller.basicdmgbuff / 100);
                        GetComponent<Schwertattack>().combochain--;
                        enemyscript.TakeDamage(enddmg);
                    }
                    else
                    {
                        enddmg = Mathf.Round(calculatedmg * 50 / 100);
                        enemyscript.TakeDamage(enddmg);
                    }
                }
                if (Enemyhit.gameObject.GetComponent<bigenemy>())
                {
                    if (Enemyhit.gameObject.GetComponent<Enemydebuff>().takendmgdebuffcd == false)
                    {
                        Enemyhit.gameObject.GetComponent<Enemydebuff>().starttakendmgdebuff();
                    }
                    enddmg = Mathf.Round(calculatedmg * 85 / 100);
                    enemyscript.TakeDamage(enddmg);
                }
                var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                showtext.GetComponent<TextMeshPro>().text = enddmg.ToString();
                if (crit == true)
                {
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
                spielerhp.TakeDamage(-basicendheal);
            }
        }
    }
}
public void Checksworduphitbox()
{
    manacontroller.Managemana(endmanarestore);
    Collider[] cols = Physics.OverlapSphere(midofsword.transform.position, 3f, Layerhitbox);
    foreach (Collider Enemyhit in cols)
    {
        if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
        {
            enemyscript.dmgonce = false;
        }
    }
    foreach (Collider Enemyhit in cols)
    {
        if (Enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
        {
            if (enemyscript.dmgonce == false)
            {
                enemyscript.dmgonce = true;
                enemyscript.tookdmgfrom(1);
                float calculatedmg;
                if (Random.Range(0, 100) < basiccritchance + attributecontroller.critchance + Enemyhit.gameObject.GetComponent<Enemydebuff>().enemycritdebuff)
                {
                    crit = true;
                    calculatedmg = overallenddmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
                }
                else
                {
                    crit = false;
                    calculatedmg = overallenddmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
                }

                if (Enemyhit.gameObject.GetComponent<smallenemy>())
                {
                    if (Enemyhit.gameObject.GetComponent<Enemydebuff>().takendmgdebuffcd == false)
                    {
                        Enemyhit.gameObject.GetComponent<Enemydebuff>().starttakendmgdebuff();
                    }
                    enddmg = Mathf.Round(calculatedmg * 85 / 100);
                    enemyscript.TakeDamage(enddmg);
                }
                if (Enemyhit.gameObject.GetComponent<midenemy>())
                {
                    enddmg = Mathf.Round(calculatedmg);
                    enemyscript.TakeDamage(enddmg);
                }
                if (Enemyhit.gameObject.GetComponent<bigenemy>())
                {
                    if (Enemyhit.gameObject.GetComponent<Enemydebuff>().takendmgdebuff == true)
                    {
                        enddmg = Mathf.Round(calculatedmg * attributecontroller.basicdmgbuff / 100);
                        GetComponent<Schwertattack>().combochain--;
                        enemyscript.TakeDamage(enddmg);
                    }
                    else
                    {
                        enddmg = Mathf.Round(calculatedmg * 50 / 100);
                        enemyscript.TakeDamage(enddmg);
                    }
                }
                var showtext = Instantiate(damagetext, Enemyhit.transform.position, Quaternion.identity);
                showtext.GetComponent<TextMeshPro>().text = enddmg.ToString();
                if (crit == true)
                {
                    showtext.GetComponent<TextMeshPro>().color = Color.red;
                }
                spielerhp.TakeDamage(-basicendheal);
            }
        }
    }
}*/