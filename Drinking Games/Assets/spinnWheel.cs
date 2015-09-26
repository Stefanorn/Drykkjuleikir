using UnityEngine;
using System.Collections;

public class spinnWheel : MonoBehaviour {

    public float spinForce = 50;
    public float rotationThreshold = 0.1f;

    Vector3 mouseClickPosition;
    Vector3 mouesEndClickPosition;
    float dragDist;
    Rigidbody rb;
    Quaternion lastFrameRotation;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        lastFrameRotation = transform.rotation;
    }
	
	// Update is called once per frame
	void OnMouseDown()
    {
       mouseClickPosition = Input.mousePosition;
    }
    void OnMouseUp()
    {
        mouesEndClickPosition = Input.mousePosition;
        dragDist = Vector3.Distance(mouesEndClickPosition, mouseClickPosition);
        rb.AddTorque(new Vector3(0, dragDist * spinForce, 0), ForceMode.Impulse);
    }
    void FixedUpdate()
    {
        Quaternion thisFrameRotation = transform.rotation;
        float rotationSpeed = thisFrameRotation.eulerAngles.x - lastFrameRotation.eulerAngles.x;

        if(rotationSpeed < rotationThreshold && rotationSpeed < -rotationThreshold )
        {
            Debug.Log(rotationSpeed);
        }

        lastFrameRotation = transform.rotation;
    }


}
