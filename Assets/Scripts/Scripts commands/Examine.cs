using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
[CreateAssetMenu(menuName = "Prototype_EscapeRoom/InputAction/Examine")]
public class Examine : InputAction
{
    [HideInInspector] public TextInput textInput;
    public override void RespondToInput(GameController controller, string[] inputAction)
    {
        try
        {
            controller.DisplayExamineText(inputAction[1]);
        }
        catch (IndexOutOfRangeException)
        {
            DisplayText.variableText1[0] = "You need to say what you want to examine";

        }
        

    }
    
}
