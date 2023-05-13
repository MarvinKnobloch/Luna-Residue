using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disableobjects : MonoBehaviour
{
    public GameObject Object;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.triggercollider)
        {
            Object.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.triggercollider)
        {
            Object.SetActive(false);
        }
    }
}
