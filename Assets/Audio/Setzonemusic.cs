using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setzonemusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] zonemusic;
    private void Start()
    {
        if(Musiccontroller.instance != null)
        {
            Musiccontroller.instance.allzonesongs = zonemusic;
            Musiccontroller.instance.setcurrentzonemusic(Statics.currentzonemusicint);
        }
    }
}
