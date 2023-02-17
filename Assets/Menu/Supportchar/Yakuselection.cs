using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yakuselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (Statics.currentfirstchar == 3 || Statics.currentsecondchar == 3)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
