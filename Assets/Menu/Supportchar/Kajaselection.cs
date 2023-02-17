using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kajaselection : MonoBehaviour
{
    private void OnEnable()
    {
        if (Statics.currentfirstchar == 2 || Statics.currentsecondchar == 2)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
