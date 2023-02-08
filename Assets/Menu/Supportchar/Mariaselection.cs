using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mariaselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (Statics.currentfirstchar == 0 || Statics.currentsecondchar == 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
