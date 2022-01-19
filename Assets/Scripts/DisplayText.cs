using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public Text myTextArea;
    public Text myTextVar;
    [HideInInspector] public RoomNavigation roomNavigation;
    public static string[] variableText1 = new string[1];
    
    private void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
    }

    private void DisplayVariableText()
    {
        string variableText = variableText1[0];
        myTextVar.text = variableText;
    }

    
    public void DisplayAllText()
    {
        DisplayAreaText();
        DisplayVariableText();
    }

    private void Start()
    {
        try
        {
            string roomDescription = roomNavigation.currentArea.description;
            myTextArea.text = "\n" + roomDescription;
            myTextVar.text = "Welcome to the game, if you are lost press /help";
        }
        catch (NullReferenceException)
        {
        }
    }

    public void DisplayAreaText()
    {
        string areaText = roomNavigation.currentArea.description + "\n";
        myTextArea.text = areaText;
    }

    private void Update()
    {
        try
        {
            if (Input.GetKey(KeyCode.Return))
            {
                DisplayAllText();
            }
        }
        catch (NullReferenceException)
        {
        }
    }
}