using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveform : MonoBehaviour
{
    private Vector3 Endposi;
    private Vector3 Startposi;

    public float moveforward;
    public float movesideward;
    public float moveup;
    public float traveltime;
    public float movetime;


    public State state;

    public enum State
    {
        moveout,
        movein,
    }
    void Start()
    {
        Startposi = transform.position;
        Endposi = transform.position + (transform.right * movesideward) + (transform.forward * moveforward) + (transform.up * moveup);
    }

    /*private void OnValidate()
    {
        Endposi = transform.position + (transform.right * movesideward) + (transform.forward * moveforward) + (transform.up * moveup);
    }*/
    private void FixedUpdate()                        //normals update hat den player nicht mitbewegt
    {
        switch (state)
        {
            default:
            case State.moveout:
                outofwallmove();
                break;
            case State.movein:
                inwallmove();
                break;
        }
    }
    void outofwallmove()
    {
        movetime += Time.deltaTime;
        float precentagecomplete = movetime / traveltime;
        transform.position = Vector3.Lerp(Endposi, Startposi, precentagecomplete);
        //transform.position = Vector3.MoveTowards(transform.position, outofwall, speed * Time.deltaTime);
        if (transform.position == Startposi)
        {
            movetime = 0;
            state = State.movein;
        }
    }
    void inwallmove()
    {
        movetime += Time.deltaTime;
        float precentagecomplete = movetime / traveltime;
        transform.position = Vector3.Lerp(Startposi, Endposi, precentagecomplete);
        //transform.position = Vector3.MoveTowards(transform.position, inwall, speed * Time.deltaTime);
        if (transform.position == Endposi)
        {
            movetime = 0f;
            state = State.moveout;
        }
    }
}
