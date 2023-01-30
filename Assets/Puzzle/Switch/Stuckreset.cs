using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuckreset : MonoBehaviour
{
    public GameObject puzzlestartposi;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = puzzlestartposi.transform.position + new Vector3(0, 0.5f, 0);
            LoadCharmanager.Overallmainchar.GetComponent<CharacterController>().enabled = true;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
        }
    }
}
