using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeplayerchildofplattform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar)
        {
            other.transform.parent = transform.parent;           //braucht ein übertransform damit der Scale vom Char nicht umgeändert wird
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            other.transform.parent = null;
        }
    }
}
