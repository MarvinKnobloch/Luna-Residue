using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erikaselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Maincharindex") == 1 || PlayerPrefs.GetInt("Secondcharindex") == 1)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
