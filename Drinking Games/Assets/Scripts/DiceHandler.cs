using UnityEngine;
using System.Collections;

public class DiceHandler : MonoBehaviour
{

    public GameObject dice;
    public float diceDropHight = 50f;
    public float clearTimer = 5f;
    public int maxNoDice = 3;


    public GameObject[] diceInsts;

    Vector3 startPos;
    Vector3 endPos;

    void Start()
    {
        diceInsts = new GameObject[maxNoDice];
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = ClickMessageSender.MousePos();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = ClickMessageSender.MousePos();
            SpawnDice();
        }

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
    void SpawnDice()
    {
        for (int i = 0; i < diceInsts.Length; i++) 
            if (diceInsts[i] == null)
            {
                Vector3 instPos = new Vector3(  ClickMessageSender.MousePos().x + (i * Random.Range(1, 5)),
                                                diceDropHight,
                                                ClickMessageSender.MousePos().z + (i * Random.Range(1, 5)));
                diceInsts[i] = (GameObject)Instantiate(dice, instPos, Quaternion.identity);
                Rigidbody rb = diceInsts[i].GetComponent<Rigidbody>();
                float randomRotatioAmount = 400;
                rb.AddForce(    (endPos - startPos) * 30 ,
                                ForceMode.Impulse);
                rb.AddTorque(Random.Range(  -randomRotatioAmount, randomRotatioAmount), 
                                            Random.Range(randomRotatioAmount, randomRotatioAmount),
                                            Random.Range(randomRotatioAmount, randomRotatioAmount) ,
                                            ForceMode.Impulse);
                //Hér getur komið alskonar foce og eginleikar fyrir hvern tening

             }
    }
    void MouseUp(Vector3 endClick)
    {

    }
}
