using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiegoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar)
        {
            gameObject.SetActive(false);
        }
    }
}
