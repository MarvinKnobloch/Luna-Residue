using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantsphere : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            gameObject.SetActive(false);
        }
    }
}
