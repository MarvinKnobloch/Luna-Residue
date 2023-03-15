using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Closestinteraction : MonoBehaviour
{
    public Transform closestinteraction;
    private Transform closestobj;
    private Transform oldclosestobj;
    private SpielerSteu Steuerung;

    [SerializeField] private GameObject interactionfield;
    [SerializeField] private TextMeshProUGUI interactiontext;

    private void Awake()
    {
        interactionfield.SetActive(false);
        Steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        Steuerung.Enable();
        Detectinteractionobject.enableinteractionfield += enableactionfield;
        Detectinteractionobject.disableinteractionfield += disableactionfield;
        Portal.disableinteractionfield += disableactionfield;
    }
    private void OnDisable()
    {
        Detectinteractionobject.enableinteractionfield -= enableactionfield;
        Detectinteractionobject.disableinteractionfield -= disableactionfield;
        Portal.disableinteractionfield -= disableactionfield;
    }
    void Update()
    {
        if(Statics.interactionobjects.Count != 0)
        {
            closestinteraction = getclosestinteraction();
            if (closestinteraction != null)
            {
                Interactioninterface interactable = closestinteraction.GetComponent<Interactioninterface>();
                interactiontext.text = interactable.Interactiontext;

                if (interactable !=null && Steuerung.Player.Interaction.WasPerformedThisFrame() && LoadCharmanager.gameispaused == false && Statics.infight == false && LoadCharmanager.interaction == false)
                {
                    interactable.Interact(this);
                }
            }
        }
        else
        {
            if(oldclosestobj != null)
            {
                oldclosestobj = null;
            }
        }
    }
    public Transform getclosestinteraction()
    {
        float closestdistance = 10f;

        foreach (GameObject obj in Statics.interactionobjects)
        {
            float currentdistance;
            currentdistance = Vector3.Distance(LoadCharmanager.Overallmainchar.transform.position, obj.transform.position);
            if (currentdistance < closestdistance)
            {
                closestdistance = currentdistance;
                closestobj = obj.transform;
                oldclosestobj = closestobj;
            }
        }
        return closestobj;
    }
    private void enableactionfield()
    {
        interactionfield.SetActive(true);
    }
    private void disableactionfield()
    {
        if(interactionfield != null)
        {
            interactionfield.SetActive(false);
        }
    }
}
