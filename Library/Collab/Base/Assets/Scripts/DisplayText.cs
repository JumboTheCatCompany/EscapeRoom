using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    

public class DisplayText : MonoBehaviour
{
    
    public Text myText;
    public Text myText1;
    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public GameController gameController;
    List<string> actionLog = new List<string>();


    private GameController controller;
    
    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
        gameController = GetComponent<GameController>();
        
    }
    
    
    public void DisplayLoggedText()
    {
        
        string logAsText = string.Join("\n", actionLog.ToArray());

      
    }
   
    
    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }


    public void DisplayAllText()
    {
        string allText = string.Join("\n", roomNavigation.currentArea.description  );
        myText.text = allText;
    }
    
    void ClearAllForNewArea()
    {
      
        roomNavigation.ClearExits();
    }
    void Start()
    {
        string asciintro = @"        
██████╗  ██████╗  ██████╗ ███╗   ███╗
██╔══██╗██╔═══██╗██╔═══██╗████╗ ████║
██████╔╝██║   ██║██║   ██║██╔████╔██║
██╔══██╗██║   ██║██║   ██║██║╚██╔╝██║
██║  ██║╚██████╔╝╚██████╔╝██║ ╚═╝ ██║
╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝     ╚═ ";
        
        string roomDescription = roomNavigation.currentArea.description;
        myText.text ="\n" + asciintro  + roomDescription;

    }

    
    public void DisplayAreaText()
    {
        ClearAllForNewArea();
        string combinedText = roomNavigation.currentArea.description + "\n";
       
    }
    
    
    void Update()
    {
        
            if ( Input.GetKey(KeyCode.Return))
            {
                DisplayAllText();
                // myText.text = roomNavigation.currentArea.description ;
            }
       
        
        
    }
}


