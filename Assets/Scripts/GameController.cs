using System;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//To finish the game and go directly to the epilogue type: go east - take key - go west - open door
//Walkthrough intended: go north - take scissors - go west - take flashlight - go south - cut carpet - go east - go east -
// - use flashlight - take key - go west - use rag - open door
public class GameController : MonoBehaviour
{
    public InputAction[] inputActions;
    public List<string> Inventory = new List<string>();
    public Dictionary<string, string> AllItems = new Dictionary<string, string>();
    public List<string> ItemsAreaList = new List<string>();
    public List<string> ObjectsAreaList = new List<string>();
    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public DisplayText displayText;

    private readonly string notake1 = "You need to say what you want to take!";
    private string randomtake;
    private string randomuse;
    private string nonexistent;
    private string randomcut;
    private string noscissors;
    private string nullopen;
    private string randomopen;
    private string randomopen1;
    
    private void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
        displayText = GetComponent<DisplayText>();
    }

    public void AddInventoryItems()
    {
        
    }

    public void DisplayLookText(string lookOptions)
    {
        // Look around shows area description
        if (lookOptions == "around")
        {
            DisplayText.variableText1[0] = roomNavigation.currentArea.examineDescription;
        }
    }
    public void DisplayExamineText(string objectToExamine)
    {
        // Examine shows:
        // room/area description
        // objects in the room description
        // items in the room description
        // items in inventory description
        
        PrepareObjectsItemsExamine();
        PrepareItemsToBeAddedList();
        PrepareObjectsToBeAddedList();
        AddAllItems();
        if (objectToExamine == "room" || objectToExamine == "area")
        {
            DisplayText.variableText1[0] = roomNavigation.currentArea.examineDescription;
        }
        else if (Inventory.Contains(objectToExamine))
        {
            string description = AllItems[objectToExamine];
            DisplayText.variableText1[0] = description;
        }
        else if (ObjectsAreaList.Contains(objectToExamine))
        {
            string description = roomNavigation.objectsAreaDictionary[objectToExamine];
            DisplayText.variableText1[0] = description;
        }
        else if (ItemsAreaList.Contains(objectToExamine) && objectToExamine != "rag")
        {
            string description = roomNavigation.itemsAreaDictionary[objectToExamine];
            DisplayText.variableText1[0] = description;
        }
        else if (string.IsNullOrEmpty(objectToExamine))
        {
            DisplayText.variableText1[0] = "You need to say what you want to examine";
        }
        else
        {
            string nonexistent1 = TextInput.input11;
            nonexistent =
                string.Format(
                    "It's impossible to examine ..::: {0} :::.. you cannot see it around you and it's not in your inventory",
                    nonexistent1);
            DisplayText.variableText1[0] = nonexistent;
        }
        
        ClearObjects();
        
    }

    
    public void UseItem(string item)
    {
        // Use items to get clues how to finish the game:
        // 1. Take flashlight Area4 - Use flashlight Area 5 (reveals that there is key to be taken)
        // 2. CutCarpet() [see below] to get item rag - Use rag Area 1 (reveals that there is door to be opened)
        
        PrepareObjectsToBeAddedList();
        if (item == "flashlight" && Inventory.Contains("flashlight") && ObjectsAreaList.Contains("painting"))
        {
            DisplayText.variableText1[0] = Painting.descriptionLight;
        }
        else if (item == "flashlight" && Inventory.Contains("flashlight"))
        {
            DisplayText.variableText1[0] = "This room isn't dark enough to need using flashlight";
        }
        else if (item == "rag" && Inventory.Contains("rag") && ObjectsAreaList.Contains("blood"))
        {
            DisplayText.variableText1[0] = "***SUCCESS***" + "\n" +
                                           "You use the rag on the poodle of blood to wipe it. You can see a door underneath it!" +
                                           "\n" + "Maybe you can open it if you have a key in your inventory?" + "\n" +
                                           "***SUCCESS***";
        }
        else if (item == "rag" && Inventory.Contains("rag") && ObjectsAreaList.Contains("poodle"))
        {
            DisplayText.variableText1[0] = "***SUCCESS***" + "\n" +
                                           "You use the rag on the poodle of blood to wipe it. You can see a door underneath it!" +
                                           "\n" + "Maybe you can open it with a key?" + "\n" +
                                           "***SUCCESS***";
        }
        else
        {
            randomuse = string.Format("You cannot use ..::: {0} :::.. examine your surroundings and furniture for clues", item);
            DisplayText.variableText1[0] = randomuse;
        }

    }
    
    public void CutCarpet(string object1)
    {
        // Take scissors Area2 - Cut carpet Area6 or Area9 - "rag" added to inventory (Use rag Area1 to reveal door to be opened)
        PrepareObjectsToBeAddedList();
        if (object1 == "carpet" && Inventory.Contains("scissors") && ObjectsAreaList.Contains("carpet")
        && !Inventory.Contains("rag"))
        {
            Inventory.Add("rag");
            DisplayText.variableText1[0] = "***SUCCESS***" + "\n" +
                                            "You cut a piece of rag from the smelly carpet. Maybe you can use it to wipe something?" +
                                            "\n" +
                                            "***SUCCESS***";
        }
        else if (Inventory.Contains("scissors") && Inventory.Contains("rag") && ObjectsAreaList.Contains("carpet"))
        {
            DisplayText.variableText1[0] = "You have cut a piece of rag already - don't need more";
        }
        else if (Inventory.Contains("scissors"))
        {
            randomcut = string.Format("You cannot cut ..::: {0} :::.. with scissors!", object1);
            DisplayText.variableText1[0] = randomcut;
        }
        else 
        {
           noscissors = string.Format("You cannot cut ..::: {0} :::.. you have no scissors - find them and we will see if you can cut it", object1);
           DisplayText.variableText1[0] = noscissors;
        }
        ObjectsAreaList.Clear();
        
    }

    public void PrepareObjectsItemsExamine()
    {
        AddItemsInArea();
        AddObjectsInArea();
    }

    public void PrepareItemsToBeAddedList()
    {
        for (int i = 0; i < roomNavigation.currentArea.itemsInArea.Length; i++)
            ItemsAreaList.Add(roomNavigation.currentArea.itemsInArea[i].name);
    }
    public void PrepareObjectsToBeAddedList()
    {
        for (int i = 0; i < roomNavigation.currentArea.objectsInArea.Length; i++)
            ObjectsAreaList.Add(roomNavigation.currentArea.objectsInArea[i].name);
    }

    public void TakeItem(string item)
    {
        
        // Takes item if in the area and adds to inventory
        
        PrepareItemsToBeAddedList();
        PrepareObjectsToBeAddedList();
        try
        {
            if (ItemsAreaList.Contains(item) && !Inventory.Contains(item))
            {
                Inventory.Add(item);
                string itemTaken = string.Format("..::: {0} :::.. are now in your inventory", item);
                DisplayText.variableText1[0] = itemTaken;
                
            }
            else if (Inventory.Contains((item)))
            {
                string itemTaken1 = "You cannot take an item you have already taken!" + "\n" + string.Format("..::: {0} :::.. already in your inventory", item);
                DisplayText.variableText1[0] = itemTaken1;
            }
            else if (ObjectsAreaList.Contains(item))
            {
                string objectRoom = string.Format("You cannot take ..::: {0} :::.. it's a permanent fixture of this creepy room", item);
                DisplayText.variableText1[0] = objectRoom;

            }
            else if (string.IsNullOrEmpty(item))
            {
                DisplayText.variableText1[0] = notake1;
            }
            else
            {
                randomtake = string.Format(" This ..::: {0} :::.. that you want to take is not here", item);
                DisplayText.variableText1[0] = randomtake;
            }
            {
                
            }
        }
        catch
        {
        }
        
    }

    public void OpenDoor(string objectOpen)
    {
        // Final puzzle - key is in the inventory - Open door command in Area1 triggers end of the game
        PrepareObjectsToBeAddedList();
        if (objectOpen == "door" && Inventory.Contains("key") && ObjectsAreaList.Contains("door"))
        {
           LoadEpilogue();
        }
        else if (string.IsNullOrEmpty(objectOpen))
        {
            nullopen = "You need to say what you want to open";
            DisplayText.variableText1[0] = nullopen;
        }
        else if (!Inventory.Contains("key"))
        {
            randomopen = string.Format(" You cannot open ..::: {0} :::.. and anyway you need a key to open something", objectOpen);
            DisplayText.variableText1[0] = randomopen;
        }
        else
        {
            randomopen1 = string.Format(" You cannot open ..::: {0} :::.. try to open something else with your key", objectOpen);
            DisplayText.variableText1[0] = randomopen1;  
        }
    }
    
    public void LoadEpilogue()
    {
        SceneManager.LoadScene("Scenes/END");
    }

    public void AddItemsInArea()
    {
        for (int i = 0; i < roomNavigation.currentArea.itemsInArea.Length; i++)
            roomNavigation.itemsAreaDictionary.Add(roomNavigation.currentArea.itemsInArea[i].name,
               roomNavigation.currentArea.itemsInArea[i].description);
    }
    
    public void AddAllItems()
    {
        try
        {
            for (int i = 0; i < roomNavigation.currentArea.itemsInArea.Length; i++)
                AllItems.Add(roomNavigation.currentArea.itemsInArea[i].name,
                    roomNavigation.currentArea.itemsInArea[i].description);
        }
        catch (ArgumentException)
        {
            Debug.Log("Already exists in dictionary");
        }
        
    }
    

    public void AddObjectsInArea()
    {
        for (int i = 0; i < roomNavigation.currentArea.objectsInArea.Length; i++)
            roomNavigation.objectsAreaDictionary.Add(roomNavigation.currentArea.objectsInArea[i].name,
               roomNavigation.currentArea.objectsInArea[i].description);
    }
    
    
    
    public void ClearObjects()
    {
        roomNavigation.objectsAreaDictionary.Clear();
        roomNavigation.itemsAreaDictionary.Clear();
    }
}