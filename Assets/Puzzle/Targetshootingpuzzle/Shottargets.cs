using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shottargets : MonoBehaviour
{ 
    public GameObject reward;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Puzzlearrow"))
        {
            reward.GetComponent<Rewardinterface>().addrewardcount();
            gameObject.SetActive(false);
        }
    }
}
