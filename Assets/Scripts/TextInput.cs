using System;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;

    private GameController controller;
    public static string input00;
    public static string input11;
    

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }
    
    private void Update()
    {
        // Detect if user clicked enter, and if input field is not null
        // then run Analyze and Accept
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(inputField.text))
        {
            
                AnalyzeUserInput(inputField.text);
                AcceptStringInput(inputField.text);
            
        }
        
    }

    public void AnalyzeUserInput(string userInput)
    {
        userInput = userInput.ToLower();
        char[] wordSeparation = { ' ' };
        string[] inputArray = userInput.Split(wordSeparation);
        try
        {
            input00 = inputArray[0];
            //Debug.Log(input00);
            switch (input00)
            {
                case "/help":
                    string help = @$"
----------------------------HELP---------------------------------
In the Room you can interact with your surroundings
You need to figure out how to use what you find to escape
Use syntax verb + sth to explore and conquer the game
Available verbs: GO, EXAMINE, LOOK (AROUND), OPEN, TAKE, USE, CUT 
Examples: Go north, Examine area, Take key
You can also DISPLAY or SHOW Inventory or just type Inventory
------------------------------------------------------------------";
                    DisplayText.variableText1[0] = help;
                    Debug.Log("Help");
                    break;

                case "inventory":
                    DisplayText.variableText1[0] = "- - - ..:::Inventory:::.. - - -" + "\n" +
                                                   string.Join("\n", controller.Inventory);
                    break;

                default:
                    break;
            }

            input11 = inputArray[1];

            if (input00 == "show" && input11 == "inventory" || input00 == "display" && input11 == "inventory")
            {
                DisplayText.variableText1[0] =
                    "- - - ..:::Inventory:::.. - - -" + "\n" + string.Join("\n", controller.Inventory);
            }
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log("Index out of range");
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.Log("Argument out of range"); 
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }        
    }

    private void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        char[] wordSeparation = { ' ' };
        string[] singleinputAction = userInput.Split(wordSeparation);

        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            if (inputAction.keyWord == singleinputAction[0])
            {
                inputAction.RespondToInput(controller, singleinputAction);
            }
        }
        InputComplete();
    }

    private void InputComplete()
    {
        inputField.ActivateInputField();
        inputField.text = null;
    }
}