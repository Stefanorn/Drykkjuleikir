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
    public Image higCardImageOnCard;
    public Image[] sortOnCard = new Image[2];
    AudioSource source;

    int[] chooesnCardIndex;
    int cardCounter = 0;
    float cuirtainAlphaFader = 0;
    float levelChangeTimer = 2;
    float fadeTimer = 0;

    void Start()
    {
        SetImgOnCardDisabled(true);
    

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
        UpdateCardGFX(deck[randomNumber]);
        StartCoroutine(RotateCard(deck[randomNumber].cardGFX));

    }
    void BackToMainMenu()
    {
        Application.LoadLevel(0);
    }
    void UpdateCardGFX(deck card)
    {
        SetImgOnCardDisabled(false);

        UpdateSuit(card);
        UpdateNumber(card);
        if (card.card == Card.ace ||
            card.card == Card.jack ||
            card.card == Card.queen ||
            card.card == Card.king)
        {
            UpdateHighCardIMG(card);
        }
        else
        {

        }

    }
    void SetImgOnCardDisabled( bool everything )
    {
        foreach (Image img in imgOnCard)
        {
            img.gameObject.SetActive(false);
        }
        higCardImageOnCard.gameObject.SetActive(false);

        if (everything)
        {
            foreach( Text num in numberOnCard)
            {
                num.gameObject.SetActive(false);
            }
            foreach(Image suit in sortOnCard)
            {
                suit.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateHighCardIMG(deck card)
    {
        if (card.card == Card.ace)
        {
            higCardImageOnCard.gameObject.SetActive(true);
            if (card.suit == Suit.clubs)
            {
                higCardImageOnCard.sprite = highCardIMG[1];
            }
            else if (card.suit == Suit.dimond)
            {
                higCardImageOnCard.sprite = highCardIMG[2];
            }
            else if (card.suit == Suit.spade)
            {
                higCardImageOnCard.sprite = highCardIMG[0];
            }
            else if (card.suit == Suit.heart)
            {
                higCardImageOnCard.sprite = highCardIMG[3];
            }
        }
        else if (card.card == Card.jack)
        {
            higCardImageOnCard.gameObject.SetActive(true);
            if (card.suit == Suit.clubs)
            {
                higCardImageOnCard.sprite = highCardIMG[5];
            }
            else if (card.suit == Suit.dimond)
            {
                higCardImageOnCard.sprite = highCardIMG[8];
            }
            else if (card.suit == Suit.spade)
            {
                higCardImageOnCard.sprite = highCardIMG[7];
            }
            else if (card.suit == Suit.heart)
            {
                higCardImageOnCard.sprite = highCardIMG[6];
            }
        }
        else if (card.card == Card.queen)
        {
            higCardImageOnCard.gameObject.SetActive(true);
            if (card.suit == Suit.clubs)
            {
                higCardImageOnCard.sprite = highCardIMG[10];
            }
            else if (card.suit == Suit.dimond)
            {
                higCardImageOnCard.sprite = highCardIMG[11];
            }
            else if (card.suit == Suit.spade)
            {
                higCardImageOnCard.sprite = highCardIMG[13];
            }
            else if (card.suit == Suit.heart)
            {
                higCardImageOnCard.sprite = highCardIMG[12];
            }
        }
        else if (card.card == Card.king)
        {
            higCardImageOnCard.gameObject.SetActive(true);
            if (card.suit == Suit.clubs)
            {
                higCardImageOnCard.sprite = highCardIMG[14];
            }
            else if (card.suit == Suit.dimond)
            {
                higCardImageOnCard.sprite = highCardIMG[15];
            }
            else if (card.suit == Suit.spade)
            {
                higCardImageOnCard.sprite = highCardIMG[4];
            }
            else if (card.suit == Suit.heart)
            {
                higCardImageOnCard.sprite = highCardIMG[9];
            }
        }
    }
    private void UpdateNumber(deck card)
    {
        if (card.card == Card.ace)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "A";
            }
        }
        else if (card.card == Card.two)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "2";
            }
        }
        else if (card.card == Card.three)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "3";
            }
        }
        else if (card.card == Card.four)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "4";
            }
        }
        else if (card.card == Card.five)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "5";
            }
        }
        else if (card.card == Card.six)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "6";
            }
        }
        else if (card.card == Card.seven)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "7";
            }
        }
        else if (card.card == Card.eight)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "8";
            }
        }
        else if (card.card == Card.nine)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "9";
            }
        }
        else if (card.card == Card.ten)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "10";
            }
        }
        else if (card.card == Card.jack)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "G";
            }
        }
        else if (card.card == Card.queen)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "Q";
            }
        }
        else if (card.card == Card.king)
        {
            foreach (Text number in numberOnCard)
            {
                number.text = "K";
            }
        }
    }
    private void UpdateSuit(deck card)
    {
        foreach (Image img in imgOnCard)
        {
            img.gameObject.SetActive(false);
        }

        if (card.suit == Suit.heart)
        {
            foreach (Image sort in sortOnCard)
            {
                sort.sprite = suits[3];
            }
        }
        else if (card.suit == Suit.spade)
        {
            foreach (Image sort in sortOnCard)
            {
                sort.sprite = suits[0];
            }
        }
        else if (card.suit == Suit.dimond)
        {
            foreach (Image sort in sortOnCard)
            {
                sort.sprite = suits[2];
            }
        }
        else if (card.suit == Suit.clubs)
        {
            foreach (Image sort in sortOnCard)
            {
                sort.sprite = suits[1];
            }
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
