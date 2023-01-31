using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserbeam : MonoBehaviour
{
    public int maxreflections;
    private LineRenderer renderline;
    public Transform startpoint;
    public float laserrange;
    public string Mirror;
    [SerializeField] GameObject endgoal;
    [SerializeField] Material goalmaterial;
    [SerializeField] private LayerMask mirrorlayer;

    void Start()
    {
        renderline = GetComponent<LineRenderer>();
        renderline.SetPosition(0, startpoint.position);
    }
    private void OnDisable()
    {
        endgoal.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    void Update()
    {
        Castlaser(transform.position, transform.forward);
    }
    private void Castlaser(Vector3 position, Vector3 direction)
    {
        renderline.SetPosition(0, startpoint.position);

        for (int i = 0; i < maxreflections; i++)
        {
            Ray laser = new Ray(position, direction);
            if(Physics.Raycast(laser, out RaycastHit laserhit, laserrange, mirrorlayer, QueryTriggerInteraction.Ignore))
            {
                position = laserhit.point;
                direction = Vector3.Reflect(direction, laserhit.normal);
                renderline.SetPosition(i + 1, laserhit.point);

                if (laserhit.transform == endgoal.transform)
                {
                    endgoal.GetComponent<MeshRenderer>().material.color = goalmaterial.color;
                    endgoal.GetComponent<Lasersuccsess>().laserupdate();
                }
                else
                {
                    endgoal.GetComponent<MeshRenderer>().material.color = Color.white;
                    endgoal.GetComponent<Lasersuccsess>().laserdoeshit = false;
                }

                if (laserhit.transform.CompareTag(Mirror) == false)
                {
                    for(int e= (i+1); e <=maxreflections; e++)
                    {
                        renderline.SetPosition(e, laserhit.point);
                    }
                    break;
                }
            }
        }
    }
}
