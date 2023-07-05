using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disableeffect : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("disableobj");
    }
    IEnumerator disableobj()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
