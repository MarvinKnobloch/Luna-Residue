using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disablebeam : MonoBehaviour
{
    [SerializeField] private GameObject[] laserbeams;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            foreach(GameObject obj in laserbeams)
            {
                obj.SetActive(false);
            }            
        }
    }
} 
