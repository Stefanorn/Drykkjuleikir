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
    bool rotationCheckDelayer = false;

    Rigidbody2D rb;
    void Start()
    {
        rect = transform.GetComponent<RectTransform>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        // Kóði sem sendir allar reglur í textaskjal
        //     RuleReader.WriteRules(rules);

    }
    void FixedUpdate()
    {

        if (Input.GetKeyDown("space"))
        {
            rb.angularVelocity = 0;
        }
        if (rb.angularVelocity == 0)
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
        Debug.Log(rb.angularVelocity);
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
        Vector3 spinVector = startPointerPos - eventData.position;
        rb.AddTorque(-(spinVector.x + spinVector.y ) );
        Invoke("GameHasStarted", 0.5f);
    }
    public void GameHasStarted() //Hack to delay the 
    {
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
