using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialareacontroller : MonoBehaviour
{
    [SerializeField] private Areacontroller areacontroller;
    private int tutorialnumber;
    void Start()
    {
        tutorialnumber = GetComponent<Areanumber>().areanumber;
        if (areacontroller.tutorialcomplete[tutorialnumber] == true)
        {
            gameObject.SetActive(false);
        }
    }
    public void tutorialfinish()
    {
        if (areacontroller.tutorialcomplete[tutorialnumber] == false)
        {
            areacontroller.tutorialcomplete[tutorialnumber] = true;
            areacontroller.autosave();
            Invoke("areadisable", 3f);                             //weil fehlermeldung kam bei direkt disable???
        }
    }
    private void areadisable()
    {
        gameObject.SetActive(false);
    }
}
