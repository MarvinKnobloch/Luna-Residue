using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activateobjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            foreach(Transform obj in gameObject.transform)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            foreach (Transform obj in gameObject.transform)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}
