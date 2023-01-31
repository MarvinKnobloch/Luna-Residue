using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetswitchtimer : MonoBehaviour
{
    [SerializeField] private Switchwithtimer switchwithtimer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            switchwithtimer.resettimer();
        }
    }
}
