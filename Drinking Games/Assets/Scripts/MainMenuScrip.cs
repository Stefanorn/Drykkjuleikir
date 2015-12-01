using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MainMenuScrip : MonoBehaviour
{
    public float FadeTimer = 0.5f;

    private List<UIBehaviour> thingsInCanvas = new List<UIBehaviour>();
    void Start()
    {
        thingsInCanvas = FindThingsInCanvas();
        foreach (UIBehaviour thing in thingsInCanvas)
        {
            StartCoroutine(FadeIn(thing));
        }
    }


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
        foreach (UIBehaviour thing in thingsInCanvas)
        {
            StartCoroutine(FadeOut(thing, LevelName));
        }
    }
    public List<UIBehaviour> FindThingsInCanvas() //Fáramlega Redundat kóði þarf að láta skila bara array EZPZ
    {
        GameObject canvas = GameObject.Find("Canvas");
         Transform[] canvasChildrens = canvas.GetComponentsInChildren<Transform>();


        List<UIBehaviour> temp = new List<UIBehaviour>();
        
        foreach (Transform go in canvasChildrens)
        {
            if (go.GetComponent<Button>() != null)
            {
                temp.Add(go.GetComponent<Button>());
                //  return go.GetComponent<Button>();
                //  StartCoroutine(FadeOut(go.GetComponent<Button>(), LevelName));
            }
            else if (go.GetComponent<Text>() != null)
            {
                temp.Add(go.GetComponent<Text>());
                //   StartCoroutine(FadeOut(go.GetComponent<Text>(), LevelName));
            }
            else if(go.GetComponent<Image>() != null)
            {
                temp.Add(go.GetComponent<Image>());
            }
        }
        return temp;
    }
    IEnumerator FadeOut(UIBehaviour thingToFade, string levelToLoad) //TODO ekki troða levelToLoad hingað
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
        else if (thingToFade.GetComponent<Image>() != null)
        {
            while (timer >= 0)
            {
                Color imgColor = thingToFade.GetComponent<Image>().color;
                imgColor = new Color(imgColor.r, imgColor.g, imgColor.b, timer / FadeTimer);
                thingToFade.GetComponent<Image>().color = imgColor;
                timer -= Time.deltaTime;
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.5f);

        Application.LoadLevel(levelToLoad);
        yield return null;
    }
    IEnumerator FadeIn(UIBehaviour thingToFade)
    {

        float timer = 0;
        if (thingToFade.GetComponent<Button>() != null)
        {
            ColorBlock color = thingToFade.GetComponent<Button>().colors;
            color.normalColor = new Color(color.normalColor.r, color.normalColor.g, color.normalColor.b, 0);
            color.highlightedColor = new Color(color.normalColor.r, color.normalColor.g, color.normalColor.b, 0);
            thingToFade.GetComponent<Button>().colors = color;
            yield return new WaitForSeconds(0.4f);
            while (timer <= FadeTimer)
            {
                color = thingToFade.GetComponent<Button>().colors;
                color.normalColor = new Color(color.normalColor.r, color.normalColor.g, color.normalColor.b, timer / FadeTimer);
                color.highlightedColor = new Color(color.normalColor.r, color.normalColor.g, color.normalColor.b, timer / FadeTimer);
                thingToFade.GetComponent<Button>().colors = color;
                timer += Time.deltaTime;
                yield return null;
            }
        }
        else if (thingToFade.GetComponent<Text>() != null)
        {
            Color textColor = thingToFade.GetComponent<Text>().color;
            textColor = new Color(textColor.r, textColor.g, textColor.b, 0);
            thingToFade.GetComponent<Text>().color = textColor;
            yield return new WaitForSeconds(0.4f);
            while (timer <= FadeTimer)
            {
                textColor = thingToFade.GetComponent<Text>().color;
                textColor = new Color(textColor.r, textColor.g, textColor.b, timer / FadeTimer);
                thingToFade.GetComponent<Text>().color = textColor;
                timer += Time.deltaTime;
                yield return null;
            }
        }
        else if (thingToFade.GetComponent<Image>() != null)
        {
            Color imgColor = thingToFade.GetComponent<Image>().color;
            imgColor = new Color(imgColor.r, imgColor.g, imgColor.b, 0);
            thingToFade.GetComponent<Image>().color = imgColor;
            yield return new WaitForSeconds(0.4f);
            while (timer <= FadeTimer)
            {
                imgColor = thingToFade.GetComponent<Image>().color;
                imgColor = new Color(imgColor.r, imgColor.g, imgColor.b, timer / FadeTimer);
                thingToFade.GetComponent<Image>().color = imgColor;
                timer += Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
    }
}
