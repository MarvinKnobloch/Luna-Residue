using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanterncontroller : MonoBehaviour
{
    public LayerMask layer;

    [SerializeField] private GameObject north;
    [SerializeField] private GameObject east;
    [SerializeField] private GameObject south;
    [SerializeField] private GameObject west;

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.forward * 20);
    }*/
    private void OnEnable()
    {
         RaycastHit N;
         Ray no = new Ray(transform.position, Vector3.forward);
         if (Physics.Raycast(no, out N, 20, layer))
         { 
            north = N.transform.gameObject;
            if(north.GetComponent<Housecontroller>())
            {
                north.GetComponent<Housecontroller>().addlantern();
            }
         }

         RaycastHit S;
         Ray so = new Ray(transform.position, Vector3.forward * -1);
         if (Physics.Raycast(so, out S, 20, layer))
         {
            south = S.transform.gameObject;
            if(south.GetComponent<Housecontroller>())
            {
                south.GetComponent<Housecontroller>().addlantern();
            }
         }

         RaycastHit E;
         Ray ea = new Ray(transform.position, Vector3.right);
         if (Physics.Raycast(ea, out E, 20, layer))
         {
            east = E.transform.gameObject;
            if(east.GetComponent<Housecontroller>())
            {
                east.GetComponent<Housecontroller>().addlantern();
            }
         }

         RaycastHit W;
         Ray we = new Ray(transform.position, Vector3.right * -1);
         if (Physics.Raycast(we, out W, 20, layer))
         {
            west = W.transform.gameObject;
            if(west.GetComponent<Housecontroller>())
            {
                west.GetComponent<Housecontroller>().addlantern();
            }
         }
    }
    private void OnDisable()
    {
        if(north != null)
        {
            if(north.GetComponent<Housecontroller>())
            {
                north.GetComponent<Housecontroller>().removelantern();
            }
            north = null;
        }
        if(east != null)
        {
            if(east.GetComponent<Housecontroller>())
            {
                east.GetComponent<Housecontroller>().removelantern();
            }
            east = null;
        }
        if(south != null)
        {
            if(south.GetComponent<Housecontroller>())
            {
                south.GetComponent<Housecontroller>().removelantern();
            }
            south = null;
        }
        if(west != null)
        {
            if(west.GetComponent<Housecontroller>())
            {
                west.GetComponent<Housecontroller>().removelantern();
            }
            west = null;
        }
    }
}
