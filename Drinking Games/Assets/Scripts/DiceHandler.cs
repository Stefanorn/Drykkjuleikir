using UnityEngine;
using System.Collections;

public class DiceHandler : MonoBehaviour {

    public GameObject dice;
    public float diceDropHight = 50f;
    

    void Clicked( Vector3 clickPos )
    {
        clickPos = new Vector3(clickPos.x, diceDropHight, clickPos.z);
        Instantiate(dice, clickPos, Quaternion.identity);
    }
}
