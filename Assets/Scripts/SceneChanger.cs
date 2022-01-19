using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public GameObject helpPanel;

	public void ShowPanel()
	{
		helpPanel.SetActive(true);
	}

	public void ClosePanel()
	{
		helpPanel.SetActive(false);
	}

	public void ZmienScene()
	{
		SceneManager.LoadScene(1);
	}
	
	public void Wyjdz()
	{
		Application.Quit();	
	}
}