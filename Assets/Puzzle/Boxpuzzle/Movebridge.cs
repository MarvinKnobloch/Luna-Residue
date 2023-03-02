using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movebridge : MonoBehaviour
{
    public GameObject[] Cubes;
    public int cubesfinished;
    public int needcubes;
    public bool bridgemoves;

    private float movingbridge;
    public float movebridgetimer;
    public Vector3 startposi;
    public Vector3 endposi;
    [SerializeField] private Areacontroller areacontroller;
    private int puzzlenumber;

    private void OnEnable()
    {
        puzzlenumber = GetComponent<Areanumber>().areanumber;
        if (areacontroller.puzzlecomplete[puzzlenumber] == true)
        {
            transform.position = endposi;
        }
        else
        {
            transform.position = startposi;
            StartCoroutine("movebridge");
        }
    }
    IEnumerator movebridge()
    {
        while (true)
        {
            movingbridge += Time.deltaTime;
            float gateopenpercantage = movingbridge / movebridgetimer;
            transform.position = Vector3.Lerp(startposi, endposi, gateopenpercantage);

            if (movingbridge >= movebridgetimer)
            {
                StopCoroutine("movebridge");
            }
            yield return null;
        }
    }
}
