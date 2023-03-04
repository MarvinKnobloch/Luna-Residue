using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Laserpuzzlefinish : MonoBehaviour
{
    public bool laserdoeshit = false;
    [SerializeField] private GameObject reward;

    public void laserupdate()
    {
        if(laserdoeshit == false)
        {
            laserdoeshit = true;
            reward.GetComponent<Rewardinterface>().addrewardcount();
        }
    }
    public void laserdoesnthit()
    {
        if (laserdoeshit == true)
        {
            laserdoeshit = false;
            reward.GetComponent<Rewardinterface>().removerewardcount();
        }
    }
}
