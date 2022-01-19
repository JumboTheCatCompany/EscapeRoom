using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Prototype_EscapeRoom/Objects")]
public class Objects : ScriptableObject
{
  
  [TextArea]
  public new string name;
  public string description;
  
  public string Name { get => name; set => name = value; }
  private string Description { get => description; set => description = value; }
  
  
}
 