using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disablepuzzleobjects : MonoBehaviour
{
    public GameObject Object;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar)
        {
            Object.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("test");
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            Object.SetActive(false);
        }
    }
}
