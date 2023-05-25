using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapiconcontroller : MonoBehaviour
{
    [SerializeField] private Areacontroller areacontroller;

    private List<Travelpointvalues> instantiatepoints = new List<Travelpointvalues>();

    private List<GameObject> questmarker = new List<GameObject>();
    [SerializeField] private Quests[] quests;

    [SerializeField] private GameObject fasttravelicon;
    [SerializeField] private GameObject questicon;

    private float iconoffset = 6;

    private void OnEnable()
    {
        createtravelpoints();
        activatequesticons();
    }
    private void Start()
    {
        createquesticons();
    }
    private void createtravelpoints()
    {
        for (int i = 0; i < Fasttravelpoints.travelpoints.Count; i++)
        {
            if (instantiatepoints.Contains(Fasttravelpoints.travelpoints[i]) == false)
            {
                instantiatepoints.Add(Fasttravelpoints.travelpoints[i]);
                GameObject mapicon = Instantiate(fasttravelicon, Vector2.zero, Quaternion.identity, gameObject.transform);
                float xposi = (Fasttravelpoints.travelpoints[i].travelcordinates.x - iconoffset) * 1.43f;
                float zposi = (Fasttravelpoints.travelpoints[i].travelcordinates.z - 350) * 1.15f;
                mapicon.GetComponent<RectTransform>().anchoredPosition = new Vector2(xposi, zposi);
            }
        }
    }
    private void createquesticons()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            GameObject mapicon = Instantiate(questicon, Vector2.zero, Quaternion.identity, gameObject.transform);
            float xposi = (quests[i].mapvalues.x - iconoffset) * 1.43f;
            float zposi = (quests[i].mapvalues.z - 350) * 1.15f;
            mapicon.GetComponent<RectTransform>().anchoredPosition = new Vector2(xposi, zposi);
            questmarker.Add(mapicon);
        }
        activatequesticons();
    }
    private void activatequesticons()
    {      
        if(questmarker.Count > 0)
        {
            for (int i = 0; i < quests.Length; i++)
            {
                if (quests[i].questactiv == true && quests[i].questcomplete == false)
                {
                    questmarker[i].SetActive(true);
                }
                else
                {
                    questmarker[i].SetActive(false);
                }
            }
        }    
    }
}
