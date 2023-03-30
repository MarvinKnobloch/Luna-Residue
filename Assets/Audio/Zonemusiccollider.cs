using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zonemusiccollider : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LoadCharmanager.Overallmainchar.gameObject && Musiccontroller.instance.currentzonemusic != music)
        {
            if(Musiccontroller.instance.oldzonemusic != music)
            {
                Musiccontroller.instance.enternewzone(music);              
            }
            else
            {
                Musiccontroller.instance.enteroldzone(music);
            }
        }
    }
}
