using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Supportheal
{
    public Supportmovement ssm;

    private Vector3 resposi;

    const string runstate = "Run";
    const string healstate = "Alliesheal";
    const string resurrectplayerstate = "Supportresurrect";
    const string supportstandupstate = "Supportstandup";

    public void supporthealing()
    {
        if (ssm.ishealer == true)
        {
            ssm.healtimer += Time.deltaTime;
            if(ssm.healtimer + ssm.additverandomhealtimer > Statics.alliegrouphealspawntime)
            {
                ssm.healtimer = 0;
                ssm.ChangeAnimationState(healstate);
                ssm.state = Supportmovement.State.castheal;
            }
        }
    }
    public void matecastheal()
    {
        Vector3 potionspawn = ssm.transform.position;
        potionspawn.y += 4;
        ssm.healpotion.transform.position = potionspawn;
        ssm.healpotion.SetActive(true);
        ssm.additverandomhealtimer = Random.Range(1, 5);
        ssm.switchtoweaponstate();
    }
    public void checkforresurrect()
    {
        if (Statics.supportcanresurrect == true && ssm.ishealer == true)
        {
            Statics.supportcanresurrect = false;
            ssm.resurrecttraget = null;
            setresurrecttarget();

            if(ssm.resurrecttraget == null) return;
            else
            {
                NavMeshHit hit;
                bool blocked;
                blocked = NavMesh.Raycast(ssm.transform.position, ssm.resurrecttraget.transform.position, out hit, NavMesh.AllAreas);
                if (blocked == true)
                {
                    resposi = hit.position;
                    ssm.Meshagent.SetDestination(resposi);
                }
                else
                {
                    resposi = ssm.resurrecttraget.transform.position;
                    ssm.Meshagent.SetDestination(resposi);
                }
                ssm.ChangeAnimationState(runstate);
                ssm.state = Supportmovement.State.resurrect;
            }
        }
    }
    private void setresurrecttarget()
    {
        if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == true)
        {
            ssm.resurrecttraget = LoadCharmanager.Overallmainchar;
            return;
        }
        else if (LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().playerisdead == true)
        {
            ssm.resurrecttraget = LoadCharmanager.Overallthirdchar;
            return;
        }
        else if (LoadCharmanager.Overallforthchar.GetComponent<Playerhp>().playerisdead == true)
        {
            ssm.resurrecttraget = LoadCharmanager.Overallforthchar;
            return;
        }
    }
    public void resurrectplayer()
    {
        if (Vector3.Distance(ssm.transform.position, resposi) < 3)
        {
            ssm.Meshagent.ResetPath();
            ssm.ChangeAnimationState(resurrectplayerstate);
            ssm.state = Supportmovement.State.empty;
        }
    }
    public void resurrect()
    {
        ssm.supportsounds.playresurrect();
        Statics.infightresurrectcd++;
        ssm.resurrecttraget.GetComponent<Playerhp>().playerisresurrected();

        if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == true || LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().playerisdead == true || LoadCharmanager.Overallforthchar.GetComponent<Playerhp>().playerisdead == true)
        {
            Debug.Log("stilloneplayerdead");
            GlobalCD.startsupportresurrectioncd();
        }
    }
    public void supportresurrected()
    {
        ssm.ChangeAnimationState(supportstandupstate);
        ssm.playerhp.playerisdead = false;
        float reshealth = Mathf.Round(ssm.playerhp.maxhealth * (0.2f + (Statics.groupstonehealbonus * 0.01f)));
        ssm.playerhp.addhealthwithtext(reshealth);
    }
}
