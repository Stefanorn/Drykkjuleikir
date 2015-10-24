using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class OnClickedDice : MonoBehaviour, IBeginDragHandler
{
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        gameObject.tag = "Dice";
    }
}
