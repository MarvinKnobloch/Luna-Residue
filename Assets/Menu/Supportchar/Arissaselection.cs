using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arissaselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Maincharindex") == 4 || PlayerPrefs.GetInt("Secondcharindex") == 4)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
