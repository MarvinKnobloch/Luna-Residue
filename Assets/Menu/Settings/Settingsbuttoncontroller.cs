using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settingsbuttoncontroller : MonoBehaviour
{
    [SerializeField] private GameObject openbuttonobj;
    [SerializeField] private Menusoundcontroller menusoundcontroller;

    public void buttonobjopen()
    {
        openbuttonobj.SetActive(true);
        menusoundcontroller.playmenubuttonsound();
    }
    public void buttonobjclose()
    {
        openbuttonobj.SetActive(false);
    }
}
