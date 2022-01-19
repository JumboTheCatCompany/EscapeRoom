using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Prototype_EscapeRoom/InputAction/Cut")]
public class Cut : InputAction
{
    [HideInInspector] public TextInput textInput;
    public override void RespondToInput(GameController controller, string[] inputAction)
    {
        try
        {
            controller.CutCarpet(inputAction[1]);
        }
        catch (IndexOutOfRangeException)
        {
            DisplayText.variableText1[0] = "You need to say what you want to cut!";
        }
    }
}
