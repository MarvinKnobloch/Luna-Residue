using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuereset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Statue"))
        {
            other.gameObject.GetComponent<Statuecontroller>().resetstatues();
        }
    }
}
