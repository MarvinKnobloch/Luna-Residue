using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummyexplosion : MonoBehaviour
{
    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine("disable");
    }
    IEnumerator disable()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
