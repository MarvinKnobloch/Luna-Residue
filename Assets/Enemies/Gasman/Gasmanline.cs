using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gasmanline : MonoBehaviour
{
    public LineRenderer line;
    [SerializeField] private Transform pillar;
    [SerializeField] private Transform righttarget;
    [SerializeField] private Transform wrongtarget;
    [SerializeField] private GameObject gasmancontroller;

    public LayerMask layer;

    private void Start()
    {
        line.positionCount = 2;
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Linecast(pillar.position + Vector3.up, LoadCharmanager.Overallmainchar.transform.position + Vector3.up, out hit, layer))
        {
            if (hit.collider.gameObject == righttarget.gameObject)
            {
                if (righttarget.GetComponent<Gasmantarget>().lasercomplete == false)
                {
                    gasmancontroller.GetComponent<Gasmancontroller>().checktargets();
                }

                righttarget.GetComponent<Renderer>().material.color = pillar.GetComponent<Renderer>().material.color;
                righttarget.GetComponent<Gasmantarget>().lasercomplete = true;
                righttarget.GetComponent<Gasmantarget>().laserfail = false;
            }
            if (hit.collider.gameObject == wrongtarget.gameObject)
            {
                if (wrongtarget.GetComponent<Gasmantarget>().lasercomplete == true)
                {
                    gasmancontroller.GetComponent<Gasmancontroller>().targetscomplete -= 1;
                }
                wrongtarget.GetComponent<Renderer>().material.color = pillar.GetComponent<Renderer>().material.color;
                wrongtarget.GetComponent<Gasmantarget>().lasercomplete = false;
                wrongtarget.GetComponent<Gasmantarget>().dealdmg();
            }
        }
        line.SetPosition(0, pillar.position + Vector3.up);
        line.SetPosition(1, LoadCharmanager.Overallmainchar.transform.position + Vector3.up);
    }
}
