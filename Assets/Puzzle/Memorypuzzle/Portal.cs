using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject portalend;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            //LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = false;
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = portalend.transform.position;
            other.gameObject.SetActive(true);
            //LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = true;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
        }
    }
}
