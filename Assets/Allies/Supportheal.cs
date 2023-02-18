using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportheal
{
    public Supportmovement ssm;

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
            if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == true) ssm.resurrecttraget = LoadCharmanager.Overallmainchar;
            else if (LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().playerisdead == true) ssm.resurrecttraget = LoadCharmanager.Overallthirdchar;
            else if (LoadCharmanager.Overallforthchar.GetComponent<Playerhp>().playerisdead == true) ssm.resurrecttraget = LoadCharmanager.Overallforthchar;

            if(ssm.resurrecttraget == null)
            {
                return;
            }
            else
            {
                ssm.Meshagent.SetDestination(ssm.resurrecttraget.transform.position);
                ssm.ChangeAnimationState(runstate);
                ssm.state = Supportmovement.State.resurrect;
            }
        }
    }
    public void resurrectplayer()
    {
        if (Vector3.Distance(ssm.currenttarget.transform.position, ssm.resurrecttraget.transform.position) > 3)
        {
            ssm.Meshagent.ResetPath();
            ssm.ChangeAnimationState(resurrectplayerstate);
            ssm.state = Supportmovement.State.empty;
        }
    }
    public void resurrect()
    {
        if(ssm.resurrecttraget == LoadCharmanager.Overallmainchar)
        {
            ssm.resurrecttraget.GetComponent<Movescript>().resurrected();
        }
        else
        {
            ssm.resurrecttraget.GetComponent<Supportmovement>().supportresurrected();
        }
        if (LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().playerisdead == true || LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().playerisdead == true || LoadCharmanager.Overallforthchar.GetComponent<Playerhp>().playerisdead == true)
        {
            Debug.Log("stilloneplayerdead");
            Statics.supportresurrectcd++;
            GlobalCD.startsupportresurrectioncd();
        }
        else
        {
            Debug.Log("noplayerdead");
            Statics.oneplayerisdead = false;
        }
    }
    public void supportresurrected()
    {
        ssm.ChangeAnimationState(supportstandupstate);
        float reshealth = Mathf.Round(ssm.playerhp.maxhealth * (0.2f + (Statics.groupstonehealbonus * 0.01f)));
        ssm.playerhp.addhealth(reshealth);
        ssm.playerhp.playerisdead = false;
    }
}
