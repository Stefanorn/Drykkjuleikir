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
    public string cardRule; //Þarf ekki að hafa sem public þegar ég er buin aðsenda reglur yfir.

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

    private RefrensToCardImages cardIMG;
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
        cardIMG = this.GetComponent<RefrensToCardImages>();

        chooesnCardIndex = new int[deck.Length];
        source = GetComponent<AudioSource>();
        cardChecker = GameObject.FindGameObjectWithTag("Card");
    }
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

        cardImage.sprite = cardIMG.reverseBackImage; //Stillir spritin á back cover þannig þegar card fer aftur á byrjunar reit og snýst þá er eins og það snúi öfugt
                                      // cardImage.tag = "Card";
        rule.text = deck[randomNumber].cardRule; //velur næstu reglu
        StartCoroutine(TextEffects.FadeText(rule, 0.5f)); // feidar inn regluna
        cardChecker.SetActive(true); //spil situr sjálft sig á false Þetta er trick til að passa að þessi kóði keyri bara 1x
        cardChecker.transform.rotation = startRotation; //Gefur spilinu upprunalega rotationið
        source.volume = Random.Range(0.5f, 1f); // Gefur hljóði smá random í vol
        source.Play(); // Spilar hljóð
        source.pitch = source.clip.length / rotateTimer;
        UpdateCardGFX(deck[randomNumber]);
        StartCoroutine(RotateCard(deck[randomNumber]));

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
            if (card.card == Card.two)
            {
                SetUpImgAndPatern(2);
                UpdateSortImg(card);
            }
            else if (card.card == Card.three)
            {
                SetUpImgAndPatern(3);
                UpdateSortImg(card);
            }
            else if (card.card == Card.four)
            {
                SetUpImgAndPatern(4);
                UpdateSortImg(card);
            }
            else if (card.card == Card.five)
            {
                SetUpImgAndPatern(5);
                UpdateSortImg(card);
            }
            else if (card.card == Card.six)
            {
                SetUpImgAndPatern(6);
                UpdateSortImg(card);
            }
            else if (card.card == Card.seven)
            {
                SetUpImgAndPatern(7);
                UpdateSortImg(card);
            }
            else if (card.card == Card.eight)
            {
                SetUpImgAndPatern(8);
                UpdateSortImg(card);
            }
            else if (card.card == Card.nine)
            {
                SetUpImgAndPatern(9);
                UpdateSortImg(card);
            }
            else if (card.card == Card.ten)
            {
                SetUpImgAndPatern(10);
                UpdateSortImg(card);
            }
        }

    }

    private void SetUpImgAndPatern(int noOnCard)
    {
        for (int i = 0; i < noOnCard; i++)
        {
            imgOnCard[i].gameObject.SetActive(true);
            
        }

        for (int i = 0; i < paterns.Length; i++)
        {
            if(paterns[i].CenterImgPos.Length == noOnCard)
            {
                
                for (int u = 0; u < noOnCard; u++)
                {
                    imgOnCard[u].rectTransform.anchoredPosition = paterns[i].CenterImgPos[u];
                }
                break;
            }
        }
    }

    void SetImgOnCardDisabled(bool everything)
    {
        foreach (Image img in imgOnCard)
        {
            img.gameObject.SetActive(false);
        }
        higCardImageOnCard.gameObject.SetActive(false);

        if (everything)
        {
            foreach (Text num in numberOnCard)
            {
                num.gameObject.SetActive(false);
            }
            foreach (Image suit in sortOnCard)
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
                higCardImageOnCard.sprite = cardIMG.aceSpade;
            }
            else if (card.suit == Suit.dimond)
            {
                higCardImageOnCard.sprite = cardIMG.aceDimond;
            }
            else if (card.suit == Suit.spade)
            {
                higCardImageOnCard.sprite = cardIMG.aceSpade;
            }
            else if (card.suit == Suit.heart)
            {
                higCardImageOnCard.sprite = cardIMG.aceHeart;
            }
        }
        else if (card.card == Card.jack)
        {
            higCardImageOnCard.gameObject.SetActive(true);
            if (card.suit == Suit.clubs)
            {
                higCardImageOnCard.sprite = cardIMG.jackClubs;
            }
            else if (card.suit == Suit.dimond)
            {
                higCardImageOnCard.sprite = cardIMG.jackDimond;
            }
            else if (card.suit == Suit.spade)
            {
                higCardImageOnCard.sprite = cardIMG.jackSpade;
            }
            else if (card.suit == Suit.heart)
            {
                higCardImageOnCard.sprite = cardIMG.jackHeart;
            }
        }
        else if (card.card == Card.queen)
        {
            higCardImageOnCard.gameObject.SetActive(true);
            if (card.suit == Suit.clubs)
            {
                higCardImageOnCard.sprite = cardIMG.queenClubs;
            }
            else if (card.suit == Suit.dimond)
            {
                higCardImageOnCard.sprite = cardIMG.queenDimond;
            }
            else if (card.suit == Suit.spade)
            {
                higCardImageOnCard.sprite = cardIMG.queenSpade;
            }
            else if (card.suit == Suit.heart)
            {
                higCardImageOnCard.sprite = cardIMG.queenHeart;
            }
        }
        else if (card.card == Card.king)
        {
            higCardImageOnCard.gameObject.SetActive(true);
            if (card.suit == Suit.clubs)
            {
                higCardImageOnCard.sprite = cardIMG.kingClubs;
            }
            else if (card.suit == Suit.dimond)
            {
                higCardImageOnCard.sprite = cardIMG.kingDimond;
            }
            else if (card.suit == Suit.spade)
            {
                higCardImageOnCard.sprite = cardIMG.kingSpade;
            }
            else if (card.suit == Suit.heart)
            {
                higCardImageOnCard.sprite = cardIMG.kingHeart;
            }
        }
    }
    private void UpdateNumber(deck card)
    {
        foreach (Text number in numberOnCard)
        {
            number.gameObject.SetActive(true); //þarf ekki að vera kallað í hvert sinn

            if (card.card == Card.ace)
            {
                number.text = "A";
            }
            else if (card.card == Card.two)
            {
                number.text = "2";
            }
            else if (card.card == Card.three)
            {
                number.text = "3";

            }
            else if (card.card == Card.four)
            {
                number.text = "4";
            }
            else if (card.card == Card.five)
            {
                number.text = "5";
            }
            else if (card.card == Card.six)
            {
                number.text = "6";
            }
            else if (card.card == Card.seven)
            {
                number.text = "7";
            }
            else if (card.card == Card.eight)
            {
                number.text = "8";
            }
            else if (card.card == Card.nine)
            {
                number.text = "9";
            }
            else if (card.card == Card.ten)
            {
                number.text = "10";
            }
            else if (card.card == Card.jack)
            {
                number.text = "G";
            }
            else if (card.card == Card.queen)
            {
                number.text = "Q";
            }
            else if (card.card == Card.king)
            {
                number.text = "K";
            }
        }
    }
    private void UpdateSortImg(deck card)
    {

        foreach (Image sort in imgOnCard)
        {
            if (sort.IsActive())
            {
                sort.gameObject.SetActive(true);
                if (card.suit == Suit.heart)
                {
                    sort.sprite = cardIMG.heart;
                }
                else if (card.suit == Suit.spade)
                {
                    sort.sprite = cardIMG.spade;
                }
                else if (card.suit == Suit.dimond)
                {
                    sort.sprite = cardIMG.dimond;
                }
                else if (card.suit == Suit.clubs)
                {
                    sort.sprite = cardIMG.clubs;
                }
            }
        }
    }
    private void UpdateSuit(deck card)
    {

        foreach (Image sort in sortOnCard)
        {
            sort.gameObject.SetActive(true);
            if (card.suit == Suit.heart)
            {
                sort.sprite = cardIMG.heart;
            }
            else if (card.suit == Suit.spade)
            {
                sort.sprite = cardIMG.spade;
            }
            else if (card.suit == Suit.dimond)
            {
                sort.sprite = cardIMG.dimond;
            }
            else if (card.suit == Suit.clubs)
            {
                sort.sprite = cardIMG.clubs;
            }
        }
    }

    IEnumerator RotateCard(deck drawnCard)
    {
        SetImgOnCardDisabled(true);
        float timer = 0;
        Quaternion orgRotation = cardChecker.transform.rotation;
        bool cardHasBeenUpdated = true;
        while (timer < rotateTimer)
        {
            cardChecker.transform.rotation = Quaternion.Lerp(orgRotation, Quaternion.Euler(0, 0, 0), timer / rotateTimer);

            if (timer / rotateTimer >= 0.5)
            {
                if (cardHasBeenUpdated)
                {
                    cardImage.sprite = cardIMG.blankCard;
                    UpdateCardGFX(drawnCard);
                    cardHasBeenUpdated = false;
                    
                }
            }
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
