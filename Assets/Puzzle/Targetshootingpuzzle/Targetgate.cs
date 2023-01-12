using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetgate : MonoBehaviour
{
    private bool gateisclosed;
    public int targethits = 0;
    public int targetsneed;
    private float gateopentimer;
    public float gateopeningtime;
    public Vector3 gateisclosedposi;
    public Vector3 gateisopen;
    

    public void checkforopening()
    {
        targethits++;
        if(targethits == targetsneed)
        {
            if (gateisclosed == false)
            {
                StartCoroutine("openthegate");
                gateisclosed = true;
            }
        }
    }
    IEnumerator openthegate()
    {
        while (true)
        {
            gateopentimer += Time.deltaTime;
            float gateopenpercantage = gateopentimer / gateopeningtime;
            transform.position = Vector3.Lerp(gateisclosedposi, gateisopen, gateopenpercantage);

            if (gateopentimer >= gateopeningtime)
            {
                gateopentimer = 0;
                StopCoroutine("openthegate");
            }
            yield return null;
        }
    }
}
