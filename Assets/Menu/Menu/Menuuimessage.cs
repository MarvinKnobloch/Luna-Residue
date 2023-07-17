using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menuuimessage : MonoBehaviour
{
    [SerializeField] private Camera cam;
    

    private void OnEnable()
    {
        Vector2 mouseposi = Input.mousePosition;
        mouseposi.x += 200;
        mouseposi.y += 25;
        transform.position = mouseposi;
        resettimer();
    }

    private void Update()
    {
        Vector2 mouseposi = Input.mousePosition;
        mouseposi.x += 200;
        mouseposi.y += 25;
        transform.position = mouseposi;
    }
    public void resettimer()
    {
        StopAllCoroutines();
        StartCoroutine("disableobject");
    }
    IEnumerator disableobject()
    {
        yield return new WaitForSecondsRealtime(0.9f);
        gameObject.SetActive(false);
    }
}
