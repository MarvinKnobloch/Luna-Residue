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
    private Puzzlesave puzzlesave;

    private void Start()
    {
        puzzlesave = LoadCharmanager.puzzlesave;
        if(puzzlesave.boxpuzzlecomplete == true)
        {
            transform.position = endposi;
        }
    }
    private void OnEnable()
    {
        Boxfinish.checkforbridge += bridge;
    }
    private void OnDisable()
    {
        Boxfinish.checkforbridge -= bridge;
    }
    private void bridge()
    {
        needcubes = 0;
        cubesfinished = 0;
        foreach(GameObject obj in Cubes)
        {
            needcubes += 1;
            if (obj.GetComponent<Boxfinish>().finish == true)
            {
                cubesfinished += 1;
            }
        }
        if(needcubes == cubesfinished)
        {
            if(puzzlesave.boxpuzzlecomplete == false)
            {
                StartCoroutine("movebridge");
                puzzlesave.boxpuzzlecomplete = true;
            }
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
