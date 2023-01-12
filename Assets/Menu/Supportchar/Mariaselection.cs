using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mariaselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Maincharindex") == 0 || PlayerPrefs.GetInt("Secondcharindex") == 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
