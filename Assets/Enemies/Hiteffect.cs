using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiteffect : MonoBehaviour
{
    private ParticleSystem particlesystem;
    private void Awake()
    {
        particlesystem = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), 0, 0);
        transform.position = LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 1f;
        particlesystem.Play();
        StartCoroutine("effectdisable");
    }
    IEnumerator effectdisable()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

}
