using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public Text DisplayText;
	public InputAction[] inputActions;
	[HideInInspector] public RoomNavigation roomNavigation;
	[HideInInspector] public List<string> interactionDescriptionsinArea = new List<string>();
	private List<string> actionLog = new List<string>();

	private void Awake()
	{
		roomNavigation = GetComponent<RoomNavigation>();
	}

	private void Start()
	{
		DisplayAreaText();
		DisplayLoggedText();
	}

	public void DisplayLoggedText()
	{
		string logAsText = string.Join("\n", actionLog.ToArray());

		foreach (var action in actionLog)
		{
			DisplayText.text = action;
		}
	}

	public void DisplayAreaText()
	{
		ClearAllForNewArea();

		string combinedText = roomNavigation.currentArea.description + "\n";
		LogStringWithReturn(combinedText);
	}

	private void ClearAllForNewArea()
	{
		interactionDescriptionsinArea.Clear();
		roomNavigation.ClearExits();
	}

	public void LogStringWithReturn(string stringToAdd)
	{
		actionLog.Add(stringToAdd + "\n");
		DisplayText.text = string.Join("\n", stringToAdd);
	}
}