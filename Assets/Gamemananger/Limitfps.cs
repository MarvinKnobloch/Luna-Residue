using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Limitfps : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("fpslimit") == 0 || PlayerPrefs.GetInt("fpslimit") > 200)
        {
            PlayerPrefs.SetInt("fpslimit", 60);
            Application.targetFrameRate = PlayerPrefs.GetInt("fpslimit");
        }
        else Application.targetFrameRate = PlayerPrefs.GetInt("fpslimit");
    }
}
