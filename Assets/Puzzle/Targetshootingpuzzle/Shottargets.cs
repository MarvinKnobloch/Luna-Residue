using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shottargets : MonoBehaviour
{ 
    public GameObject gate;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if(other.CompareTag("Puzzlearrow"))
        {
            Debug.Log("arrow");
            gate.GetComponent<Targetgate>().checkforopening();
            gameObject.SetActive(false);
        }
    }
}
