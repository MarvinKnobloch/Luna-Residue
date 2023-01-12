using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Leftrightclick : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onRigthClick;
    void Start()
    { }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            onRigthClick.Invoke();
    }
}
