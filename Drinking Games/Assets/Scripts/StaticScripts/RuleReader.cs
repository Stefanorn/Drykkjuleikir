using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;


public class RuleReader : MonoBehaviour {

    public static string pathToRules = @"C:\Users\StefanOrn\Documents\" + Application.loadedLevelName +  "Rules.txt";
    public static string[] streamContent;


    public static string[] ReadRules( string keyword )
    {
        FileStream fs = new FileStream(pathToRules, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);

        MatchCollection matches = Regex.Matches(sr.ReadToEnd(), Application.loadedLevel + @" === (.*)...");

        string[] returnString = new string[matches.Count-1];

        int i = 0;
        foreach( Match match in matches)
        {
            returnString[i] = match.Groups[1].ToString();
            i++;
        }
        sr.Close();
        fs.Close();
        return returnString;
    }
    public static void WriteRules(string[] Rules)
    {
        FileStream fs = new FileStream(pathToRules, FileMode.OpenOrCreate ); //opnar straum í .txt skjal
        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine(Application.loadedLevelName);
        sw.WriteLine("-----------------------");

        foreach (string rule in Rules)
        {
           
            sw.WriteLine(rule); //Skrifar í strauminn nafnið á levelinu þar sem þetta fall er kalla
        }
        sw.WriteLine("---------------------------");
        sw.Close();
        fs.Close();
    }
    public static string[] ReadRules(string keyword, int maxArraySize)
    {
        streamContent = new string[maxArraySize];
        FileStream fs = new FileStream(pathToRules, FileMode.Open);
        StreamReader sr = new StreamReader(fs);

        keyword = "--KeyWord-" + keyword;
        int i = 0;
        bool grabStuff = false;
        Debug.Log(sr.ReadToEnd());
        while (!sr.EndOfStream)
        {
            Debug.Log(sr.ReadLine());
            if (sr.ReadLine() == keyword)
            {
                grabStuff = true;
            }
            if (sr.ReadLine() == "--END_GRAB")
            {
                break;
            }
            if (grabStuff)
            {
                streamContent[i] = sr.ReadLine();
                i++;
                if( i >= maxArraySize)
                {
                  break;
                }
            }

        }

        sr.Close();
        fs.Close();
        return streamContent;
    }
}
