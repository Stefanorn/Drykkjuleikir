using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RuleHandler : MonoBehaviour {

    public Text textBox;
    public string[] rules;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	}
    public void ChangeRule(int diceNumber)
    {
        textBox.text = rules[diceNumber - 1];
    }
}
