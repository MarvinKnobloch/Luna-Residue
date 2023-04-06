using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fasttravelpoints : MonoBehaviour
{
    public GameObject fasttravelcommit;
    public static List<Travelpointvalues> travelpoints = new List<Travelpointvalues>();
    private List<Travelpointvalues> instantiatepoints = new List<Travelpointvalues>();

    [SerializeField] private GameObject travelpointmenuprefab;
    public GameObject mapimage;
    private void OnEnable()
    {
        mapimage.SetActive(false);
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
                var obj = Instantiate(travelpointmenuprefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = travelpoints[i].travelpointname;
                obj.GetComponentInChildren<Openfasttravelcommit>().setfasttravelpoint = travelpoints[i].travelcordinates;
                obj.GetComponentInChildren<Openfasttravelcommit>().setpointonmap = travelpoints[i].fasttravelmapcordinates;
            }
        }
    }
}
