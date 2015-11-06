using UnityEngine;
using System.Collections;

public class DiceHasStoped : MonoBehaviour {

    Vector3 lastFramePos;
    Quaternion lastFrameRotation;

    bool triggerOnce = true;
	// Update is called once per frame
	void FixedUpdate () {

        if(lastFramePos == transform.position && lastFrameRotation == transform.rotation)
        {
            if (triggerOnce)
            {
                gameObject.tag = "NonSelectedDice";
                GameObject.FindGameObjectWithTag("DiceRule").GetComponent<RuleHandler>().DiceStopedChecker();         //Segir við RuleHandler að þessi teningu er búin að stoppa, 
                triggerOnce = false;                                                                                  //og rule handler telur hversu margir teningar eru stopp og kallar svo á diceface þegar allir teningar eru stopp
                                                                                                                      //Hér væri hugsamlega hægt að láta hvern tening skila +1 til að auka performance
                                                                                                                      //þegar þeir hafa stöðvast enn það skilar stundum óskiljandi bögga (triggerOnce verður stundum ekki false og þá loopast allt)
            }
        }
        else
        {
            lastFramePos = transform.position;
            lastFrameRotation = transform.rotation;
        }
    }
    //0 90 180 270 360
    int RoundUp(int input )
    {
        Debug.Log(input);
        if(input < 45 && input >= 0) //Ef input er 45 þá er skilað 0 afþví bara
        {
            return 0;
        }
        else if( input > 45 && input < 135 )
        {
            return 90;
        }
        else if( input > 135 && input < 225)
        {
            return 180;
        }
        else if(input > 225 && input < 315)
        {
            return 270;
        }
        else if ( input > 315 && input <= 360)
        {
            return 360;
        }
        else
        {
            Debug.LogError("Tala verður að vera á bilinu 0 - 360");
            return input;
        }
    }

    public int DiceFace()
    {
        int xDiceRot = (int)transform.rotation.eulerAngles.x;
        int zDiceRot = (int)transform.rotation.eulerAngles.z;

        if( xDiceRot%90 != 0)
        {
            xDiceRot = RoundUp(xDiceRot);
        }
        if (zDiceRot % 90 != 0)
        {
            zDiceRot= RoundUp(zDiceRot);
        }
        

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
            Debug.LogError("Kóði gat ekki lesið hverning teningur snýr");
            return 7; //ÞARF AÐ GETA BETRI K'OÐA TIL AÐ TEKKA A VILLU
        }
    }
}
