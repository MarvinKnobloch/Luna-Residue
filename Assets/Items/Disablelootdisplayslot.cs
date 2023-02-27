using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disablelootdisplayslot : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("toogleoff", 3);
    }
    private void toogleoff()
    {
        gameObject.SetActive(false);
    }
    public void reactivate()
    {
        CancelInvoke();
        Invoke("toogleoff", 3);
    }
}
