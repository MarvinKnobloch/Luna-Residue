using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elkspezial : MonoBehaviour
{
    [SerializeField] private GameObject elkcontroller;
    [SerializeField] private GameObject elkcircle;
    [SerializeField] private GameObject elkcone;

    const string spezialattackstate = "Spezialattack";

    public void elkspezial()
    {
        elkcircle.transform.position = transform.position;
        elkcontroller.SetActive(true);
    }
    private void spezialanipart2()
    {
        gameObject.GetComponent<Enemymovement>().ChangeAnimationState(spezialattackstate);
    }
    private void castelkcone()
    {
        elkcone.transform.position = transform.position + transform.forward * 10;
        elkcone.transform.rotation = transform.rotation * Quaternion.Euler(90, 90, 0);
        elkcone.SetActive(true);
    }
}
