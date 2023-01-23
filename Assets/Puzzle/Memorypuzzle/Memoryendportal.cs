using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memoryendportal : MonoBehaviour
{
    [SerializeField] private GameObject portalend;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = portalend.transform.position + new Vector3(0, 0.3f, 0);
            LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = true;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
        }
    }
}
