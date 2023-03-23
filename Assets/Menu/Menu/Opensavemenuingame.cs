using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opensavemenuingame : MonoBehaviour
{
    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject menu;

    public void openmenu()
    {
        if(LoadCharmanager.cantsavehere == false)
        {
            menu.SetActive(true);
            overview.SetActive(false);
        }
    }
}
