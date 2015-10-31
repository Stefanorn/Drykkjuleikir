using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;


[System.Serializable]
public class YatzeeRule
{
    public string name;
    public string rule;
}

public class RuleHandler : MonoBehaviour
{
    public YatzeeRule[] yatzeeRule = new YatzeeRule[8];
    public Text textBox;
    public Button resetButton;
    public int ReThrows = 1;
    public int reRoll = 1;

    //privat Breytur
    public bool haveAllDiceStop = false;
    int[] diceResults;
    int numOfStopedDice = 0;
    DiceHandler diceHandler;

    // Use this for initialization
    void Start()
    {
        diceHandler = gameObject.GetComponent<DiceHandler>();
        diceResults = new int[diceHandler.maxNoDice];
    }

    public void RoleAgin() //Kallað þegar það er ýtt á rest button
    {

        if (reRoll <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
            /*
            reRoll = reRollCatcher;
            diceHandler.ClearDice();
            for (int i = 0; i < diceResults.Length; i++)
            {
                diceResults[i] = 0;
            }*/
        }
        else
        {
            diceHandler.ClearDice();
            haveAllDiceStop = false;
            reRoll--;
        }
    }
    void FixedUpdate()
    {

        if (haveAllDiceStop) // Þegar allir teningarnir eru búnir að stoppa keyrist þetta
        {
            if (reRoll == 0)
            {
                CallRule();
            }
            resetButton.gameObject.SetActive(true);
        }
        else
        {
            resetButton.gameObject.SetActive(false);
        }
    }

    //Telur alla go sem hafa gefið tagg og skila inn fjölda þeirra
    private int CountTheDice(string tag)
    {
        GameObject[] stopedDice = GameObject.FindGameObjectsWithTag(tag);
        return stopedDice.Length;
    }

    //þegar teningur stöðvast kallar hann á þetta
    public void DiceStopedChecker()
    {
        //taggið dice er sett á dice sem hefur verið valin og takkið nonselected dice kemur á tening sem hefur stöðvast
        numOfStopedDice = CountTheDice("NonSelectedDice") + CountTheDice("Dice");
        if (numOfStopedDice >= diceHandler.maxNoDice)
        {
            haveAllDiceStop = true;
            if (reRoll <= 0)
            {
                for (int i = 0; i <= diceHandler.maxNoDice - 1; i++)
                {
                    diceResults[i] = diceHandler.diceInsts[i].GetComponent<DiceHasStoped>().DiceFace();
                }
            }
        }
    }

    public void CallRule()
    {
        SortArray(diceResults); //RuleChecking algorithmar ganga útfá því að arrayið er sortað

        StartCoroutine(TextEffects.FadeText(textBox, 0.5f));
        if (ResultYatzee())
        {
            textBox.text = yatzeeRule[0].rule;
        }
        else if (ResultLargeStraigt())
        {
            textBox.text = yatzeeRule[1].rule;
        }
        else if (ResultFourOfAKind())
        {
            textBox.text = yatzeeRule[2].rule;
        }
        else if (ResultFullHouse())
        {
            textBox.text = yatzeeRule[3].rule;
        }
        else if (ResultThreeOfAKind())
        {
            textBox.text = yatzeeRule[4].rule;
        }
        else if (ResultSmallStraigt())// tékka á þessu síðast því þetta er þingsti algoritmin
        {
            textBox.text = yatzeeRule[5].rule;
        }
        else if (ResultTwoOfAKind())
        {
            textBox.text = yatzeeRule[6].rule;
        }
        else
        {
            textBox.text = yatzeeRule[7].rule;
        }
    }


    int[] SortArray(int[] array) //Bubble sort
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            for (int u = 0; u < i; u++)
            {
                if (array[u] > array[u + 1])
                {
                    int temp = array[u];
                    array[u] = array[u + 1];
                    array[u + 1] = temp;
                }
            }
        }
        return array;
    }

    bool ResultYatzee()
    {
        if (diceResults[0] == diceResults[1] &&
                diceResults[1] == diceResults[2] &&
                diceResults[2] == diceResults[3] &&
                diceResults[3] == diceResults[4])
        {
            //
            return true;
            //
        }
        return false;
    }
    bool ResultLargeStraigt()
    {
        if (diceResults[0] == 1)
        {
            if (diceResults[1] == 2 &&
                diceResults[2] == 3 &&
                diceResults[3] == 4 &&
                diceResults[4] == 5)
            {
                return true;
            }
        }
        if (diceResults[0] == 2)
        {
            if (diceResults[1] == 3 &&
                    diceResults[2] == 4 &&
                    diceResults[3] == 5 &&
                    diceResults[4] == 6)
            {
                return true;
            }
        }
        return false;
    }
    bool ResultSmallStraigt()  //#ATH Room For optimize?
    {
        int arrayLength = diceResults.Length;

        for (int i = 0; i < arrayLength; i++)
        {
            if (diceResults[i] == 1)
            {
                for (int u = 0; u < arrayLength; u++)
                {
                    if (diceResults[u] == 2)
                    {
                        for (int y = 0; y < arrayLength; y++)
                        {
                            if (diceResults[y] == 3)
                            {
                                for (int t = 0; y < arrayLength; y++)
                                {
                                    if (diceResults[t] == 4)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (diceResults[i] == 2)
            {
                for (int u = 0; u < arrayLength; u++)
                {
                    if (diceResults[u] == 3)
                    {
                        for (int y = 0; y < arrayLength; y++)
                        {
                            if (diceResults[y] == 4)
                            {
                                for (int t = 0; y < arrayLength; y++)
                                {
                                    if (diceResults[t] == 5)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (diceResults[i] == 3)
            {
                for (int u = 0; u < arrayLength; u++)
                {
                    if (diceResults[u] == 4)
                    {
                        for (int y = 0; y < arrayLength; y++)
                        {
                            if (diceResults[y] == 5)
                            {
                                for (int t = 0; y < arrayLength; y++)
                                {
                                    if (diceResults[t] == 6)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    bool ResultFullHouse()
    {
        if (diceResults[0] == diceResults[1] &&
            diceResults[2] == diceResults[3] &&
            diceResults[3] == diceResults[4])
        {
            return true;
        }
        else if (diceResults[0] == diceResults[1] &&
                 diceResults[1] == diceResults[2] &&
                 diceResults[3] == diceResults[4])
        {
            return true;
        }
        return false;
    }
    bool ResultFourOfAKind()
    {
        if (diceResults[0] == diceResults[1] &&
            diceResults[1] == diceResults[2] &&
            diceResults[2] == diceResults[3])
        {
            return true;
        }
        else if (diceResults[1] == diceResults[2] &&
                    diceResults[2] == diceResults[3] &&
                    diceResults[3] == diceResults[4])
        {
            return true;
        }
        return false;
    }
    bool ResultThreeOfAKind()
    {
        for (int i = 0; i < diceResults.Length - 3; i++)
        {
            if (diceResults[i] == diceResults[i + 1] &&
                diceResults[i + 1] == diceResults[i + 2])
            {
                return true;
            }
        }
        return false;
    }
    bool ResultTwoOfAKind()
    {
        for (int i = 0; i < diceResults.Length - 2; i++)
        {
            if (diceResults[i] == diceResults[i + 1])
            {
                return true;
            }
        }
        return false;
    }
}
