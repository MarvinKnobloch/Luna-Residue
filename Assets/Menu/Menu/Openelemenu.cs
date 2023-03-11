using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openelemenu : MonoBehaviour
{
    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject menu;

    public void openelemenu()
    {
        if (Statics.elementalmenuisactiv == true)
        {
            menu.SetActive(true);
            overview.SetActive(false);
        }
    }
}
