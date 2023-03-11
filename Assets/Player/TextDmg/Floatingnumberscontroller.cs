using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Floatingnumberscontroller : MonoBehaviour
{
    public static Floatingnumberscontroller floatingnumberscontroller;
    private void Awake()
    {
        if(floatingnumberscontroller == null)
        {
            floatingnumberscontroller = this;
        }
    }
    public void activatenumbers(GameObject spawnposition, float number, Color textcolor)
    {
        GameObject nextprefab = transform.GetChild(0).gameObject;
        nextprefab.GetComponent<TextMeshPro>().text = number.ToString();
        nextprefab.GetComponent<TextMeshPro>().color = textcolor;
        nextprefab.transform.position = spawnposition.transform.position;
        nextprefab.SetActive(true);
    }
}
