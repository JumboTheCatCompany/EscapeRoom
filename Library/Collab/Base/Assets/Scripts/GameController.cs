using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
   public Text DisplayText;
   public InputAction[] inputActions;
   [HideInInspector] public RoomNavigation roomNavigation;
   [HideInInspector] public List<string> interactionDescriptionsinArea = new List<string>();
   List<string> actionLog = new List<string>();
   void Awake()
   {
      roomNavigation = GetComponent<RoomNavigation>();
      
   }

   private void Start()
   {
      DisplayAreaText();
      DisplayLoggedText();
   }

   public void DisplayLoggedText()
   {
      string logAsText = string.Join("\n", actionLog.ToArray());

      DisplayText.text = logAsText;
   }
   
   public void DisplayAreaText()
   {
      ClearAllForNewArea();
      
      string combinedText = roomNavigation.currentArea.description + "\n";
      LogStringWithReturn( (combinedText));
   }

   void ClearAllForNewArea()
   {
      interactionDescriptionsinArea.Clear();
      roomNavigation.ClearExits();
   }
   
   
   
   public void LogStringWithReturn(string stringToAdd)
   {
      actionLog.Add(stringToAdd + "\n");
   }
}
