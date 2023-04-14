using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fasttravelpoints : MonoBehaviour
{
    public GameObject fasttravelcommit;
    public static List<Travelpointvalues> travelpoints = new List<Travelpointvalues>();
    private List<Travelpointvalues> instantiatepoints = new List<Travelpointvalues>();
    [SerializeField] private GameObject fasttravelicon;
    [SerializeField] private RectTransform playerposi;
    public GameObject travelpointnametext;

    private float playeroffset = 6;
    private float iconoffset = 6;
    private void OnEnable()
    {
        travelpointnametext.SetActive(false);
        fasttravelcommit.SetActive(false);
        createtravelpointmenu();
        float xposi = (LoadCharmanager.Overallmainchar.transform.position.x - playeroffset) * 1.43f;
        float zposi = (LoadCharmanager.Overallmainchar.transform.position.z - 350) * 1.15f;
        playerposi.anchoredPosition = new Vector2(xposi, zposi);
    }
    private void createtravelpointmenu()
    {
        for (int i = 0; i < travelpoints.Count; i++)
        {
            if (instantiatepoints.Contains(travelpoints[i]) == false)
            {
                instantiatepoints.Add(travelpoints[i]);
                GameObject mapicon = Instantiate(fasttravelicon, Vector2.zero, Quaternion.identity, gameObject.transform);
                float xposi = (travelpoints[i].travelcordinates.x - iconoffset) * 1.43f;
                float zposi = (travelpoints[i].travelcordinates.z - 350) * 1.15f;
                mapicon.GetComponent<RectTransform>().anchoredPosition = new Vector2(xposi, zposi);
                mapicon.GetComponent<Openfasttravelcommit>().travelpoint = travelpoints[i];
            }
        }
    }
}
