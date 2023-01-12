using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastmovingplattform : MonoBehaviour
{
    public GameObject nextplattform;
    public GameObject nextplattform2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            nextplattform.GetComponent<contectmovement>().state = contectmovement.State.movetosecond;
            nextplattform2.GetComponent<contectmovement>().state = contectmovement.State.movetosecond;
        }
    }
}
