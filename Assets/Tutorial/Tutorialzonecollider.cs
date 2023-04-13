using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialzonecollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            LoadCharmanager.cantsavehere = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            LoadCharmanager.cantsavehere = false;
        }
    }
}
