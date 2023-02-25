using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemygroup : MonoBehaviour
{
    [SerializeField] private GameObject[] linkedenemies;

    public void tiggerenemies()
    {
        foreach (GameObject obj in linkedenemies)
        {
            obj.GetComponent<Enemymovement>().enemygroupistriggered();
        }
    }
}
