using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fasttravelpoints : MonoBehaviour
{
    public GameObject fasttravelcommit;
    private List<Travelpointvalues> travelpoints = new List<Travelpointvalues>();
    [SerializeField] Travelpointvalues[] travelpointvalues;

    [SerializeField] private GameObject travelpointmenuprefab;
    public int x_space_between_items;
    public int xStart;
    public int y_space_between_items;
    public int yStart;
    public int numberofcolumn;

    private void Awake()
    {
        addtravelpoints();
        createtravelpointmenu();
    }
    private void OnEnable()
    {
        fasttravelcommit.SetActive(false);
    }
    private void addtravelpoints()
    {
        for (int i = 0; i < travelpointvalues.Length; i++)
        {
            travelpoints.Add(travelpointvalues[i]);
        }
    }
    private void createtravelpointmenu()
    {
        for (int i = 0; i < travelpoints.Count; i++)
        {
            var obj = Instantiate(travelpointmenuprefab, Vector3.zero, Quaternion.identity, transform);
            //obj.GetComponent<RectTransform>().localPosition = getinventoryposi(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = travelpoints[i].travelpointname;
            obj.GetComponentInChildren<Openfasttravelcommit>().setfasttravelpoint = travelpoints[i].travelcordinates;
        }
    }
    private Vector3 getinventoryposi(int i)
    {
        return new Vector3(xStart + (x_space_between_items * (i % numberofcolumn)), yStart + (-y_space_between_items * (i / numberofcolumn)), 0f);
    }
}
