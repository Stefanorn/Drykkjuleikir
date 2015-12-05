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

    AudioSource source;

    int[] chooesnCardIndex;
    int cardCounter = 0;
    float cuirtainAlphaFader = 0;
    float levelChangeTimer = 2;
    float fadeTimer = 0;

    // Use this for initialization
    void Start()
    {
        chooesnCardIndex = new int[deck.Length];
        source = GetComponent<AudioSource>();
        cardChecker = GameObject.FindGameObjectWithTag("Card");
    }

    // Update is called once per frame
    public void FindAndCallTheNextCard()
    {
        if (cardCounter == deck.Length) //Þegar búnkinn er búinn þá er alltaf kallað return og kallað á fall sem fer aftur í mainmenu
        {
            GameObject.FindGameObjectWithTag("Finish").GetComponent<MainMenuScrip>().FadeGameObjectAndLoad("MENU");
            return;
        }

        int randomNumber = Random.Range(0, deck.Length - 1);//Velur Random tölu 
        foreach (int index in chooesnCardIndex) //Tékkar hvort að þessi random tala hafi komið fyrir áður og dregur þá aftur random tölu
        {
            if (index == randomNumber)
            {
                randomNumber = Random.Range(0, deck.Length - 1);
            }
        }
        chooesnCardIndex[cardCounter] = randomNumber;
        cardCounter++;

        cardImage.sprite = backCover; //Stillir spritin á back cover þannig þegar card fer aftur á byrjunar reit og snýst þá er eins og það snúi öfugt
                                      // cardImage.tag = "Card";
        rule.text = deck[randomNumber].cardRule; //velur næstu reglu
        StartCoroutine(TextEffects.FadeText(rule, 0.5f)); // feidar inn regluna
        cardChecker.SetActive(true); //spil situr sjálft sig á false Þetta er trick til að passa að þessi kóði keyri bara 1x
        cardChecker.transform.rotation = startRotation; //Gefur spilinu upprunalega rotationið
        source.volume = Random.Range(0.5f, 1f); // Gefur hljóði smá random í vol
        source.Play(); // Spilar hljóð
        source.pitch = source.clip.length / rotateTimer;
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
