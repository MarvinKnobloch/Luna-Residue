using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagetext : MonoBehaviour
{
    public float Destroytime;
    private Vector3 positiontext = new Vector3(0, 1, 0);
    private Vector3 randomtextposi = new Vector3(2, 0, 0);
    //private float randomtextposi;
    void Start()
    {
        Destroy(gameObject, Destroytime);
        transform.localPosition += positiontext;
        transform.localPosition += new Vector3(Random.Range(-randomtextposi.x, randomtextposi.x), Random.Range(-randomtextposi.y, randomtextposi.y), Random.Range(-randomtextposi.z, randomtextposi.z));
    }
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
