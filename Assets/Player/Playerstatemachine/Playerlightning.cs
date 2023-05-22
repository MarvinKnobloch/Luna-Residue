using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlightning
{
    public Movescript psm;

    public void stormchainligthning()
    {
        if (Movescript.lockontarget != null && psm.currentlightningtarget != null) // && Movescript.lockontarget == psm.lightningfirsttarget)
        {
            lightningmovement(psm.currentlightningtarget);
            if (Vector3.Distance(psm.transform.position, psm.currentlightningtarget.position) < 1f)
            {
                singlestormdamage(psm.currentlightningtarget.gameObject);
                if(psm.lightningthirdtarget == psm.currentlightningtarget)
                {
                    psm.state = Movescript.State.Endlightning;
                }
                checkfornewtarget();
            }
        }
        else
        {
            psm.Abilitiesend();
        }
    }
    private void checkfornewtarget()
    {
        if (Physics.CheckSphere(psm.transform.position, 20f, psm.spellsdmglayer) == true)
        {
            Collider[] colliders = Physics.OverlapSphere(psm.transform.position, 10, psm.spellsdmglayer);            //sucht das 2. lightning target
            for (int i = 0; i < colliders.Length; i++)
            {
                if (psm.ligthningsecondtarget == null)
                {
                    if (colliders[i].gameObject != psm.lightningfirsttarget.gameObject)
                    {
                        psm.ligthningsecondtarget = colliders[i].gameObject.transform;
                        psm.currentlightningtarget = colliders[i].gameObject.transform;
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (psm.lightningthirdtarget == null)               //sucht das 3. lightning target
                {
                    if (colliders[i].gameObject == psm.lightningfirsttarget.gameObject || colliders[i].gameObject == psm.ligthningsecondtarget.gameObject)
                    {
                        continue;
                    }
                    else
                    {
                        psm.lightningthirdtarget = colliders[i].gameObject.transform;
                        psm.currentlightningtarget = colliders[i].gameObject.transform;
                        return;
                    }
                }
            }
            if (psm.ligthningsecondtarget == null)
            {
                psm.Abilitiesend();
            }
            else if (psm.ligthningsecondtarget != null)
            {
                if (psm.lightningthirdtarget == null)
                {
                    psm.state = Movescript.State.Endlightning;
                }
            }
        }
        else
        {
            psm.Abilitiesend();
        }
    }
    public void stormlightningbacktomain()
    {
        if (Movescript.lockontarget != null && Movescript.lockontarget == psm.lightningfirsttarget)
        {
            lightningmovement(Movescript.lockontarget);
            if (Vector3.Distance(psm.transform.position, Movescript.lockontarget.position) < 1f)
            {
                aoestormdamage();
                psm.Abilitiesend();
            }
        }
        else
        {
            psm.Abilitiesend();
        }
    }
    private void lightningmovement(Transform target)
    {
        if(psm.lightningspeed < psm.maxlightningspeed)
        {
            psm.lightningspeed += Time.deltaTime * 10;
        }
        Vector3 distancetomove = target.position - psm.transform.position;
        Vector3 move = distancetomove.normalized * psm.lightningspeed * Time.deltaTime;
        psm.charactercontroller.Move(move);
        psm.transform.rotation = Quaternion.LookRotation(target.position - psm.transform.position, Vector3.up);
    }
    private void singlestormdamage(GameObject target)
    {
        if(target.TryGetComponent(out EnemyHP enemyhp))
        {
            enemyhp.takeelementaldamage(7);
        }
    }
    private void aoestormdamage()
    {
        psm.eleAbilities.overlapssphereeledmg(psm.transform.gameObject, 1, 7);
    }
}

/*public void stormchainlightningsecondtarget()
{
    if (Movescript.lockontarget != null && psm.ligthningsecondtarget != null)
    {
        lightningmovement(psm.ligthningsecondtarget);
        if (Vector3.Distance(psm.transform.position, psm.ligthningsecondtarget.position) < 2f)
        {
            lightningdealdmg();
            if (Physics.CheckSphere(psm.transform.position, 10f, psm.spellsdmglayer) == true)
            {
                Collider[] colliders = Physics.OverlapSphere(psm.transform.position, 10, psm.spellsdmglayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != psm.lightningfirsttarget.gameObject || colliders[i].gameObject != psm.ligthningsecondtarget.gameObject)
                    {
                        psm.lightningthirdtarget = colliders[i].gameObject.transform;
                        psm.state = Movescript.State.Thirdlightning;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (psm.lightningthirdtarget == null)
                {
                    psm.state = Movescript.State.Endlightning;
                }
            }
        }
    }
    else
    {
        psm.Abilitiesend();
    }
}

public void stormchainlightningthirdtarget()
{
    if (Movescript.lockontarget != null)
    {
        lightningmovement(psm.lightningthirdtarget);
        if (Vector3.Distance(psm.transform.position, psm.lightningthirdtarget.position) < 1f)
        {
            lightningdealdmg();
            psm.state = Movescript.State.Endlightning;
        }
    }
    else
    {
        psm.Abilitiesend();
    }
}*/