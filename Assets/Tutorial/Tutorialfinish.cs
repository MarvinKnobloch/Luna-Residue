using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialfinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            LoadCharmanager.cantsavehere = false;
            GetComponentInParent<Tutorialareacontroller>().tutorialfinish();
        }
    }
}
