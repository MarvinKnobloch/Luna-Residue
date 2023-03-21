using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantsphere : MonoBehaviour
{
    [SerializeField] private GameObject nextsphere;
    public bool isactivsphere;

    private Color greencolor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#2C6536", out greencolor);
        greencolor.a = 0.74f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && isactivsphere == true)
        {
            isactivsphere = false;
            if(nextsphere != null)
            {
                nextsphere.GetComponent<Plantsphere>().isactivsphere = true;
                nextsphere.GetComponent<MeshRenderer>().material.color = greencolor;
            }
            gameObject.SetActive(false);
        }
    }
}
