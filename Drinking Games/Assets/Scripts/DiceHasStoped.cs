using UnityEngine;
using System.Collections;

public class DiceHasStoped : MonoBehaviour {

    Vector3 lastFramePos;
    Quaternion lastFrameRotation;

    bool triggerOnce = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(lastFramePos == transform.position && lastFrameRotation == transform.rotation)
        {
            if (triggerOnce)
            {
                GameObject.FindGameObjectWithTag("DiceRule").GetComponent<RuleHandler>().DiceStopedChecker();         //Segir við RuleHandler að þessi teningu er búin að stoppa, 
                                                                                                                      //og rule handler telur hversu margir teningar eru stopp og kallar svo á diceface þegar allir teningar eru stopp

                triggerOnce = false;
            }
        }
        else
        {
            lastFramePos = transform.position;
            lastFrameRotation = transform.rotation;
        }
    }
    public int DiceFace()
    {
        int xDiceRot = (int)transform.rotation.eulerAngles.x;
        int zDiceRot = (int)transform.rotation.eulerAngles.z;


        if (xDiceRot == 0 && zDiceRot == 90) //ás
        {
            return 1;
        }
        else if (xDiceRot == 0 && zDiceRot == 0) //Tvistur
        {
            return 2;
        }
        else if (xDiceRot == 270 && zDiceRot == 0) //Þristur
        {
            return 3;
        }
        else if (xDiceRot == 90 && zDiceRot == 0) //Fjarki
        {
            return 4;
        }
        else if (xDiceRot == 0 && zDiceRot == 180) //Fimma
        {
            return 5;
        }
        else if (xDiceRot == 0 && zDiceRot == 270) //Sexa
        {
            return 6;
        }
        else //Villa ?
        {
            return 6; //ÞARF AÐ GETA BETRI K'OÐA TIL AÐ TEKKA A VILLU
        }
    }
}
