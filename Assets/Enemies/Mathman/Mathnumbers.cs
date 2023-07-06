using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mathnumbers : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI solution;
    [SerializeField] private TextMeshProUGUI number;

    public void setnumber()
    {
        solution.text += number.text;
    }
}
