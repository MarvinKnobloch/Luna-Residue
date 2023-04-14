using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapcontroller : MonoBehaviour
{
    [SerializeField] private RectTransform minimaprecttransform;
    [SerializeField] private RectTransform minimapplayerrecttransform;
    [SerializeField] private RectTransform mapplayerrecttransform;
    private float updatetimer;

    private void OnEnable()
    {
        float xposi = LoadCharmanager.Overallmainchar.transform.position.x * 1.41f;
        float zposi = (LoadCharmanager.Overallmainchar.transform.position.z - 350) * 1.16f;
        minimapplayerrecttransform.anchoredPosition = new Vector2(xposi, zposi);
        minimapplayerrecttransform.rotation = Quaternion.Euler(0, 0, LoadCharmanager.Overallmainchar.transform.localRotation.eulerAngles.y * -1);
        minimaprecttransform.anchoredPosition = new Vector2(xposi * -1, zposi * -1);
    }
    void Update()
    {
        updatetimer += Time.deltaTime;
        if(updatetimer > 0.05f)
        {  
            updatetimer = 0;
            float xposi = LoadCharmanager.Overallmainchar.transform.position.x * 1.41f;
            float zposi = (LoadCharmanager.Overallmainchar.transform.position.z - 350) * 1.16f;
            minimapplayerrecttransform.anchoredPosition = new Vector2(xposi, zposi);
            minimapplayerrecttransform.rotation = Quaternion.Euler(0, 0, LoadCharmanager.Overallmainchar.transform.localRotation.eulerAngles.y * -1);
            minimaprecttransform.anchoredPosition = new Vector2(xposi * -1, zposi * -1);
            if (mapplayerrecttransform.gameObject.activeSelf == true)
            {
                mapplayerrecttransform.anchoredPosition = new Vector2(xposi, zposi);
                mapplayerrecttransform.rotation = Quaternion.Euler(0, 0, LoadCharmanager.Overallmainchar.transform.localRotation.eulerAngles.y * -1);
            }
            
        }
    }
}
//recttransform.rotation = Quaternion.Euler(0, 0, cam.localRotation.eulerAngles.y * -1);
