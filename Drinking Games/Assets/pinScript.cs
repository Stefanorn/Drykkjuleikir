using UnityEngine;
using System.Collections;

public class pinScript : MonoBehaviour
{
    public float inpactAmount = 50f ;
    public float timeToOrgPos = 1;

    float rotationTimer = 0;
    Vector3 orgPos;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        orgPos = transform.position;

    }
    void FixedUpdate()
    {
        transform.position = orgPos;
       // Quaternion desierdRotation = Quaternion.LookRotation( Vector3.zero);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationTimer * timeToOrgPos );
        rotationTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        rb.AddTorque(new Vector3(0, 0, inpactAmount));
        rotationTimer = 0;

    }
}
