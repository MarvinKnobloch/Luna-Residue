using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingamemessagecontroller : MonoBehaviour
{
    [SerializeField] private CanvasGroup messagecanvas;
    [SerializeField] private float fadeouttime;
    private float fadeouttimer;
    private void OnEnable()
    {
        messagecanvas.alpha = 1;
        Invoke("fadeout", 2);
    }
    private void fadeout() => StartCoroutine(startfadeout());

    IEnumerator startfadeout()
    {
        fadeouttimer = fadeouttime;
        while (messagecanvas.alpha > 0.01f)
        {
            fadeouttimer -= Time.deltaTime;
            messagecanvas.alpha = fadeouttimer / fadeouttime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
    public void cancelfadeout()
    {
        CancelInvoke();
        StopAllCoroutines();
        messagecanvas.alpha = 1;
        Invoke("fadeout", 2);
    }
}
