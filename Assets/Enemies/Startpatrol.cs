using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startpatrol : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            if(transform.GetChild(0).gameObject.activeSelf == true)
            {
                GetComponentInChildren<Enemymovement>().patrolstart();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            if (transform.GetChild(0).gameObject.activeSelf == true) 
            {
                GetComponentInChildren<Enemymovement>().patrolend();
            }
        }
    }
}
