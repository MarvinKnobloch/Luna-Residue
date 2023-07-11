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
    public LayerMask Layerhitbox;

    private float overallbasicdmg;
    private float overallenddmg;
    private float overallcritchance;
    private float basicweaponswitchdmg;
    private float dmgdealed;

    private float basicmanarestore = 2;
    private float endmanarestore = 5;
    private float weaponhealing = 5;

    private bool crit;

    private float enemydebuffcrit;

    public GameObject charmanager;
    private Manamanager manacontroller;

    private Attributecontroller attributecontroller;
    private Playerhp spielerhp;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        manacontroller = charmanager.GetComponent<Manamanager>();
        spielerhp = GetComponent<Playerhp>();
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
        overallbasicdmg = Globalplayercalculations.calculateplayerdmgdone(chainbasicdmg, attributecontroller.attack, attributecontroller.swordattack, attributecontroller.stoneclassbonusdmg);
        overallenddmg = Globalplayercalculations.calculateplayerdmgdone(chainenddmg, attributecontroller.attack, attributecontroller.swordattack, attributecontroller.stoneclassbonusdmg);
        basicweaponswitchdmg = Globalplayercalculations.calculateplayerdmgdone(weaponswitchdmg, attributecontroller.attack, attributecontroller.swordattack, attributecontroller.stoneclassbonusdmg);

        overallcritchance = attributecontroller.critchance;

        weaponhealing = Globalplayercalculations.calculateweaponheal();
    }

    public void Checkswordbasichitbox()
    {
        lookfordmgcollision(midofsword.transform.position, 3, overallbasicdmg, 0, basicmanarestore, 0);
    }
    public void Checksworddownhitbox()
    {
        lookfordmgcollision(midofsword.transform.position, 3f, overallenddmg, 1, endmanarestore, 1);
    }
    public void Checkswordmidhitbox()
    {
        lookfordmgcollision(midofsword.transform.position, 3f, overallenddmg, 2, endmanarestore, 1);
    }
    public void Checksworduphitbox()
    {
        lookfordmgcollision(midofsword.transform.position, 3f, overallenddmg, 3, endmanarestore, 1);
    }
    public void Swordweaponswitchhitbox()
    {
        if (Statics.bonusdmgweaponswitch == true) lookfordmgcollision(weaponswitchboxposi.transform.position, 3f, basicweaponswitchdmg * Statics.bonusdmgweaponswitchmultipler, 0, endmanarestore, 1);
        else lookfordmgcollision(weaponswitchboxposi.transform.position, 3f, basicweaponswitchdmg, 0, endmanarestore, 1);
    }
    private void lookfordmgcollision(Vector3 hitposition, float hitrange, float damage, int dmgtype, float manarestore, float sound)
    {
        if(Statics.infight == true)
        {
            Collider[] cols = Physics.OverlapSphere(hitposition, hitrange, Layerhitbox);
            foreach (Collider enemyhit in cols)
            {
                if (enemyhit.isTrigger)               //damit nur die meleehitbox getriggered wird
                {
                    if (enemyhit.gameObject.TryGetComponent(out EnemyHP enemyscript))
                    {
                        enemyscript.tookdmgfrom(1, Statics.playertookdmgfromamount);
                        if (enemyhit.gameObject == Movescript.lockontarget.gameObject)                       //es ist möglich, dass das lockontarget stirbt und das nächste target dann auch den vollen dmg bemommt
                        {
                            calculatecritchance(enemyscript, damage, true);
                            enemyscript.takeplayerdamage(dmgdealed, dmgtype, crit);
                        }
                        else
                        {
                            calculatecritchance(enemyscript, damage, false);
                            enemyscript.takeplayerdamage(Mathf.Round(dmgdealed / Statics.cleavedamagereduction), dmgtype, crit);
                        }
                    }
                }
            }
            if (cols.Length > 0)
            {
                Weaponsounds.instance.setswordhit(sound);
                healandmana(dmgtype, manarestore);
            }
            else
            {
                Weaponsounds.instance.setswordmiss(sound);
            }
        }
        else
        {
            Weaponsounds.instance.setswordmiss(sound);
        }
    }
    private void calculatecritchance(EnemyHP enemyscript, float dmg, bool maintarget)
    {
        if (enemyscript.enemydebuffcd == true) enemydebuffcrit = attributecontroller.basicattributecritbuff;
        else enemydebuffcrit = 0;

        float switchbuffdmg = Globalplayercalculations.calculateweaponcharbuff(dmg);
        float finalcritchance;
        if (maintarget == true) finalcritchance = overallcritchance + enemydebuffcrit + Statics.bonusnoncrit;
        else finalcritchance = overallcritchance + enemydebuffcrit;
        if (Random.Range(0, 100) < finalcritchance)
        {
            crit = true;
            dmgdealed = Globalplayercalculations.calculatecritdmg(dmg, overallcritchance, attributecontroller.critdmg, switchbuffdmg, maintarget);
        }
        else
        {
            crit = false;
            dmgdealed = Globalplayercalculations.calculatenoncritdmg(dmg, switchbuffdmg, maintarget);
        }
    }
    private void healandmana(int type, float manarestore)
    {
        manacontroller.Managemana(manarestore);
        if (type == 0)
        {
            return;
        }
        else
        {
            spielerhp.addhealth(weaponhealing);            //weaponhealing wird bei sworddmgupdate berechnet
        }
    }
}