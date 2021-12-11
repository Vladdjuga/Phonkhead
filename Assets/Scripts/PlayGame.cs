using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{

	void Start()
	{
		Debug.Log("Start!");

	}
	public void OnMouseDown()
	{
		Debug.Log("You have clicked the button!");
		SceneManager.LoadScene("Loading");
	}
}
