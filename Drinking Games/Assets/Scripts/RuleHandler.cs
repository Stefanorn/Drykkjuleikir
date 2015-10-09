using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        for(int i = 0; i < diceResults.Length ; i++) 
        {
            int u = i + 1;
            if(u > diceResults.Length -1 ) //passar að u fari aldrei útfyrir arrayin
            {
                u = i;
            }
                
            if(diceResults[i] == diceResults[u]) //Tékkar á tvennum
            {
                for (int y = 0; y< diceResults.Length ; y++)
                {
                    if( diceResults[i] == diceResults[y] && y != u)
                    {
                        Debug.Log("ÞRENNA " + diceResults[i] + " og " + diceResults[y] + " og  " + diceResults[u]);
                        for(int t = 0; t < diceResults.Length ; t++)
                        {
                            if (diceResults[i] == diceResults[t] && t != y && t != u) 
                            {
                                Debug.Log("FERNA " + diceResults[i] + " og " + diceResults[y] + " og  " + diceResults[u] + " og " + diceResults[t]); //RUGL fynna betri laust
                            }
                          
                        }
                       
                    }
                    
                }
                Debug.Log("Tvenna " + diceResults[i] + " og " + diceResults[u]);
             
            }
            if(diceResults[i] == 7)
            {
                Debug.Log("VILLA");
            }
        }
    }
}
