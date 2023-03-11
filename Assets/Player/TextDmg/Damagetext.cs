using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagetext : MonoBehaviour
{
    [SerializeField] private float uptime;
    private Vector3 positiontext = new Vector3(0, 1, 0);
    private Vector3 randomtextposi = new Vector3(2, 0, 0);

    private void OnEnable()
    {
        CancelInvoke();
        gameObject.transform.SetAsLastSibling();
        transform.localPosition += positiontext;
        transform.localPosition += new Vector3(Random.Range(-randomtextposi.x, randomtextposi.x), Random.Range(-randomtextposi.y, randomtextposi.y), Random.Range(-randomtextposi.z, randomtextposi.z));
        Invoke("turnoff", uptime);
    }
    private void turnoff()
    {
        gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
