using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menucontroller : MonoBehaviour
{
    public static bool inoverview;
    public GameObject fasttravelmenu;
    public Text elementalelmenutext;
    private void OnEnable()
    {
        inoverview = true;
        fasttravelmenu.SetActive(false);                //weil das Ui beim fasttravel commit nicht mehr geschlossen wird
        if (Statics.elementalmenuisactiv == false) elementalelmenutext.color = Color.gray;
        else elementalelmenutext.color = Color.black;
    }
    private void OnDisable()
    {
        inoverview = false;
    }
}
