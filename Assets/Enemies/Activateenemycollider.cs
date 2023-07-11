using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activateenemycollider : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform obj in gameObject.transform)
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
