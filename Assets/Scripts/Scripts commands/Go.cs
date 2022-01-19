using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(menuName = "Prototype_EscapeRoom/InputAction/Go")]
public class Go : InputAction
{
    
    [HideInInspector] public TextInput textInput;
   public override void RespondToInput(GameController controller, string[] inputAction)
   {
       try
       {
          controller.roomNavigation.TryChangeArea(inputAction[1]);
           
       }

       catch (IndexOutOfRangeException)
       {
           DisplayText.variableText1[0] = "You need to enter direction to go somewhere!";
       }
       
   }
}
