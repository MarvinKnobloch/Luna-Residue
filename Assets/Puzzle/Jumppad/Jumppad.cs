using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MonoBehaviour
{
    [SerializeField] private float jumppadlaunchbasevalue;
    [SerializeField] private float jumppadlaunchheight;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LoadCharmanager.Overallmainchar.gameObject)
        {
            Debug.Log("hallo");
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().jumppad(jumppadlaunchbasevalue + jumppadlaunchheight);
        }
    }
}
