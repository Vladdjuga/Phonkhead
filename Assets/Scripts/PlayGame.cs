using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
	public Button yourButton;

	void Start()
	{

	}

	void OnMouseUpAsButton()
	{
		Debug.Log("You have clicked the button!");
		Application.LoadLevel("Game");
	}
}
