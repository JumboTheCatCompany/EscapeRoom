using UnityEngine;

[CreateAssetMenu(menuName = "Prototype_EscapeRoom/Area")]
public class Area : ScriptableObject
{
    [TextArea]
    public string description;
    public string examineDescription;
    public Objects[] objectsInArea;
    public Items[] itemsInArea;
    public ChangeArea[] exits;
}