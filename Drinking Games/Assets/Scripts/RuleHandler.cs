using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class RuleHandler : MonoBehaviour {

    public Text textBox;
    public string[] rules;

    int[] diceResults;
    int dC = 0;
    DiceHandler diceHandler;

	// Use this for initialization
	void Start () {
        diceHandler = gameObject.GetComponent<DiceHandler>();
        diceResults = new int[diceHandler.maxNoDice];
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(diceResults[diceResults.Length-1] != 0)
        {
            
            diceHandler.ClearDice();
            //CallRule(); 
            textBox.text = rules[diceResults[0]-1];
            for(int i = 0;i < diceResults.Length ; i++) //Núlla gögnin í diceResult vegna þess að ég tékka hvort að það sé eitthvað annað enn 0 í því þegar ég cleara teningana
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
        ResultYatzee();
        ResultLargeStraigt();
        ResultSmallStraigt();



        for(int i = 0; i < diceResults.Length ; i++) 
        {
            int u = i + 1;
            if(u > diceResults.Length -1 ) //passar að u fari aldrei útfyrir arrayin
            {
                u = i;
            }
                
            if(diceResults[i] == diceResults[u]) //Tékkar á tvennum
            {
                Debug.Log("Tvenna " + diceResults[i] + " og " + diceResults[u]);
            }

            if(diceResults[i] == 7)
            {
                Debug.Log("VILLA");
            }
        }
    }

    void ResultLargeStraigt()
    {
        int arrayLength = diceResults.Length;
        for(int i = 0; i < diceResults.Length; i++) //Rúllar í gegnum alla teningana og leitra að langri röð
        {
            if( diceResults[i] == 1) //Tékkar hvort þú fékkst 1 og leitar þá að 2
            {
                for(int u = 0; u < arrayLength; u++)
                {
                    if( diceResults[u] == 2) // tékkar hvort að það sé enhverstaðra 2 og leitar þá að 3
                    {
                        for(int y = 0; y < arrayLength; y++)
                        {
                            if( diceResults[y] == 3) // tékkar hvort að það sé enhverstaðar 3 og leitar þá að 4
                            {
                                for( int t = 0; t < arrayLength; t++)
                                {
                                    if (diceResults[t] == 4) // tékkar hvort að það sé 4 enhverstaðar og leitar þá að 5
                                    {
                                        for(int r = 0; r < arrayLength; r++)
                                        {
                                            if( diceResults[r] == 5)
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
            else if( diceResults[i] == 2) //tékkar hvort að þú fékkst 2 einhverstaðar og leitar þá að 3
            {
                for(int u = 0; u < arrayLength; u++)
                {
                    if( diceResults[u] == 3)
                    {
                        for (int y = 0; y < arrayLength; y++)
                        {
                            if( diceResults[y] == 4)
                            {
                                for( int t = 0; t < arrayLength; t++)
                                {
                                    if(diceResults[t] == 5)
                                    {
                                        for( int r = 0; t < arrayLength; r++)
                                        {
                                            if(diceResults[r] == 6)
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

    void ResultYatzee()
    {
        if(     diceResults[0] == diceResults[1] &&
                diceResults[1] == diceResults[2] &&
                diceResults[2] == diceResults[3] &&
                diceResults[3] == diceResults[4]    )
        {
            Debug.Log("Allir eins, þú færð Yatzee");
        }
    }

    void ResultSmallStraigt()
    {
        int arrayLength = diceResults.Length;

        for( int i = 0; i < arrayLength; i++)
        {
            if (diceResults[i] == 1)
            {
                for(int u = 0; u < arrayLength; u++)
                {
                    if(diceResults[u] == 2)
                    {
                        for( int y = 0; y < arrayLength; y++)
                        {
                            if(diceResults[y] == 3)
                            {
                                for(int t = 0; y <arrayLength; y++)
                                {
                                    if(diceResults[t] == 4)
                                    {
                                        Debug.Log("Þú fékkstr röð frá 1-4");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            } 
            else if( diceResults[i] == 2)
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
                                        Debug.Log("Þú fékkstr röð frá 2-5");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if(diceResults[i] == 3)
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
                                        Debug.Log("Þú fékkstr röð frá 3-6");
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
}
