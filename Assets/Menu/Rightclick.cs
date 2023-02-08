using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Rightclick : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onRigthClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            onRigthClick.Invoke();
    }
}
