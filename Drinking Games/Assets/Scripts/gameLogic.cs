using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Paterns
{
    public string name;
    public Vector2[] CenterImgPos;
}
[System.Serializable]
public class deck
{
    public string name;
    public Sprite cardGFX;
    public string cardRule;

    public Suit suit;
    public Card card;
}
public enum Suit
{
    spade,
    heart,
    clubs,
    dimond
}
public enum Card
{
    ace,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
    ten,
    jack,
    queen,
    king
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

    public string[] numbers;
    public Color[] sortColor;
    public Sprite[] suits;
    public Sprite[] highCardIMG;
    public Paterns[] paterns;

    public Text[] numberOnCard = new Text[2];
    public Image[] imgOnCard = new Image[11];
    public Image[] sortOnCard = new Image[2];
    AudioSource source;

    int[] chooesnCardIndex;
    int cardCounter = 0;
    float cuirtainAlphaFader = 0;
    float levelChangeTimer = 2;
    float fadeTimer = 0;

    void Start()
    {
        chooesnCardIndex = new int[deck.Length];
        source = GetComponent<AudioSource>();
        cardChecker = GameObject.FindGameObjectWithTag("Card");
    }
    public void FindAndCallTheNextCard()
    {
        if (cardCounter == deck.Length) //�egar b�nkinn er b�inn �� er alltaf kalla� return og kalla� � fall sem fer aftur � mainmenu
        {
            GameObject.FindGameObjectWithTag("Finish").GetComponent<MainMenuScrip>().FadeGameObjectAndLoad("MENU");
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
        source.volume = Random.Range(0.5f, 1f); // Gefur hlj��i sm� random � vol
        source.Play(); // Spilar hlj��
        source.pitch = source.clip.length / rotateTimer;
        StartCoroutine(RotateCard(deck[randomNumber].cardGFX));

    }
    void BackToMainMenu()
    {
        Application.LoadLevel(0);
    }
    void UpdateCardGFX(deck card)
    {
        foreach(Image img in imgOnCard)
        {
            img.gameObject.SetActive(false);
        }
        if(card.suit == Suit.heart)
        {

        }
        else if (card.suit == Suit.spade)
        {

        }
        else if (card.suit == Suit.dimond)
        {

        }
        else if (card.suit == Suit.clubs)
        {

        }


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
