using UnityEngine;
using System.Collections;

public class DiceForce : MonoBehaviour {

    public Vector2 rotRange;
    

    

	// Use this for initialization
	void Start () {
        Rigidbody rb =  gameObject.GetComponent<Rigidbody>();

        Vector3 rotTheDice = new Vector3(   Random.Range(rotRange.x, rotRange.y),
                                            Random.Range(rotRange.x, rotRange.y),
                                            Random.Range(rotRange.x, rotRange.y));
        rb.AddTorque(rotTheDice);

        Vector2 giveforce = new Vector2(Random.Range(-500, 500), Random.Range(-500, 500)); //Verður determenað af player

        rb.AddForce(giveforce.x, 0, giveforce.y);
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
