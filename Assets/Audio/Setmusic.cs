using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setmusic : MonoBehaviour
{
    [SerializeField] private AudioClip audioclip;
    private void Start()
    {
        if(Musiccontroller.instance != null)
        {
            Musiccontroller.instance.setclip(audioclip);
        }
    }
}
