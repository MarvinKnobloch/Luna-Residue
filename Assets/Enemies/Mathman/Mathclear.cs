using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mathclear : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI solution;
    public void clearsolution()
    {
        solution.text = "";
    }
}
