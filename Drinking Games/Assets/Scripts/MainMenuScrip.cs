using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class MainMenuScrip : MonoBehaviour
{
    float FadeTimer = 0.5f;
    float offsetPosition = 8f;



    private UIBehaviour[] thingsInCanvas;
    private Animation anim;
    void Start()
    {
        thingsInCanvas = FindThingsInCanvas();
        if(GameObject.FindGameObjectWithTag("SceneAnimator") != null)
        {
            anim = GameObject.FindGameObjectWithTag("SceneAnimator").GetComponent<Animation>();
            anim.Play("4ColorScrollDown");
        }
        else
        {
            foreach (UIBehaviour thing in thingsInCanvas)
            {
                TurnAlphaToZero(thing);
                StartCoroutine(FadeIn(thing));
            }
        }
    }

    IEnumerator findOffsetPositon(UIBehaviour thing)
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        float timer = 0f;

        if (thing.GetComponent<Button>() != null)
        {
            while (timer < FadeTimer)
            {
                thing.GetComponent<RectTransform>().position = new Vector3(thing.GetComponent<RectTransform>().position.x - offsetPosition * timer / FadeTimer,
                                                                            thing.GetComponent<RectTransform>().position.y,
                                                                            thing.GetComponent<RectTransform>().position.z);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        yield return null;
    }
    private void TurnAlphaToZero(UIBehaviour thing)
    {
        if (thing.GetComponent<Text>() != null)
        {
            Color textColor = thing.GetComponent<Text>().color;
            textColor = new Color(textColor.r, textColor.g, textColor.b, 0);
            thing.GetComponent<Text>().color = textColor;
        }
        else if (thing.GetComponent<Image>() != null)
        {
            Color imgColor = thing.GetComponent<Image>().color;
            imgColor = new Color(imgColor.r, imgColor.g, imgColor.b, 0);
            thing.GetComponent<Image>().color = imgColor;
        }
    }
    public void LoadGame(string LevelName)
    {
        gameObject.GetComponent<RuleSender>().LevelSelected(LevelName);
        Application.LoadLevel(LevelName);
    }
    //public void LoadGame(int LevelIndex)
    //{
    //    Application.LoadLevel(LevelIndex);
    //}
    public void FadeGameObjectAndLoad(string LevelName)
    {
        foreach (UIBehaviour thing in thingsInCanvas)
        {
            StartCoroutine(findOffsetPositon(thing));
            StartCoroutine(FadeOut(thing));
        }
        StartCoroutine(LoadLevel(LevelName, FadeTimer));
    }
    public void PlayAnimationOnLoad(string LevelName)
    {
        if (anim != null)
        {
        anim.Play("4colorScrollDownPart1");
        StartCoroutine(LoadLevel(LevelName, anim.clip.length));
        }
        else
        {
            FadeGameObjectAndLoad(LevelName);
        }

    }

    public UIBehaviour[] FindThingsInCanvas()
    {
        GameObject canvas = GameObject.Find("Canvas");
        UIBehaviour[] canvasChildrens = canvas.GetComponentsInChildren<UIBehaviour>();
        return canvasChildrens;
    }
    IEnumerator FadeOut(UIBehaviour thingToFade)
    {
        float timer = FadeTimer;

        if (thingToFade.GetComponent<Text>() != null)
        {
            while (timer > -0.1f)
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
            while (timer > -0.1f)
            {
                Color imgColor = thingToFade.GetComponent<Image>().color;
                imgColor = new Color(imgColor.r, imgColor.g, imgColor.b, timer / FadeTimer);
                thingToFade.GetComponent<Image>().color = imgColor;
                timer -= Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
    }
    IEnumerator FadeIn(UIBehaviour thingToFade)
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        float timer = 0;
        if (thingToFade.GetComponent<Text>() != null)
        {
            while (timer <= FadeTimer)
            {
                Color textColor = thingToFade.GetComponent<Text>().color;
                textColor = new Color(textColor.r, textColor.g, textColor.b, timer / FadeTimer);
                thingToFade.GetComponent<Text>().color = textColor;
                timer += Time.deltaTime;
                yield return null;
            }
        }
        else if (thingToFade.GetComponent<Image>() != null)
        {
            while (timer <= FadeTimer)
            {
                Color imgColor = thingToFade.GetComponent<Image>().color;
                imgColor = new Color(imgColor.r, imgColor.g, imgColor.b, timer / FadeTimer);
                thingToFade.GetComponent<Image>().color = imgColor;
                timer += Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
    }
    IEnumerator LoadLevel(string levelName, float timer)
    {
        yield return new WaitForSeconds(timer);
        Application.LoadLevel(levelName);
    }
}
