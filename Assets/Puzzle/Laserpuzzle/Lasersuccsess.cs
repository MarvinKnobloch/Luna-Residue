using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Lasersuccsess : MonoBehaviour
{
    public bool laserdoeshit = false;
    public static event Action checklaserhitlist;

    public void laserupdate()
    {
        if(laserdoeshit == false)
        {
            laserdoeshit = true;
            checklaserhitlist?.Invoke();
        }
    }
}
