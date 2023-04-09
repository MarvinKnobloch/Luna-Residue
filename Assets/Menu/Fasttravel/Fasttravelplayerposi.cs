using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fasttravelplayerposi : MonoBehaviour
{
    private void OnEnable()
    {
        float xposi = LoadCharmanager.savemainposi.x * 1.41f;
        float zposi = (LoadCharmanager.savemainposi.z - 350) * 1.16f;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(xposi, zposi);
    }
}
