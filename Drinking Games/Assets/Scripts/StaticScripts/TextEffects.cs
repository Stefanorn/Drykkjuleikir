using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextEffects : MonoBehaviour
{
    public static IEnumerator FadeText( Text TextBox,  float animationTime )
    {
        TextBox.color = new Color(TextBox.color.r, TextBox.color.g, TextBox.color.b, 0); //gerir hlutinn gegnsæjan
        float timer = 0f;
        while (timer < animationTime)
        {
            timer += Time.deltaTime;
            TextBox.color = new Color(TextBox.color.r, TextBox.color.g, TextBox.color.b, timer / animationTime);
            yield return null;
        }
    }
}
