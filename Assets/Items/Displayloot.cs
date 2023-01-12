using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Displayloot : MonoBehaviour
{
    public GameObject[] lootslots;

    public void displayloot(string itemname)
    {
        foreach (GameObject obj in lootslots)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                obj.GetComponentInChildren<Text>().text = itemname;
                break;
            }
        }
    }
}
