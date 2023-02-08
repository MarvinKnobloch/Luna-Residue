using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erikaselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (Statics.currentfirstchar == 1 || Statics.currentsecondchar == 1)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
