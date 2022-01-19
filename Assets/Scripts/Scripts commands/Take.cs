using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Prototype_EscapeRoom/InputAction/Take")]
public class Take : InputAction
{
    [HideInInspector] public TextInput textInput;
    public override void RespondToInput(GameController controller, string[] inputAction)
    {
        try
        {
            controller.TakeItem(inputAction[1]);
            
        }
        catch (IndexOutOfRangeException)
        {
            DisplayText.variableText1[0] = "You need to say what you want to take!";
        }
        
    }
}
