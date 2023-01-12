using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move5tostart : MonoBehaviour
{
    public GameObject plattform5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            plattform5.GetComponent<contectmovement>().state = contectmovement.State.movetostarting;
        }
    }
}
