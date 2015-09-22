using UnityEngine;
using System.Collections;

public class spinnWheel : MonoBehaviour {

    Vector3 mouseClickPosition;
    Vector3 mouesEndClickPosition;
    float dragDist;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void OnMouseDown()
    {
       mouseClickPosition = Input.mousePosition;
    }
    void OnMouseUp()
    {
        mouesEndClickPosition = Input.mousePosition;
    }
    void FixedUpdate()
    {
        dragDist = Vector3.Distance(mouesEndClickPosition, mouseClickPosition);
        Camera.main.transform.Rotate(0, 0, dragDist * Time.deltaTime);
    }

}
