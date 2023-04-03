using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opensupportchar : MonoBehaviour
{
    [SerializeField] private GameObject menuoverview;
    private Menucontroller menucontroller;
    public GameObject thirdcharselection;
    public bool thirdcharselectionactive;
    public GameObject[] thirdselectionslots;

    public GameObject forthcharselection;
    public bool forthcharselectionactive;
    public GameObject[] forthselectionslots;

    private void Awake()
    {
        menucontroller = menuoverview.GetComponent<Menucontroller>();
    }
    private void OnEnable()
    {
        thirdcharselectionactive = false;
        thirdcharselection.SetActive(false);

        forthcharselectionactive = false;
        forthcharselection.SetActive(false);
    }
    public void openthirdcharselection()
    {
        if(thirdcharselectionactive == false)
        {
            menucontroller.closeallselections();
            menucontroller.somethinginmenuisopen = true;
            thirdcharselectionactive = true;
            thirdcharselection.SetActive(true);
            forthcharselectionactive = false;
            foreach (GameObject slot in thirdselectionslots)
            {
                slot.SetActive(true);
            }
        }
        else
        {
            menucontroller.somethinginmenuisopen = false;
            thirdcharselectionactive = false;
            thirdcharselection.SetActive(false);
        }
        menucontroller.menusoundcontroller.playmenubuttonsound();
    }
    public void openforthcharselection()
    {
        if(forthcharselectionactive == false)
        {
            menucontroller.closeallselections();
            menucontroller.somethinginmenuisopen = true;
            forthcharselectionactive = true;
            forthcharselection.SetActive(true);
            thirdcharselectionactive = false;

            foreach (GameObject slot in forthselectionslots)
            {
                slot.SetActive(true);
            }
        }
        else
        {
            menucontroller.somethinginmenuisopen = false;
            forthcharselectionactive = false;
            forthcharselection.SetActive(false);
        }
        menucontroller.menusoundcontroller.playmenubuttonsound();
    }
}
