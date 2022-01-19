using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Prototype_EscapeRoom/Items")]
public class Items : ScriptableObject
{
    [TextArea]
    public new string name;
    public string description;

    public string Name { get => name; set => name = value; }
    private string Description { get => description; set => description = value; }
    
}