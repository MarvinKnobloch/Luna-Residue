using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatecollidercontroller : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform obj in gameObject.transform)                     //disable bei load wenn mainchar in der nähe ist, ist in enemyhp. triggerweaponswitch in Loadcharmanager verhindert den weaponswitch
        {
            obj.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.triggercollider)
        {
            foreach (Transform obj in gameObject.transform)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LoadCharmanager.triggercollider)
        {
            foreach (Transform obj in gameObject.transform)
            {
                obj.gameObject.SetActive(false);
                if (Infightcontroller.infightenemylists.Contains(obj.gameObject))
                {
                    Infightcontroller.infightenemylists.Remove(obj.transform.gameObject);
                    Infightcontroller.instance.checkifinfight();
                }
            }
        }
        /*if (other.gameObject == LoadCharmanager.Overallmainchar && Statics.donttriggerenemies == false)
        {
            foreach (Transform obj in gameObject.transform)
            {
                obj.gameObject.SetActive(false);
            }
        }*/
    }
}
