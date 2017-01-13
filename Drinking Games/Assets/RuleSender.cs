using UnityEngine;
using System.Collections;

public class RuleSender : MonoBehaviour
{
    public void LevelSelected(string SelectedLevelName)
    {
        if( SelectedLevelName == "FUBAR")
        {
            PlayerPrefs.SetString("rules", "Fuuult af reglum");
        }
        else
        {
            // hér þarf að kasta villu á eftir að gera fleirri if/else fyrir hin scene
        }
    }

}
