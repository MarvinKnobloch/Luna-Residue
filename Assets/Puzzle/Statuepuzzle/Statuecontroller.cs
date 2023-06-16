using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuecontroller : MonoBehaviour
{
    private Vector3 startposi;
    public GameObject[] statues;
    public LayerMask statueraylayer;

    private Vector3 endposi;
    public bool ismoving;

    public bool isfinished;
    [SerializeField] private GameObject Reward;

    private void Awake()
    {
        startposi = transform.position;
    }

    public void startmoving(int number)
    {
        if (number == 0) startinteraction(transform.forward, transform.forward * 3);         //forward
        if (number == 1) startinteraction(-transform.forward, transform.forward * -3);        //back
        if (number == 2) startinteraction(transform.right, transform.right * 3);           //right
        if (number == 3) startinteraction(-transform.right, transform.right * -3);          //left
    }
    private void startinteraction(Vector3 movedirection, Vector3 movement)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.up * -0.5f + movedirection);
        if (Physics.Raycast(ray, out hit, 3, statueraylayer, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("dontmove");
        }
        else
        {
            movestatues(movedirection, movement);
        }
    }
    public void movestatues(Vector3 movedirection, Vector3 movement)
    {
        foreach (GameObject obj in statues)
        {
            RaycastHit hit;
            Ray ray = new Ray(obj.transform.position, obj.transform.up * -0.5f + movedirection);
            if (Physics.Raycast(ray, out hit, 3, statueraylayer, QueryTriggerInteraction.Ignore) == false)
            {
                obj.GetComponentInChildren<Statuecontroller>().startstatuemovement(movement);
            }
        }
    }
    public void startstatuemovement(Vector3 movement)
    {
        removeisfinished();
        ismoving = true;
        endposi = transform.position + movement;
        StartCoroutine("movestatue");
    }
    IEnumerator movestatue()
    {
        while (Vector3.Distance(transform.position, endposi) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endposi, 10 * Time.deltaTime);
            yield return null;
        }
        transform.position = endposi;
        ismoving = false;
    }
    public void resetstatues()
    {
        foreach (GameObject obj in statues)
        {
            obj.gameObject.GetComponent<Statuecontroller>().backtostart();
        }
    }
    public void backtostart()
    {
        StopCoroutine("movestatue");
        removeisfinished();
        ismoving = false;
        transform.position = startposi;
    }
    private void removeisfinished()
    {
        if (isfinished == true)
        {
            isfinished = false;
            GetComponent<Renderer>().material.color = Color.white;
            Reward.GetComponent<Rewardinterface>().removerewardcount();
        }
    }
}
