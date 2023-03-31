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

    private AudioSource audiosource;

    private void Awake()
    {
        attributecontroller = GetComponent<Attributecontroller>();
        manacontroller = charmanager.GetComponent<Manamanager>();
        spielerhp = GetComponent<Playerhp>();
        audiosource = GetComponent<AudioSource>();
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

        overallcritchance = Statics.playerbasiccritchance + attributecontroller.critchance;

        weaponhealing = Globalplayercalculations.calculateweaponheal(attributecontroller.maxhealth);
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
                        calculatecritchance(enemyscript, damage);
                        if (enemyhit.gameObject == Movescript.lockontarget.gameObject)
                        {
                            enemyscript.takeplayerdamage(dmgdealed, dmgtype, crit);
                        }
                        else
                        {
                            enemyscript.takeplayerdamage(Mathf.Round(dmgdealed / Statics.cleavedamagereduction), dmgtype, crit);
                        }
                    }
                }
            }
            if (cols.Length > 0)
            {
                healandmana(dmgtype, manarestore);
            }
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
            float switchbuffdmg = Globalplayercalculations.calculateweaponcharbuff(dmg);
            dmgdealed = Mathf.Round(dmg * (attributecontroller.critdmg / 100f) + switchbuffdmg);
        }
        else
        {
            crit = false;
            float switchbuffdmg = Globalplayercalculations.calculateweaponcharbuff(dmg);
            dmgdealed = Mathf.Round(dmg + switchbuffdmg);
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
            spielerhp.addhealth(weaponhealing);
        }
    }
}