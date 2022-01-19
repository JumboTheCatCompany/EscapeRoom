using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Area currentArea;

    public Dictionary<string, Area> moveDictionary = new Dictionary<string, Area>();
    public Dictionary<string, string> objectsAreaDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> itemsAreaDictionary = new Dictionary<string, string>();
    
    [HideInInspector] public TextInput textInput;
    
    private readonly string wall = "You're not a ghost cannot walk through walls!";
    private string randomdir;
    private readonly string emptydir = "You need to enter direction to go somewhere!";
    
    private GameController controller;
    private readonly string[] directions = {
        "north",
        "northeast",
        "northwest",
        "south",
        "southeast",
        "southwest",
        "east",
        "west",
    };

    private void Awake()
    {
        controller = GetComponent<GameController>();
        textInput = GetComponent<TextInput>();
    }

    public void AddExitsInArea()
    {
        for (int i = 0; i < currentArea.exits.Length; i++)
            moveDictionary.Add(currentArea.exits[i].areaKey, currentArea.exits[i].valueArea);
    }
    
    public void TryChangeArea(string direction)
    {
        AddExitsInArea();
        try
        {
            string directionVal = TextInput.input11;
            if (moveDictionary.ContainsKey(direction))
            {
                currentArea = moveDictionary[direction];
                controller.AddAllItems();
                DisplayText.variableText1[0] = null;
            }
            else if (directions.Contains(directionVal))
            {
                DisplayText.variableText1[0] = wall;
            }
            else if (string.IsNullOrEmpty(directionVal))
            {
                DisplayText.variableText1[0] = emptydir;
            }
            else if (!directions.Contains(directionVal))
            {
                randomdir = string.Format("Direction ..::: {0} :::.. doesn't exist! Do you need to go back to your geography classes?", directionVal);
                DisplayText.variableText1[0] = randomdir;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            ClearExits();
        }
    }

    public void ClearExits()
    {
        moveDictionary.Clear();
    }
}