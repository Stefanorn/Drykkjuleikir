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
    void Start()
    {
        screenClamp = Screen.width / 5f;
        startPos = transform.position;
        rect = transform.GetComponent<RectTransform>();
        startAncor = rect.pivot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 pivotOffset = new Vector2(eventData.position.x / Screen.width , (eventData.position.y - 50f) / Screen.height );
        rect.pivot = pivotOffset;
    }
    public void OnDrag (PointerEventData eventData)
    {

         transform.position = eventData.position;
    }
    public void OnEndDrag (PointerEventData eventData)
    {
        rect.pivot = startAncor;
        if ( transform.position.x > Screen.width - screenClamp ||
            transform.position.x < 0 + screenClamp ||
            transform.position.y > Screen.height - screenClamp ||
            transform.position.y < 0 + screenClamp )
        {

            Vector2 ratio = new Vector2(transform.position.x - startPos.x, transform.position.y - startPos.y );
            Vector3 offScreenPos = new Vector3( ratio.x * offScreenSpeed + transform.position.x,
                                                ratio.y * offScreenSpeed + transform.position.y ,
                                                transform.position.z );
            StartCoroutine(SmoothAnimation(offScreenPos));
            Invoke("Delay", animTime);
        }

        else 
        {
                StartCoroutine(SmoothAnimation(startPos));
        }
    }
    void Delay()
    {
        gameObject.SetActive(false);
        transform.position = startPos;
    }
    IEnumerator SmoothAnimation(Vector3 targetPos)
    {
        float timer = 0f;  
        while (timer < animTime)
        {
            transform.position = Vector3.Lerp( transform.position , targetPos , timer / animTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }


}
