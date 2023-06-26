using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolfexplosion : MonoBehaviour
{
    [SerializeField] private GameObject werewolfcontroller;
    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine("disable");
    }
    IEnumerator disable()
    {
        yield return new WaitForSeconds(1);
        werewolfcontroller.SetActive(false);
        gameObject.SetActive(false);
    }
}
