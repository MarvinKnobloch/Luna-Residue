using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerheal
{
    public Movescript psm;

    const string healstart = "Healstart";
    const string playerstandupstate = "Playerstandup";
    public void starthealing()
    {
        if (Statics.healcdbool == false && LoadCharmanager.disableattackbuttons == false)
        {
            if (psm.controlls.Player.Heal.IsPressed() && Statics.otheraction == false)
            {
                //psm.playersounds.playhealingstate();
                psm.state = Movescript.State.Heal;
                psm.healingscript.strgpressed();
                psm.ChangeAnimationStateInstant(healstart);
            }
        }
    }
    public void resurrected()
    {
        psm.ChangeAnimationState(playerstandupstate);
        psm.playerhp.playerisdead = false;
        psm.playerhp.health = 1;                             //falls der char minushp hat
        float reshealth = Mathf.Round(psm.playerhp.maxhealth * (0.2f + (Statics.groupstonehealbonus * 0.01f)));
        psm.playerhp.addhealth(reshealth);
    }
    public void playerstandup()
    {
        Statics.otheraction = false;
        LoadCharmanager.disableattackbuttons = false;
        psm.switchtogroundstate();
    }
}


