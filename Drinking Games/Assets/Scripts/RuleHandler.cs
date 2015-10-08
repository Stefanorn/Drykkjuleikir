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
            CallRule(); 
            for(int i = 0;i < diceResults.Length ; i++) //Núlla gögnin í diceResult vegna þess að ég tékka hvort að það sé eitthvað annað enn 0 í því þegar ég cleara teningana
            {
                diceResults[i] = 0;
            }
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
            if(diceResults[i] == diceResults[i+1])
            {

            }
        }
    }
}
