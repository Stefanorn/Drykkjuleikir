using UnityEngine;
using System.Collections;
using System;

public class ClickedDice : MonoBehaviour
{
    public GameObject ClickedFx;

    RuleHandler ruleHandler;
    public void Start()
    {
        ruleHandler = GameObject.FindGameObjectWithTag("DiceRule").GetComponent<RuleHandler>();
    }
    // Notar DetectClickAndTouce sem er script sem þarf alltaf að vera í scene á einhverju obj
    // Einnig er hægt að nota ClickMessageSender sem sendir clickposition
    public void MouseDown() 
    {
        if (    ruleHandler.haveAllDiceStop &&
                gameObject.tag == "NonSelectedDice")
        {
            gameObject.tag = "Dice";
            GameObject gfx =  (GameObject)Instantiate(ClickedFx, transform.position, Quaternion.identity);
            gfx.transform.parent = transform.parent;
        }
    }
}
