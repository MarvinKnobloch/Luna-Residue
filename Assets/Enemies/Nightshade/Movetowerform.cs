using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movetowerform : MonoBehaviour
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
    private void OnEnable()
    {

        Startposi = transform.position;
        Endposi = transform.position + (transform.right * movesideward) + (transform.forward * moveforward) + (transform.up * moveup);
    }
    private void OnDisable()
    {
        transform.position = Startposi;
    }

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
        if (transform.position == Endposi)
        {
            movetime = 0f;
            state = State.moveout;
        }
    }
}
