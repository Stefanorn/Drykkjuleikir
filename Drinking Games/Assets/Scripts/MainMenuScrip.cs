using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScrip : MonoBehaviour
{
    public Image curtains;

    float cuirtainAlphaFader = 0f;

    public void LoadGame(string LevelName)
    {
        Application.LoadLevel(LevelName);
    }
    void OnLevelWasLoaded()
    {
       // curtains.gameObject.SetActive(true);
    }
   /* void Update()
    {
        curtains.color = new Color(255, 255, 255, cuirtainAlphaFader);
        cuirtainAlphaFader = Mathf.Lerp(1, 0, Time.time *2 );
        if (cuirtainAlphaFader <= 0 )
        {
            curtains.gameObject.SetActive(false);
        }

    }*/
}
