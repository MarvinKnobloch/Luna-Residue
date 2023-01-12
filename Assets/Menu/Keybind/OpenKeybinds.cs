using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeybinds : MonoBehaviour
{
    public GameObject keybindsoverlay;
    public GameObject disablemenu;
    public GameObject Charoverview;

    public void openkeybinds()
    {
        disablemenu.SetActive(false);
        Charoverview.SetActive(false);
        keybindsoverlay.SetActive(true);
    }
}

