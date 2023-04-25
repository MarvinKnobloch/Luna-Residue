using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class Portal : MonoBehaviour
{
    public static event Action disableinteractionfield;

    [SerializeField] private GameObject portalend;
    [SerializeField] private LayerMask disablelayer = 1 << 8 | 1 << 20;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            Statics.interactionobjects.Clear();
            disableinteractionfield?.Invoke();
            Collider[] collider = Physics.OverlapSphere(LoadCharmanager.Overallmainchar.transform.position, 150, disablelayer);
            foreach (Collider cols in collider)
            {
                cols.gameObject.SetActive(false);
            }
            LoadCharmanager.Overallmainchar.SetActive(false);
            LoadCharmanager.Overallmainchar.gameObject.transform.position = portalend.transform.position;
            LoadCharmanager.Overallmainchar.SetActive(true);

            //LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = false;
            //other.gameObject.transform.position = portalend.transform.position;
            //LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = true;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
        }
    }
}
