using UnityEngine;
using System.Collections;

public class ClickMessageSender : MonoBehaviour
{
    public static Vector3 MouseDown()
    {
        Camera myCamera = Camera.main;
        Ray ray;
        RaycastHit hit;

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    ray = myCamera.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        return hit.point;
                        //hit.transform.gameObject.SendMessage("MouseDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))  //Check to see if we've clicked
            {
                ray = myCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    return hit.point;
                }
            }
        }
        return Vector3.zero;
    }
    public static Vector3 MousePos()
    {
        Camera myCamera = Camera.main;
        Ray ray;
        RaycastHit hit;

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    ray = myCamera.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        return hit.point;
                    }
                }
            }

        }
        else
        {
            ray = myCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                return hit.point;
            }

        }
        return Vector3.zero;
    }
    public static Vector3 MouseUp()
    {
        Camera myCamera = Camera.main;
        Ray ray;
        RaycastHit hit;

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    ray = myCamera.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        return hit.point;
                        //hit.transform.gameObject.SendMessage("MouseDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

        }
        else
        {
            if (Input.GetMouseButtonUp(0))  //Check to see if we've clicked
            {
                ray = myCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    return hit.point;
                    //hit.transform.gameObject.SendMessage("MouseDown", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        return Vector3.zero;
    }
}