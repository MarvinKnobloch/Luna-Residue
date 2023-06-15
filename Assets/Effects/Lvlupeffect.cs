using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvlupeffect : MonoBehaviour
{

    private void OnEnable()
    {
        StopCoroutine("disable");
        StartCoroutine("disable");
    }
    IEnumerator disable()
    {
        yield return new WaitForSeconds(2);
        StopCoroutine("disable");
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position = LoadCharmanager.Overallmainchar.transform.position;
    }
}
