using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limitfps : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 166;
    }
}
