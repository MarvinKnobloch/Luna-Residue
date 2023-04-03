using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closegame : MonoBehaviour
{
    [SerializeField] private Menusoundcontroller menusoundcontroller;
    public void closegame()
    {
        Application.Quit();
    }
    public void dontclosegame()
    {
        menusoundcontroller.playmenubuttonsound();
        gameObject.SetActive(false);
    }
}
