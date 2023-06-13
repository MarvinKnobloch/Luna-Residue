using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantmushroom : MonoBehaviour
{
    [SerializeField] private GameObject nextmushroom;

    [SerializeField] private bool isfirstmushroom;
    [SerializeField] private SphereCollider spherecollider;
    private Color greencolor;

    private Vector3 endscale = new Vector3(5, 5, 5);
    private float growduration = 2.5f;
    private float growtimer;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#2BFF00", out greencolor);
        //greencolor.a = 0.74f;
    }
    private void OnEnable()
    {
        if (isfirstmushroom == true) spherecollider.enabled = true;
        else spherecollider.enabled = false;
        transform.localScale = new Vector3(1, 1, 1);
        growtimer = 0;
        StopAllCoroutines();
        StartCoroutine(growmushroom());
    }

    IEnumerator growmushroom()
    {
        Vector3 startscale = transform.localScale;
        while(growtimer < growduration)
        {
            growtimer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startscale, endscale, growtimer / growduration);
            yield return null;
        }
        StopCoroutine("growmushroom");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            if(nextmushroom != null)
            {
                nextmushroom.GetComponent<Plantmushroom>().activatecollider();
                nextmushroom.GetComponent<MeshRenderer>().material.color = greencolor;
            }
            gameObject.SetActive(false);
        }
    }
    public void activatecollider()
    {
        spherecollider.enabled = true;
    }
}
