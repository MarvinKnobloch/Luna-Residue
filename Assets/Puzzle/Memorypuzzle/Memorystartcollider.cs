using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Memorystartcollider : MonoBehaviour
{
    public static event Action startpuzzle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            startpuzzle?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
