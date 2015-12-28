using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections; 

public class dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    float animTime = 0.5f;
    float offScreenSpeed = 3;

    float screenClamp = 0f;
    Vector3 startPos;
    RectTransform rect;
    Vector2 startAncor;
    gameLogic gl;
    AudioSource source;
    bool canIDrag = true;

    void Start()
    {
        source = GetComponent<AudioSource>();
        gl = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameLogic>();
        screenClamp = Screen.width / 4f;
        startPos = transform.position;
        rect = transform.GetComponent<RectTransform>();
        startAncor = rect.pivot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canIDrag)
        {
            Vector2 pivotOffset = new Vector2(eventData.position.x / Screen.width, (eventData.position.y - 100f) / Screen.height);
            rect.pivot = pivotOffset;
        }
    }
    public void OnDrag (PointerEventData eventData)
    {
        if (canIDrag)
        {
            transform.position = eventData.position;
            transform.rotation = Quaternion.Euler(0, 0, (startPos.x - transform.position.x) / 16);
        }
    }
    public void OnEndDrag (PointerEventData eventData)
    {
         
        if (canIDrag)
        {
            rect.pivot = startAncor;
            if (transform.position.x > Screen.width - screenClamp ||
                transform.position.x < 0 + screenClamp ||
                transform.position.y > Screen.height - screenClamp ||
                transform.position.y < 0 + screenClamp)
            {
                source.volume = Random.Range(0.5f, 1f);
                source.Play();
                Vector2 ratio = new Vector2(transform.position.x - startPos.x, transform.position.y - startPos.y);
                Vector3 offScreenPos = new Vector3(ratio.x * offScreenSpeed + transform.position.x,
                                                    ratio.y * offScreenSpeed + transform.position.y,
                                                    transform.position.z);
                StartCoroutine(SmoothAnimation(offScreenPos));
                if (!IsInvoking("Delay"))
                {
                    Invoke("Delay", animTime);
                }
            }

            else
            {
                StartCoroutine(SmoothAnimation(startPos));
            }
        }
    }
    void Delay()
    {
        gl.FindAndCallTheNextCard();
        while (transform.position != startPos) //Lagar einhverja skrítna buggu
        {
            transform.position = startPos;
        }
    }
    IEnumerator SmoothAnimation(Vector3 targetPos)
    {
        canIDrag = false;
        float timer = 0f;  
        while (timer < animTime)
        {
            transform.position = Vector3.Lerp( transform.position , targetPos , timer / animTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, timer / animTime);
            timer += Time.deltaTime;
            yield return null;
        }
        canIDrag = true;
    }


}
