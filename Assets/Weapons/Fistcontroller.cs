using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fistcontroller : MonoBehaviour
{
    public Transform righthand;
    public Transform lefthand;
    public GameObject rightfeet;
    public GameObject body;
    private float chainbasicdmg = 6;
    private float chainenddmg = 7;
    private float air3middmg = 4;
    private float weaponswitchdmg = 10f;
    public GameObject damagetext;
    public LayerMask Layerhitbox;

    private float overallbasicdmg;
    private float overallenddmg;
    private float overallair3middmg;
    private float basicendheal = 5;
    private float overallcritchance;
    private float basicweaponswitchdmg;

    private bool crit;
    private bool resetcombochain;

    private float enddmg;

    private float enemydebuffcrit;

    private float basicmanarestore = 2;
    private float endmanarestore = 5;

    public GameObject charmanager;

    private Manamanager manacontroller;
    private Attributecontroller attributecontroller;
    private Fistattack fistattack;
    private SpielerHP spielerhp;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        manacontroller = charmanager.GetComponent<Manamanager>();
        fistattack = GetComponent<Fistattack>();
        spielerhp = GetComponent<SpielerHP>();
    }
    private void OnEnable()
    {
        LoadCharmanager.fistcontrollerupdate -= fistdmgupdate;
        fistdmgupdate();
    }
    private void OnDisable()
    {
        LoadCharmanager.fistcontrollerupdate -= fistdmgupdate;
    }
    public void fistdmgupdate()
    {
        overallbasicdmg = chainbasicdmg + attributecontroller.attack + attributecontroller.fistattack;
        overallbasicdmg += attributecontroller.overallstonebonusdmg * 0.01f * overallbasicdmg;               //wenn ich den bonus noch durch 2 teile wär die rechung so wie wenn ich den bonus zu den switchbonus addiere

        overallenddmg = chainenddmg + attributecontroller.attack + attributecontroller.fistattack;
        overallenddmg += attributecontroller.overallstonebonusdmg * 0.01f * overallenddmg;

        overallair3middmg = air3middmg + attributecontroller.attack + attributecontroller.fistattack;
        overallair3middmg += attributecontroller.overallstonebonusdmg * 0.01f * overallair3middmg;

        basicweaponswitchdmg = weaponswitchdmg + attributecontroller.attack + attributecontroller.fistattack;
        basicweaponswitchdmg += attributecontroller.overallstonebonusdmg * 0.01f * basicweaponswitchdmg;

        overallcritchance = Statics.playerbasiccritchance + attributecontroller.critchance;
    }
    public void fistrighthandhitbox()
    {
        Collider[] cols = Physics.OverlapSphere(righthand.transform.position, 2f, Layerhitbox);
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
                    basicdmg(enemyscript, Enemyhit.gameObject);
                }
            }
        }
        if (cols.Length > 0)
        {
            manacontroller.Managemana(basicmanarestore);
        }
    }    
    public void fistlefthandhitbox()
    {
        Collider[] cols = Physics.OverlapSphere(lefthand.transform.position, 2f, Layerhitbox);
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
                    basicdmg(enemyscript, Enemyhit.gameObject);
                }
            }
        }
        if (cols.Length > 0)
        {
            manacontroller.Managemana(basicmanarestore);
        }
    }
    public void fistfeethitbox()
    {      
        Collider[] cols = Physics.OverlapSphere(rightfeet.transform.position, 2f, Layerhitbox);
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
                    basicdmg(enemyscript, Enemyhit.gameObject);
                }
            }
        }
        if (cols.Length > 0)
        {
            manacontroller.Managemana(basicmanarestore);
        }
    }
    public void Checkfistdownfeethitbox()
    {
        Collider[] cols = Physics.OverlapSphere(rightfeet.transform.position, 2f, Layerhitbox);
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
        comboresetandheal();
    }
    public void Checkfistdownhandhitbox()
    {
        Collider[] cols = Physics.OverlapSphere(lefthand.transform.position, 2f, Layerhitbox);
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
    public void Checkfistmidfeethitbox()
    {
        Collider[] cols = Physics.OverlapSphere(rightfeet.transform.position, 2f, Layerhitbox);
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
                    midattack(enemyscript, Enemyhit.gameObject, overallenddmg);
                }
            }
        }
        if (cols.Length > 0)
        {
            comboresetandheal();
        }
    }
    public void Checkfistmidbodyhitbox()
    {
        Collider[] cols = Physics.OverlapSphere(body.transform.position, 2f, Layerhitbox);
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
                    midattack(enemyscript, Enemyhit.gameObject, air3middmg);
                }
            }
        }
        if (cols.Length > 0)
        {
            comboresetandheal();
        }
    }
    public void Checkfistupfeethitbox()
    {
        Collider[] cols = Physics.OverlapSphere(rightfeet.transform.position, 2f, Layerhitbox);
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
        if (cols.Length > 0)
        {
            comboresetandheal();
        }
    }
    public void Fistweaponswitchhitbox()
    {        
        Collider[] cols = Physics.OverlapSphere(righthand.transform.position, 4f, Layerhitbox);
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
            fistattack.combochain--;
        }
        spielerhp.playerheal(basicendheal);
    }

    private void basicdmg(EnemyHP enemyscript, GameObject enemyhit)
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
            calculatedmg = Mathf.Round(overallbasicdmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
        }
        else
        {
            crit = false;
            calculatedmg = Mathf.Round(overallbasicdmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100));
        }
        enemyscript.TakeDamage(calculatedmg);
        var showtext = Instantiate(damagetext, enemyhit.transform.position, Quaternion.identity);
        if (crit == true)
        {
            showtext.GetComponent<TextMeshPro>().color = Color.red;
        }
        showtext.GetComponent<TextMeshPro>().text = calculatedmg.ToString();
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


    private void midattack(EnemyHP enemyscript, GameObject enemyhit, float attackdmg)
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
            calculatedmg = attackdmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
        }
        else
        {
            crit = false;
            calculatedmg = attackdmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100);
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

/*float calculatedmg;
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
spielerhp.TakeDamage(-basicendheal);*/

