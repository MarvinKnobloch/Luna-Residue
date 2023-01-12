using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstmoveingplattform : MonoBehaviour
{
    public GameObject nextplattform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            GetComponentInParent<contectmovement>().state = contectmovement.State.firstmovingplattform;
            nextplattform.GetComponent<contectmovement>().state = contectmovement.State.movetosecond;
        }
    }
}
