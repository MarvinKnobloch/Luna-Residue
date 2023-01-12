using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settingsbuttoncontroller : MonoBehaviour
{
    [SerializeField] private GameObject openbuttonobj;

    public void buttonobjopen()
    {
        openbuttonobj.SetActive(true);
    }
    public void buttonobjclose()
    {
        openbuttonobj.SetActive(false);
    }
}
