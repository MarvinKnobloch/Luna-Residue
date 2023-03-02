using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockforward : MonoBehaviour, Interactioninterface
{
    private LayerMask layer;
    private GameObject cube;
    
    [SerializeField] private string actiontext = "Push";
    public string Interactiontext => actiontext;
    private void Awake()
    {
        cube = transform.parent.gameObject;
        layer = GetComponentInParent<Boxmovecheck>().boxraycastlayer;
    }
    public bool Interact(Closestinteraction interactor)
    {
        if(cube.GetComponent<Boxmovecheck>().moving == false)
        {
            StartCoroutine("move");
        }
        return true;
    }
    IEnumerator move()
    {
        cube.GetComponent<Boxmovecheck>().moving = true;
        while (true)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.parent.position, transform.parent.forward);
            if (Physics.Raycast(ray, out hit, 30, layer, QueryTriggerInteraction.Ignore))
            {
                Vector3 test = hit.point;
                test.z = test.z - transform.parent.localScale.z /2;
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, test, 15 * Time.deltaTime);
                if (transform.parent.position == test)
                {
                    cube.GetComponent<Boxmovecheck>().moving = false;
                    StopCoroutine("move");
                }
            }
            yield return null;
        }
    }
}
