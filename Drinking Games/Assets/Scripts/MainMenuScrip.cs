using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MainMenuScrip : MonoBehaviour
{
    public float FadeTimer = 0.5f;

    public void LoadGame(string LevelName)
    {
        Application.LoadLevel(LevelName);
    }
    public void LoadGame(int LevelIndex)
    {
        Application.LoadLevel(LevelIndex);
    }
    public void FadeGameObjectAndLoad(string LevelName)
    {
        GameObject canvas = GameObject.Find("Canvas");
        Transform[] canvasChildrens = canvas.GetComponentsInChildren<Transform>();

        foreach (Transform go in canvasChildrens)
        {
            if (go.GetComponent<Button>() != null)
            {
                StartCoroutine(FadeOut(go.GetComponent<Button>(), LevelName));
            }
            if (go.GetComponent<Text>() != null)
            {
                StartCoroutine(FadeOut(go.GetComponent<Text>(), LevelName ));
            }
        }

    }

    IEnumerator FadeOut(UIBehaviour thingToFade, string levelToLoad)
    {
        float timer = FadeTimer;
        if (thingToFade.GetComponent<Button>() != null)
        {
            while (timer >= 0)
            {
                ColorBlock color = thingToFade.GetComponent<Button>().colors;
                color.normalColor = new Color(color.normalColor.r, color.normalColor.g, color.normalColor.b, timer / FadeTimer);
                color.highlightedColor = new Color(color.normalColor.r, color.normalColor.g, color.normalColor.b, timer / FadeTimer);
                thingToFade.GetComponent<Button>().colors = color;
                timer -= Time.deltaTime;
                yield return null;
            }
        }
        else if (thingToFade.GetComponent<Text>() != null)
        {
            while (timer >= 0)
            {
                Color textColor = thingToFade.GetComponent<Text>().color;
                textColor = new Color(textColor.r, textColor.g, textColor.b, timer / FadeTimer);
                thingToFade.GetComponent<Text>().color = textColor;
                timer -= Time.deltaTime;
                yield return null;
            }
        }
        timer = 0.05f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        Application.LoadLevel(levelToLoad);
        yield return null;
    }
}
