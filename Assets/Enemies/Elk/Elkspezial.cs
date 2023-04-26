using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elkspezial : MonoBehaviour
{
    [SerializeField] private Elkcontroller spezialcontroller;
    private GameObject elkcircleobj;
    private GameObject elkconeobj;
    private Elkcircle elkcircle;

    const string spezialattackstate = "Spezialattack";

    private Enemyspezialsound enemyspezialsound;
    private void Awake()
    {
        enemyspezialsound = spezialcontroller.GetComponentInParent<Enemyspezialsound>();
        elkcircleobj = spezialcontroller.transform.GetChild(0).gameObject;
        elkconeobj = spezialcontroller.transform.GetChild(1).gameObject;
        elkcircle = elkcircleobj.GetComponent<Elkcircle>();
    }
    public void elkspezial()
    {
        elkcircleobj.transform.position = transform.position;
        spezialcontroller.gameObject.SetActive(true);
        elkcircleobj.SetActive(true);
    }
    private void spezialanipart2()
    {
        gameObject.GetComponent<Enemymovement>().ChangeAnimationState(spezialattackstate);
    }
    private void dealelkcircledmg()
    {
        elkcircle.dealdmg();
        elkcircleobj.SetActive(false);
    }
     
    private void castelkcone()
    {
        elkconeobj.transform.position = transform.position + transform.forward * 10;
        elkconeobj.transform.rotation = transform.rotation * Quaternion.Euler(90, 90, 0);
        elkconeobj.SetActive(true);
    }
    private void dealelkconedmg() => elkconeobj.transform.GetChild(0).gameObject.SetActive(true);
    private void elkspezialstartaudio() => enemyspezialsound.playelkspezialstart();
    private void elkspezialmidaudio() => enemyspezialsound.playelkspezialmid();
    private void elkspezialendaudio() => enemyspezialsound.playelkspezialend();
}
