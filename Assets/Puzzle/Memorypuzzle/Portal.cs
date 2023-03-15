using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Portal : MonoBehaviour
{
    public static event Action disableinteractionfield;

    [SerializeField] private GameObject portalend;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            Statics.interactionobjects.Clear();
            disableinteractionfield?.Invoke();
            LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = portalend.transform.position;
            LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = true;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
        }
    }
}
