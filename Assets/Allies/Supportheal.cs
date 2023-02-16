using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportheal
{
    public Supportmovement ssm;

    const string healstate = "Alliesheal";
    public void supporthealing()
    {
        if (ssm.ishealer == true)
        {
            ssm.healtimer += Time.deltaTime;
            if(ssm.healtimer + ssm.additverandonhealtimer > Statics.alliegrouphealspawntime)
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
        ssm.additverandonhealtimer = Random.Range(1, 5);
        if (ssm.rangeweaponequiped == false)
        {
            ssm.state = Supportmovement.State.gettomeleerange;
        }
        else
        {
            ssm.state = Supportmovement.State.rangeweaporange;
        }
    }
}
