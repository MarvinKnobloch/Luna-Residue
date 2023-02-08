using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arissaselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (Statics.currentfirstchar == 4 || Statics.currentsecondchar == 4)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
