using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shottargets : MonoBehaviour
{ 
    public GameObject gate;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Puzzlearrow"))
        {
            gate.GetComponent<Targetgate>().checkforopening();
            gameObject.SetActive(false);
        }
    }
}
