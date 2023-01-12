using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openselectedmenu : MonoBehaviour
{
    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject menu;

    public void openmenu()
    {
        menu.SetActive(true);
        overview.SetActive(false);
    }
}
