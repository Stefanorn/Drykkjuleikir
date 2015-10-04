using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GrabAndSpinn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public float drag = 50;
    public float numOfPins = 14;
    public Text textBox;
    public string[] rules;

    RectTransform rect;
    Vector3 startPointerPos, endPointerPos;
    float dragDist;
    bool gameHasStarted = false;

    void Start()
    {
        rect = transform.GetComponent<RectTransform>();
    }
    void FixedUpdate()
    {

        if (gameHasStarted)
        {
            rect.Rotate(0, 0, dragDist);
            dragDist -= Time.deltaTime * drag;
        }
        if (dragDist <= 0)
        {
            if (gameHasStarted)
            {
                gameHasStarted = false;

                if (Application.loadedLevel == 3)
                {
                    textBox.text = rules[NumberChooser()];
                }
                else
                {
                    textBox.text = rules[Random.Range(0, rules.Length - 1)];
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPointerPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragDist = Vector3.Distance(startPointerPos, eventData.position) / 2;
        gameHasStarted = true;
    }


    int NumberChooser()
    {
        float returnNumber;

        returnNumber = transform.rotation.z * numOfPins;

        if (returnNumber < 0)
        {
            returnNumber *= -1;
        }
        return (int)returnNumber;
    }
}
