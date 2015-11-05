using UnityEngine;
using System.Collections;

public class pinScript : MonoBehaviour
{
    public float inpactAmount = 50f ;
    public float timeToOrgPos = 1;

    float rotationTimer = 2;
    Quaternion orgRot;
    Rigidbody2D rb;
    Rigidbody2D wheelRB;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        orgRot = transform.rotation;
        wheelRB = GameObject.Find("Lukkuhjól").GetComponent<Rigidbody2D>(); //OPTIMIZE!!! þarf að leita í öllum GO í projectinu
    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, orgRot, rotationTimer * timeToOrgPos );
        rotationTimer += Time.deltaTime;

    }

    void OnTriggerEnter2D()
    {
        float wheelAngVel = Mathf.Sqrt(Mathf.Pow( wheelRB.angularVelocity, 2 ));
        if (wheelAngVel > 10)
        {
            transform.Rotate(new Vector3(0, 0, (wheelAngVel / 100) * Random.Range(inpactAmount - (inpactAmount / 10), inpactAmount + (inpactAmount / 10))));
        }

        rotationTimer = 0;
    }

}
