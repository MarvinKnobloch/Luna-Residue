using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elementaltutorialstart : MonoBehaviour, Eventinterface
{
    [SerializeField] private GameObject elementaltutorial;

    private void OnEnable()
    {
        if(Statics.elementalmenuunlocked == true)
        {
            gameObject.SetActive(false);
        }
    }
    public void eventstart()
    {
        elementaltutorial.SetActive(true);
        gameObject.SetActive(false);
    }
}
