using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gasmanline : MonoBehaviour
{
    public LineRenderer line;
    [SerializeField] private Transform pillar;
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;
    [SerializeField] private Gasmancontroller gasmancontroller;

    [SerializeField] private Gasmanline otherline;
    public bool hitting1;
    public bool hitting2;

    public LayerMask layer;

    private Vector3 startpoint;
    private Vector3 endpoint;

    private Color whitecolor = Color.white;
    private Color greencolor = Color.green;

    private void Start()
    {
        whitecolor.a = 0.6f;
        greencolor.a = 0.6f;
        line.positionCount = 2;
    }
    private void OnEnable()
    {
        startpoint = pillar.position;
    }
    private void Update()
    {
        endpoint = LoadCharmanager.Overallmainchar.transform.position + Vector3.up * 1.5f;
        Vector3 endpointoffset = endpoint - startpoint;
        line.SetPosition(0, startpoint);
        line.SetPosition(1, endpointoffset * 3 + startpoint);

        RaycastHit hit;
        if (Physics.Linecast(startpoint, endpointoffset * 4 + startpoint, out hit, layer) == false)
        {
            hitting1 = false;
            hitting2 = false;
            if (otherline.hitting1 == false)
            {
                if (gasmancontroller.target1activate == true)
                {
                    gasmancontroller.target1activate = false;
                    CancelInvoke("activatetarget1");
                    if (gasmancontroller.target1complete == true)
                    {
                        gasmancontroller.target1complete = false;
                        target1.GetComponent<Renderer>().material.color = whitecolor;
                    }
                }
            }

            if (otherline.hitting2 == false)
            {
                if (gasmancontroller.target2activate == true)
                {
                    gasmancontroller.target2activate = false;
                    CancelInvoke("activatetarget2");
                    if (gasmancontroller.target2complete == true)
                    {
                        gasmancontroller.target2complete = false;
                        target2.GetComponent<Renderer>().material.color = whitecolor;
                    }
                }
            }
        }
        else
        {
            if (hit.collider.gameObject == target1)
            {
                hitting1 = true;
                if (gasmancontroller.target1activate == false)
                {
                    gasmancontroller.target1activate = true;
                    Invoke("activatetarget1", 0.5f);
                }
            }
            else
            {
                hitting1 = false;
            }

            if (hit.collider.gameObject == target2)
            {
                hitting2 = true;
                if (gasmancontroller.target2activate == false)
                {
                    gasmancontroller.target2activate = true;
                    Invoke("activatetarget2", 0.5f);
                }
            }
            else hitting2 = false;
        }
    }
    private void activatetarget1()
    {
        gasmancontroller.target1complete = true;
        target1.GetComponent<Renderer>().material.color = greencolor;
        gasmancontroller.checkforcomplete();
    }
    private void activatetarget2()
    {
        gasmancontroller.target2complete = true;
        target2.GetComponent<Renderer>().material.color = greencolor;
        gasmancontroller.checkforcomplete();
    }
}
