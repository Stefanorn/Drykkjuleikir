using UnityEngine;
using System.Collections;

public class DiceHandler : MonoBehaviour
{

    public GameObject dice;
    public float diceDropHight = 50f;
    public float clearTimer = 5f;
    public int maxNoDice = 3;


    GameObject[] diceInsts;

    void Start()
    {
        diceInsts = new GameObject[maxNoDice];
    }

    public void ClearDice()
    {
        foreach (GameObject diceInst in diceInsts)
        {
            Destroy(diceInst, clearTimer);
        }
    }

    void Clicked(Vector3 clickPos)
    {
        clickPos = new Vector3(clickPos.x, diceDropHight, clickPos.z);

        for (int i = 0; i < diceInsts.Length; i++) 
            if (diceInsts[i] == null)
            {
                diceInsts[i] = (GameObject)Instantiate(dice, clickPos, Quaternion.identity);
                break;
            }
    }

}
