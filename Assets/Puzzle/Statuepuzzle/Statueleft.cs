using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statueleft : MonoBehaviour, Interactioninterface
{
    public LayerMask layer;
    public GameObject[] Statues;

    [SerializeField] private string actiontext = "Move";
    public string Interactiontext => actiontext;
    private void Awake()
    {
        layer = GetComponentInParent<Statuecontroller>().statueraylayer;
    }
    public bool Interact(Closestinteraction interactor)
    {
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.parent.position, transform.parent.up * -0.5f - transform.parent.right);
            if (Physics.Raycast(ray, out hit, 3, layer, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("dontmove");
                return true;
            }
            else
            {
                movestatues();
            }
        }
        return true;
    }
    public void movestatues()
    {
        foreach (GameObject obj in Statues)
        {
            RaycastHit hit;
            Ray ray = new Ray(obj.transform.position, obj.transform.up * -0.5f - obj.transform.right);
            if (Physics.Raycast(ray, out hit, 3, layer, QueryTriggerInteraction.Ignore) == false)
            {
                obj.transform.position = obj.transform.position + transform.right * -3;
            }
        }
    }
}
