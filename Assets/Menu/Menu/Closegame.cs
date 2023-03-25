using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closegame : MonoBehaviour
{
    public void closegame()
    {
        Application.Quit();
    }
    public void dontclosegame()
    {
        gameObject.SetActive(false);
    }
}
