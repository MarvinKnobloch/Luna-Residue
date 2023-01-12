using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enterwater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            LoadCharmanager.disableattackbuttons = true;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().graviti = 0f;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().state = Movescript.State.Swim;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            LoadCharmanager.disableattackbuttons = false;
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().state = Movescript.State.Air;
        }
    }
}