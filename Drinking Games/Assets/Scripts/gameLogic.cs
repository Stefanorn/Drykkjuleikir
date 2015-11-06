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

public class gameLogic : MonoBehaviour
{

    public deck[] deck;

    GameObject cardChecker;
    public Text rule;
    public Image cardImage;
    public Image curtains;
    public Sprite backCover;
    public float rotateTimer = 1f;
    public Quaternion startRotation;

    AudioSource audio;
    int[] chooesnCardIndex = new int[150]; ///HAR�K��UN!!! �arf a� gera grein fyrir deck.length
    int cardCounter = 0;
    float cuirtainAlphaFader = 0;
    float levelChangeTimer = 2;
    float fadeTimer = 0;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        cardChecker = GameObject.FindGameObjectWithTag("Card");
    }

    // Update is called once per frame
    public void FindAndCallTheNextCard()
    {
        if (cardCounter == deck.Length) //�egar b�nkinn er b�inn �� er alltaf kalla� return og kalla� � fall sem fer aftur � mainmenu
        {

            curtains.gameObject.SetActive(true); //TODO taka �ennan k��a �t og gera static Klassa
            curtains.color = new Color(255, 255, 255, cuirtainAlphaFader);
            cuirtainAlphaFader = Mathf.Lerp(0, 1, fadeTimer / levelChangeTimer);
            fadeTimer += Time.deltaTime;
            if (!IsInvoking("BackToMainMenu"))
            {
                Invoke("BackToMainMenu", 2);
            }
            return;
        }

        int randomNumber = Random.Range(0, deck.Length - 1);//Velur Random t�lu 
        foreach (int index in chooesnCardIndex) //T�kkar hvort a� �essi random tala hafi komi� fyrir ��ur og dregur �� aftur random t�lu
        {
            if (index == randomNumber)
            {
                randomNumber = Random.Range(0, deck.Length - 1);
            }
        }
        chooesnCardIndex[cardCounter] = randomNumber;
        cardCounter++;

        cardImage.sprite = backCover; //Stillir spritin � back cover �annig �egar card fer aftur � byrjunar reit og sn�st �� er eins og �a� sn�i �fugt
                                      // cardImage.tag = "Card";
        rule.text = deck[randomNumber].cardRule; //velur n�stu reglu
        StartCoroutine(TextEffects.FadeText(rule, 0.5f)); // feidar inn regluna
        cardChecker.SetActive(true); //spil situr sj�lft sig � false �etta er trick til a� passa a� �essi k��i keyri bara 1x
        cardChecker.transform.rotation = startRotation; //Gefur spilinu upprunalega rotationi�
        audio.volume = Random.Range(0.5f, 1f); // Gefur hlj��i sm� random � vol
        audio.Play(); // Spilar hlj��
        audio.pitch = audio.clip.length / rotateTimer;
        StartCoroutine(RotateCard(deck[randomNumber].cardGFX));


    }
    void BackToMainMenu()
    {
        Application.LoadLevel(0);
    }
    IEnumerator RotateCard(Sprite drawnCard)
    {
        float timer = 0;
        Quaternion orgRotation = cardChecker.transform.rotation;
        while (timer < rotateTimer)
        {
            cardChecker.transform.rotation = Quaternion.Lerp(orgRotation, Quaternion.Euler(0, 0, 0), timer / rotateTimer);
            if (timer / rotateTimer >= 0.5)
            {
                cardImage.sprite = drawnCard;
            }
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
