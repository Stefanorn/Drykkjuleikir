using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class deck
{
    public string name;
    public Sprite cardGFX;
    public string cardRule;
}

public class gameLogic : MonoBehaviour {

    public deck[] deck;

    GameObject cardChecker;
    public Text  rule;
    public Image cardImage;
    public Sprite backCover;
    public float rotateTimer = 1f;
    public Quaternion startRotation;

	// Use this for initialization
	void Start () {
        cardChecker = GameObject.FindGameObjectWithTag("Card");
	}
	
	// Update is called once per frame
	void Update () {
        if (cardChecker.active == false)
        {
            int randomNumber = Random.Range(0, deck.Length - 1);

            cardImage.sprite = backCover;
            cardImage.tag = "Card";
            rule.text = deck[randomNumber].cardRule;
            cardChecker.SetActive(true);
            cardChecker.transform.rotation = startRotation;
            StartCoroutine(RotateCard(deck[randomNumber].cardGFX));
        }
	
	}
    IEnumerator RotateCard(Sprite drawnCard)
    {
       float timer = 0;
        Quaternion orgRotation = cardChecker.transform.rotation;
        while(timer < rotateTimer)
        {
            cardChecker.transform.rotation = Quaternion.Lerp(orgRotation, Quaternion.Euler(0,0,0), timer/rotateTimer );
            Debug.Log(timer / rotateTimer);
            if(timer / rotateTimer >= 0.5)
            {
                cardImage.sprite = drawnCard;
            }
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
