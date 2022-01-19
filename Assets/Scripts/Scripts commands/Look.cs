using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Prototype_EscapeRoom/InputAction/Look")]
public class Look : InputAction
{
    
    [HideInInspector] public TextInput textInput;
    public override void RespondToInput(GameController controller, string[] inputAction)
    {
        try
        {
            controller.DisplayLookText(inputAction[1]);
        }
        catch (IndexOutOfRangeException)
        {
            DisplayText.variableText1[0] = "You need to look  * around * to familiarize with your hopeless situation";
        }
        
    }
}
