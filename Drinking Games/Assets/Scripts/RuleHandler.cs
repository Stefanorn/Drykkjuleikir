﻿using UnityEngine;
using System.Collections;
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
    public string[] rules;

    int[] diceResults;
    int dC = 0;
    DiceHandler diceHandler;

    // Use this for initialization
    void Start()
    {
        diceHandler = gameObject.GetComponent<DiceHandler>();
        diceResults = new int[diceHandler.maxNoDice];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (diceResults[diceResults.Length - 1] != 0)
        {
            diceHandler.ClearDice();
            CallRule(); 
            //textBox.text = rules[diceResults[0] - 1];
            for (int i = 0; i < diceResults.Length; i++) //Núlla gögnin í diceResult vegna þess að ég tékka hvort að það sé eitthvað annað enn 0 í því þegar ég cleara teningana
            {
                diceResults[i] = 0;

            }
            dC = 0;
        }
    }
    public void GetDiceResult(int diceNumber)
    {
        diceResults[dC] = diceNumber;
        dC++;
    }

    public void CallRule()
    {
        SortArray(diceResults); //RuleChecking algorithmar ganga útfá því að arrayið er sortað

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
        else if( ResultThreeOfAKind())
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
                 diceResults[3] == diceResults[4]  )
        {
            return true;
        }
        return false;
    }
    bool ResultFourOfAKind()
    {
        if( diceResults[0] == diceResults[1] &&
            diceResults[1] == diceResults[2] &&
            diceResults[2] == diceResults[3])
        {
            return true;
        }
        else if(    diceResults[1] == diceResults[2] &&
                    diceResults[2] == diceResults[3] &&
                    diceResults[3] == diceResults[4])
        {
            return true;
        }
        return false;
    }
    bool ResultThreeOfAKind()
    {
        for(int i = 0; i < diceResults.Length - 3; i++)
        {
            if( diceResults[i] == diceResults[i+1] &&
                diceResults[i+1] == diceResults[i + 2]  )
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


    void ResultLargeStraigtFromNonSorted()
    {
        int arrayLength = diceResults.Length;
        for (int i = 0; i < diceResults.Length; i++) //Rúllar í gegnum alla teningana og leitra að langri röð
        {
            if (diceResults[i] == 1) //Tékkar hvort þú fékkst 1 og leitar þá að 2
            {
                for (int u = 0; u < arrayLength; u++)
                {
                    if (diceResults[u] == 2) // tékkar hvort að það sé enhverstaðra 2 og leitar þá að 3
                    {
                        for (int y = 0; y < arrayLength; y++)
                        {
                            if (diceResults[y] == 3) // tékkar hvort að það sé enhverstaðar 3 og leitar þá að 4
                            {
                                for (int t = 0; t < arrayLength; t++)
                                {
                                    if (diceResults[t] == 4) // tékkar hvort að það sé 4 enhverstaðar og leitar þá að 5
                                    {
                                        for (int r = 0; r < arrayLength; r++)
                                        {
                                            if (diceResults[r] == 5)
                                            {
                                                Debug.Log("þú fékkst röð frá 1-5");
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (diceResults[i] == 2) //tékkar hvort að þú fékkst 2 einhverstaðar og leitar þá að 3
            {
                for (int u = 0; u < arrayLength; u++)
                {
                    if (diceResults[u] == 3)
                    {
                        for (int y = 0; y < arrayLength; y++)
                        {
                            if (diceResults[y] == 4)
                            {
                                for (int t = 0; t < arrayLength; t++)
                                {
                                    if (diceResults[t] == 5)
                                    {
                                        for (int r = 0; t < arrayLength; r++)
                                        {
                                            if (diceResults[r] == 6)
                                            {
                                                Debug.Log("Þú fékkst roð frá 2-6");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
