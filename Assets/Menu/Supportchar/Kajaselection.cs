using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kajaselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Maincharindex") == 2 || PlayerPrefs.GetInt("Secondcharindex") == 2)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
