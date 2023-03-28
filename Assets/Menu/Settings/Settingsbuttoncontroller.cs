using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settingsbuttoncontroller : MonoBehaviour
{
    [SerializeField] private GameObject openbuttonobj;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void buttonobjopen()
    {
        openbuttonobj.SetActive(true);
        audioSource.Play();
    }
    public void buttonobjclose()
    {
        openbuttonobj.SetActive(false);
    }
}
