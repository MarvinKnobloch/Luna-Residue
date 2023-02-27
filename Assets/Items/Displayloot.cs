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
                obj.transform.SetAsFirstSibling();
                obj.SetActive(true);
                obj.GetComponentInChildren<Text>().text = itemname;
                return;
            }
        }
        GameObject objifallactiv = lootslots[0].transform.parent.gameObject.GetComponent<Transform>().GetChild(4).gameObject;
        objifallactiv.transform.SetAsFirstSibling();
        objifallactiv.GetComponent<Disablelootdisplayslot>().reactivate();
        objifallactiv.GetComponentInChildren<Text>().text = itemname;
    }
}
