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
    Vector2 startPointerPos, endPointerPos;
    float dragDist;
    bool gameHasStarted = false;
    float initialDrag; //Notað til að flaskan stoppar smoothly á réttum tíma
    bool InversSpinCherker = false;

    void Start()
    {
        rect = transform.GetComponent<RectTransform>();
        
        // Kóði sem sendir allar reglur í textaskjal
        //     RuleReader.WriteRules(rules);

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
        if (dragDist < 0)
        {
            if (gameHasStarted)
            {
                gameHasStarted = false; //sér til þess að þetta er kallað bara 1x þrátt fyrir að vera í updateloop

                if (Application.loadedLevelName == "Lukkuhjol2.0") // Ef þetta er lukkuhjól veldu reglu eftir hvernig spjaldið snýr
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
        Vector2 endPointerPos = eventData.position;
        Ray2D ray = new Ray2D(startPointerPos, endPointerPos - startPointerPos);
        Debug.Log(ray.direction );


        dragDist =  Vector3.Distance(startPointerPos, endPointerPos) * (ray.direction.x - ray.direction.y);
        if (dragDist > 75)
        {
            dragDist = 75;
        }

      /*  if(endPointerPos.x < startPointerPos.x)
        {
            dragDist *= -1;
        }
        if(endPointerPos.y > startPointerPos.y)
        {
            dragDist *= -1;
        }
        if((endPointerPos.y - startPointerPos.y) < (endPointerPos.x - startPointerPos.x)  )
        {
            dragDist *= -1;
        }
        */
        if(dragDist < 0)
        {
            InversSpinCherker = true;
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
