using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GrabAndSpinn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public float drag = 50;
    public float numOfPins = 14;
    public float spinStopingPoint = 0.5f;
    public Text textBox;
    public string[] rules;

    RectTransform rect;
    Vector3 startPointerPos, endPointerPos;
    float dragDist;
    bool gameHasStarted = false;
    float initialDrag; //Notað til að flaskan stoppar smoothly á réttum tíma

    void Start()
    {
        rect = transform.GetComponent<RectTransform>();
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            dragDist = 0;
        }

        if (gameHasStarted)
        {
            rect.Rotate(0, 0, dragDist);
            dragDist = dragDist * drag;
            if(dragDist < spinStopingPoint)
            {
                dragDist -=  spinStopingPoint / initialDrag;
            }
    
        }
        if (dragDist <= 0)
        {
            if (gameHasStarted)
            {
                gameHasStarted = false; //sér til þess að þetta er kallað bara 1x þrátt fyrir að vera í updateloop

                if (Application.loadedLevel == 3) // Ef þetta er lukkuhjól veldu reglu eftir hvernig spjaldið snýr
                {
                    textBox.text = rules[NumberChooser()];
                }
                else // Ef þetta er flöskustútur veldu þá reglu að handahófi
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
        Ray2D ray = new Ray2D(startPointerPos, eventData.position - (Vector2)startPointerPos);
        Debug.Log(ray.direction );

        dragDist =  Vector3.Distance(startPointerPos, eventData.position);
        if (dragDist > 10)
        {
            dragDist = 10;
        }




        gameHasStarted = true;
        initialDrag = dragDist;
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
