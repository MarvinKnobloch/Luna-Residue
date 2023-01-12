using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contectmovement : MonoBehaviour
{
    private Vector3 endposi;
    private Vector3 startposi;

    public float moveforward;
    public float movesideward;
    public float traveltime;
    private float movetime;


    public State state;

    public enum State
    {
        empty,
        movetosecond,
        movetostarting,
        firstmovingplattform,
    }
    void Start()
    {
        startposi = transform.position;
        endposi = transform.position + (transform.right * movesideward) + (transform.forward * moveforward);
        state = State.empty;
    }
    public void FixedUpdate()                        //normals update hat den player nicht mitbewegt
    {
        switch (state)
        {
            default:
            case State.empty:
                break;
            case State.movetosecond:
                movetosecondpoint();
                break;
            case State.movetostarting:
                movetostartingposi();
                break;
            case State.firstmovingplattform:
                firstmoveingplattform();
                break;
        }
    }

    private void OnValidate()
    {
        endposi = transform.position + (transform.right * movesideward) + (transform.forward * moveforward);
    }
    public void movetosecondpoint()
    {
        movetime += Time.deltaTime;
        float precentagecomplete = movetime / traveltime;
        transform.position = Vector3.Lerp(startposi, endposi, precentagecomplete);
        if (transform.position == endposi)
        {
            movetime = 0f;
            state = State.movetostarting;
        }
    }
    public void movetostartingposi()
    {
        if (transform.position == startposi)
        {
            movetime = 0;
            state = State.empty;
        }
        else
        {
            movetime += Time.deltaTime;
            float precentagecomplete = movetime / traveltime;
            transform.position = Vector3.Lerp(endposi, startposi, precentagecomplete);
        }
    }
    public void firstmoveingplattform()
    {
        movetime += Time.deltaTime;
        float precentagecomplete = movetime / traveltime;
        transform.position = Vector3.Lerp(startposi, endposi, precentagecomplete);
        if (transform.position == endposi)
        {
            movetime = 0;
            state = State.empty;
        }
    }
}
