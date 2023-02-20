using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Alliesbottlecontroller : MonoBehaviour
{
    Rigidbody rb;
    private int spawnvelocity = 15;
    [NonSerialized] public float potionheal;
    private bool cancollect;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        int randomx = UnityEngine.Random.Range(-spawnvelocity, spawnvelocity);
        int randomz = UnityEngine.Random.Range(-spawnvelocity, spawnvelocity);
        rb.velocity = new Vector3(randomx, 4, randomz);
        Physics.IgnoreLayerCollision(8, 12);
        Physics.IgnoreLayerCollision(11, 12);
        Physics.IgnoreLayerCollision(6, 12);
        CancelInvoke();
        cancollect = false;
        Invoke("cancollectenable", 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar && cancollect == true)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Playerhp>().addhealth(potionheal);
            if (LoadCharmanager.Overallthirdchar != null)
            {
                LoadCharmanager.Overallthirdchar.GetComponent<Playerhp>().addhealth(potionheal);
            }
            if (LoadCharmanager.Overallforthchar != null)
            {
                LoadCharmanager.Overallforthchar.GetComponent<Playerhp>().addhealth(potionheal);
            }
            potiondisable();
        }
    }
    private void cancollectenable()
    {
        cancollect = true;
        Invoke("potiondisable", Statics.alliegrouphealspawntime - 4);
    }
    private void potiondisable()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
}
