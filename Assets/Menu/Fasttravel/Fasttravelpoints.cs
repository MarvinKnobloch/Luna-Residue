using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fasttravelpoints : MonoBehaviour
{
    public GameObject fasttravelcommit;
    public static List<Travelpointvalues> travelpoints = new List<Travelpointvalues>();
    private List<Travelpointvalues> instantiatepoints = new List<Travelpointvalues>();
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject fasttravelicon;

    [SerializeField] private GameObject travelpointmenuprefab;
    //public GameObject mapimage;
    private void OnEnable()
    {
        //mapimage.SetActive(false);
        fasttravelcommit.SetActive(false);
        createtravelpointmenu();
    }
    private void createtravelpointmenu()
    {

        for (int i = 0; i < travelpoints.Count; i++)
        {
            if (instantiatepoints.Contains(travelpoints[i]) == false)
            {
                instantiatepoints.Add(travelpoints[i]);
                //var obj = Instantiate(travelpointmenuprefab, Vector3.zero, Quaternion.identity, transform);
                //obj.GetComponentInChildren<TextMeshProUGUI>().text = travelpoints[i].travelpointname;
                //obj.GetComponentInChildren<Openfasttravelcommit>().setfasttravelpoint = travelpoints[i].travelcordinates;
                //obj.GetComponentInChildren<Openfasttravelcommit>().setpointonmap = travelpoints[i].fasttravelmapcordinates;
                GameObject mapicon = Instantiate(fasttravelicon, Vector2.zero, Quaternion.identity, map.transform);
                float xposi = travelpoints[i].travelcordinates.x * 1.41f;
                float zposi = (travelpoints[i].travelcordinates.z - 350) * 1.16f;
                mapicon.GetComponent<RectTransform>().anchoredPosition = new Vector2(xposi, zposi);
                mapicon.GetComponent<Openfasttravelcommit>().travelpoint = travelpoints[i];
            }
        }
    }
}
