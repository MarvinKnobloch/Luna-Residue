using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggercollider : MonoBehaviour
{
    private void Update()
    {
        if(LoadCharmanager.Overallmainchar.gameObject != null)
        {
            transform.position = LoadCharmanager.Overallmainchar.transform.position;
        }
    }
}
