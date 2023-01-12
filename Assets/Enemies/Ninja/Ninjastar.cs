using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ninjastar : MonoBehaviour
{
    private float timer;
    private float speed;

    [NonSerialized] public float basedmg;              //11
    [NonSerialized] public float speedmultiplicator;   //0.028
    [NonSerialized] public float spawnspeed;           //0.4

    private void OnEnable()
    {
        timer = 0f;
        CancelInvoke();
        Invoke("turnoff", 6f);
    }
    private void Update()
    {
        transform.Rotate(0, 600 * Time.deltaTime, 0, Space.Self);
        timer += Time.deltaTime;
        speed = timer * speedmultiplicator;
        Vector3 traget = LoadCharmanager.Overallmainchar.transform.position;
        traget.y = LoadCharmanager.Overallmainchar.transform.position.y + 1f;
        transform.position = Vector3.MoveTowards(transform.position, traget, speed + spawnspeed);
    }
    private void turnoff()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar)
        {
            LoadCharmanager.Overallmainchar.GetComponent<SpielerHP>().TakeDamage(basedmg);
            gameObject.SetActive(false);
        }
    }
}
