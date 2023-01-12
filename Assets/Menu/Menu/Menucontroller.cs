using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menucontroller : MonoBehaviour
{
    public static bool inoverview;
    public GameObject fasttravelmenu;
    private void OnEnable()
    {
        inoverview = true;
        fasttravelmenu.SetActive(false);
    }
    private void OnDisable()
    {
        inoverview = false;
    }
}
