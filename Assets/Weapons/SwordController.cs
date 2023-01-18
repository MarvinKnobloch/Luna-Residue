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
    private float dmgdealed;

    private bool crit;
    private bool resetcombochain;

    private float enemydebuffcrit;

    public GameObject charmanager;
    private Manamanager manacontroller;
    private Swordattack swordattack;

    private Attributecontroller attributecontroller;
    private SpielerHP spielerhp;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        manacontroller = charmanager.GetComponent<Manamanager>();
        swordattack = GetComponent<Swordattack>();
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

    public void Checkswordbasichitbox()
    {
        lookfordmgcollision(midofsword.transform.position, 3, overallbasicdmg, 0, basicmanarestore);
    }
    public void Checksworddownhitbox()
    {
        lookfordmgcollision(midofsword.transform.position, 3f, overallenddmg, 1, endmanarestore);
    }
    public void Checkswordmidhitbox()
    {
        lookfordmgcollision(midofsword.transform.position, 3f, overallenddmg, 2, endmanarestore);
    }
    public void Checksworduphitbox()
    {
        lookfordmgcollision(midofsword.transform.position, 3f, overallenddmg, 3, endmanarestore);
    }
    public void Swordweaponswitchhitbox()
    {
        lookfordmgcollision(weaponswitchboxposi.transform.position, 3f, basicweaponswitchdmg, 0, endmanarestore);
    }
    private void lookfordmgcollision(Vector3 hitposition, float hitrange, float damage, int dmgtype, float manarestore)
    {
        Collider[] cols = Physics.OverlapSphere(hitposition, hitrange, Layerhitbox);
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
                    calculatecritchance(enemyscript, damage);
                    enemyscript.TakeDamage(dmgdealed, dmgtype, crit);
                }
            }
        }
        if (cols.Length > 0)
        {
            comboresetandheal(dmgtype, manarestore);
        }
    }
    private void calculatecritchance(EnemyHP enemyscript, float dmg)
    {
        if (enemyscript.enemydebuffcd == true)
        {
            enemydebuffcrit = attributecontroller.basicattributecritbuff;
        }
        else
        {
            enemydebuffcrit = 0;
        }
        if (Random.Range(0, 100) < overallcritchance + enemydebuffcrit)
        {
            crit = true;
            dmgdealed = Mathf.Round(dmg * (attributecontroller.critdmg / 100f) * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100f) / 100));
        }
        else
        {
            crit = false;
            dmgdealed = Mathf.Round(dmg * ((Statics.weaponswitchbuff + Statics.charwechselbuff - 100) / 100));
        }
    }
    private void comboresetandheal(int type, float manarestore)
    {
        manacontroller.Managemana(manarestore);
        if (type == 0)
        {
            return;
        }
        else
        {
            if (resetcombochain == true)
            {
                resetcombochain = false;
                swordattack.combochain--;
            }
            spielerhp.playerheal(basicendheal);
        }
    }
}
    /*private void downattack(EnemyHP enemyscript, GameObject enemyhit)
    {
        float calculatedmg;
        if (enemyscript.enemydebuffcd == true)
        {
            enemydebuffcrit = attributecontroller.basicattributecritbuff;
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
                enddmg = Mathf.Round(calculatedmg * attributecontroller.basicattributedmgbuff / 100);
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
            enemydebuffcrit = attributecontroller.basicattributecritbuff;
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
                enddmg = Mathf.Round(calculatedmg * attributecontroller.basicattributedmgbuff / 100);
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
            enemydebuffcrit = attributecontroller.basicattributecritbuff;
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
                enddmg = Mathf.Round(calculatedmg * attributecontroller.basicattributedmgbuff / 100);
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
}*/