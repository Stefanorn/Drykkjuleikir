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
    float totalAcciliration;
    bool diceReleced = false;

    void Start()
    {
        diceInsts = new GameObject[maxNoDice];
        SpawnDice();

    }

    void FixedUpdate()
    {
       float positiveAcciliration = Input.acceleration.x * Input.acceleration.x;
        if( positiveAcciliration > 0.3)
        {
            totalAcciliration += positiveAcciliration;

            if (!diceReleced)
            {
                foreach (GameObject dice in diceInsts)
                {
                    Rigidbody rb = dice.GetComponent<Rigidbody>();
                    Vector3 accilForce = new Vector3(Input.acceleration.x, 0, Input.acceleration.z);
                    rb.AddForce(accilForce * -75, ForceMode.Impulse);
                    Debug.Log(accilForce);


                }
            }

        }
        if( totalAcciliration > 200)
        {
            ReleceDice();
            totalAcciliration = 0;
        }



    }
    void ReleceDice()
    {
        diceReleced = true;
        foreach(GameObject dice in diceInsts)
        {
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(Vector3.up * 15, ForceMode.Impulse);
        }
    }

    public void ClearDice()
    {
        diceReleced = false;
        totalAcciliration = 0;
        
        foreach (GameObject diceInst in diceInsts)
        {
            if (diceInst.tag == "NonSelectedDice")
            {
                ResetDice(diceInst);
                //Destroy(diceInst);
            }
        }
        GameObject[] junks = GameObject.FindGameObjectsWithTag("JunkToClear");
        foreach(GameObject junk in junks)
        {
            Destroy(junk);
        }


        SpawnDice();
    }

    void ResetDice(GameObject dice)
    {
        Quaternion randomRotation = new Quaternion(Random.Range(0, 360),
                                            Random.Range(0, 360),
                                            Random.Range(0, 360),
                                            Random.Range(0, 360));

        Vector3 instPos = new Vector3((Random.Range(1, 7)),
                                        diceDropHight,
                                        (Random.Range(1, 7)));
        dice.transform.position = instPos;
        dice.transform.rotation = randomRotation;
        Rigidbody rb = dice.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        dice.gameObject.tag = "Dice";
    }


    void SpawnDice()
    {
        for (int i = 0; i < diceInsts.Length; i++)
            if (diceInsts[i] == null)
            {
                Quaternion randomRotation = new Quaternion(Random.Range(0, 360),
                                                            Random.Range(0, 360),
                                                            Random.Range(0, 360),
                                                            Random.Range(0, 360));

                Vector3 instPos = new Vector3(  (i * Random.Range(1, 7)),
                                                diceDropHight,
                                                (i * Random.Range(1, 7)));
                diceInsts[i] = (GameObject)Instantiate(dice, instPos, randomRotation);
                Rigidbody rb = diceInsts[i].GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezePositionY;

            }





        //for (int i = 0; i < diceInsts.Length; i++) 
        //    if (diceInsts[i] == null)
        //    {
        //        Vector3 instPos = new Vector3(  ClickMessageSender.MousePos().x + (i * Random.Range(1, 5)),
        //                                        diceDropHight,
        //                                        ClickMessageSender.MousePos().z + (i * Random.Range(1, 5)));
        //        diceInsts[i] = (GameObject)Instantiate(dice, instPos, Quaternion.identity);
        //        Rigidbody rb = diceInsts[i].GetComponent<Rigidbody>();
        //        float randomRotatioAmount = 400;
        //        rb.AddForce(    (endPos - startPos) * 30 ,
        //                        ForceMode.Impulse);
        //        rb.AddTorque(Random.Range(  -randomRotatioAmount, randomRotatioAmount), 
        //                                    Random.Range(randomRotatioAmount, randomRotatioAmount),
        //                                    Random.Range(randomRotatioAmount, randomRotatioAmount) ,
        //                                    ForceMode.Impulse);
        //        //Hér getur komið alskonar foce og eginleikar fyrir hvern tening

        //     }
    }
    void MouseUp(Vector3 endClick)
    {

    }
}
