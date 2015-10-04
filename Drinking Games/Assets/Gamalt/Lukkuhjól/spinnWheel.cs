using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spinnWheel : MonoBehaviour {

    public float drag = 50f;
    public int numOfPins = 14;
    public string[] reglur;
    public Text textBox;
    

    Vector3 mouseClickPosition;
    Vector3 mouesEndClickPosition;

    float dragDist;
    public static bool hasGameStarted = false; //ALSO USED IN pinScript!!!!

    void Start()
    {
        hasGameStarted = false;
    }

	// Update is called once per frame
	void OnMouseDown()
    {
        if (!hasGameStarted)
        {
            mouseClickPosition = Input.mousePosition;

        } 
    }
    void OnMouseUp()
    {
        if (!hasGameStarted)
        {
            mouesEndClickPosition = Input.mousePosition;
            dragDist = Vector3.Distance(mouesEndClickPosition, mouseClickPosition);
            hasGameStarted = true;
        }
    }
    void FixedUpdate()
    {
        //TODO!!!! Bæta inn þannig að hjólið getur snúist í öfuga átt,
        //TODO!!!  Gera grein fyrir Tíma þegar notandi dregur í hjólið
        //TODO!!! athuga reikning fyrir valið hólf
        //TODO!!! Rglurnar eru bara dummy reglur flestar bæta og breita og laga þar
        //TODO!!! FinnaNýa GFX
        //TODO!!! Finna leið til að tengja lengt á filkinu Regla og num of pins

        transform.Rotate(new Vector3(0, dragDist * Time.deltaTime, 0)); 
        dragDist -= (Time.deltaTime * (drag ));

        if (dragDist <= 0) //Clamps the Dragdist to 0
        {
            if (hasGameStarted)
            {
                if (Application.loadedLevel == 3)
                {
                    textBox.text = reglur[(int)NumberChooser()];
                }
                else
                {
                    textBox.text = reglur[ Random.Range(0, reglur.Length -1 ) ];
                }
              hasGameStarted = false;
            }
            dragDist = 0;
        }

    }
    float NumberChooser() //ATH Reikning! //
    {
        float pinSpacing = 0;
        if(transform.rotation.x < 0)
        {
            pinSpacing = (transform.rotation.x * numOfPins + 10) * 1;

        }
        else
        {
            pinSpacing = (transform.rotation.x* numOfPins - 10) * -1;
        }
        return pinSpacing;
    }

}
