using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mathclear : MonoBehaviour
{
    [SerializeField] private Text solution;
    public void clearsolution()
    {
        solution.text = "";
    }
}
