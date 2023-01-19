using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerheal
{
    public Movescript psm;

    const string healstart = "Healstart";
    public void starthealing()
    {
        if (Statics.healcdbool == false && LoadCharmanager.disableattackbuttons == false)
        {
            if (psm.controlls.Player.Heal.IsPressed() && Statics.otheraction == false)
            {
                psm.state = Movescript.State.Heal;
                psm.healingscript.strgpressed();
                psm.ChangeAnimationStateInstant(healstart);
            }
        }
    }
}


