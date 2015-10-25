using UnityEngine;
using System.Collections;


public class DiceHandler : MonoBehaviour
{

    public GameObject dice;
    public float diceDropHight = 50f;
    public float clearTimer = 5f;
    public int maxNoDice = 3;


    public GameObject[] diceInsts;

    void Start()
    {
        diceInsts = new GameObject[maxNoDice];
    }

    public void ClearDice()
    {
        foreach (GameObject diceInst in diceInsts)
        {
            if (diceInst.tag == "NonSelectedDice")
            {
                Destroy(diceInst);
            }
        }
    }

    void Clicked(Vector3 clickPos)
    {
        clickPos = new Vector3(clickPos.x, clickPos.y, clickPos.z);

        for (int i = 0; i < diceInsts.Length; i++) 
            if (diceInsts[i] == null)
            {
                Vector3 instPos = new Vector3(  clickPos.x + (i * Random.Range(1, 5)),
                                                diceDropHight,
                                                clickPos.z + (i * Random.Range(1, 5)));
                diceInsts[i] = (GameObject)Instantiate(dice, instPos, Quaternion.identity);
                Rigidbody rb = diceInsts[i].GetComponent<Rigidbody>();
                float randomFoceAmount = 300;
                float randomRotatioAmount = 400;
                rb.AddForce(Random.Range(   -randomFoceAmount, randomFoceAmount),
                                            0 , 
                                            Random.Range(  -randomFoceAmount, randomFoceAmount) ,
                                            ForceMode.Impulse);
                rb.AddTorque(Random.Range(  -randomRotatioAmount, randomRotatioAmount), 
                                            Random.Range(randomRotatioAmount, randomRotatioAmount),
                                            Random.Range(randomRotatioAmount, randomRotatioAmount) ,
                                            ForceMode.Impulse);
                //Hér getur komið alskonar foce og eginleikar fyrir hvern tening

             }
    }

}
