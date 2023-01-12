using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yakuselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Maincharindex") == 3 || PlayerPrefs.GetInt("Secondcharindex") == 3)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
