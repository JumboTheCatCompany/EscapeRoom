using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Prototype_EscapeRoom/InputAction/Use")]
public class Use : InputAction
{
    [HideInInspector] public TextInput textInput;
    public override void RespondToInput(GameController controller, string[]inputAction)
    {
        try
        {
            controller.UseItem(inputAction[1]);
        }
        catch (IndexOutOfRangeException)
        {
            DisplayText.variableText1[0] = "You need to say what you want to use!";
        }
    }
}
