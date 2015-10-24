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
    public void Clicked()
    {
        if (    ruleHandler.haveAllDiceStop &&
                gameObject.tag == "NonSelectedDice")
        {
            ruleHandler.SelectedDiceTracker();
            gameObject.tag = "Dice";
            GameObject gfx =  (GameObject)Instantiate(ClickedFx, transform.position, Quaternion.identity);
            gfx.transform.parent = transform.parent;
        }
    }
}
