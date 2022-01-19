using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;

    private GameController controller;
    

    private void Awake()
    {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        controller.LogStringWithReturn(userInput);
        char[] wordSeparation = {' '};
        string[] singleinputAction = userInput.Split(wordSeparation);
        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            if (inputAction.keyWord == singleinputAction[0])
            {
                inputAction.RespondToInput(controller,singleinputAction);
            }
        }
        InputComplete ();
    }

    void InputComplete()
    {
        
        inputField.ActivateInputField();
        inputField.text = null;
    }
}
