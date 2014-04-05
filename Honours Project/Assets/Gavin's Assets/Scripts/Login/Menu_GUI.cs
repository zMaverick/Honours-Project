using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu_GUI : MonoBehaviour {

	public GameObject propertiesPrefab;
	public GameObject title;
	private bool adminToggle;

	// Use this for initialization
	void Start () 
	{
		adminToggle = Properties.AdminMode;

		if(GameObject.Find(".Game Properties") == null)
		{
			GameObject properties = (GameObject)Instantiate(propertiesPrefab, Vector3.zero, Quaternion.identity);
			properties.name = ".Game Properties";
		}
		else
		{
			return;
		}
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		float screenHeight = Screen.height;
		float screenWidth = Screen.width;

		float buttonWidth = screenWidth * 0.2f;
		float buttonHeight = screenHeight * 0.1f;
		float buttonPosX = (screenWidth * 0.5f) - (buttonWidth/2);

		float boxHeight = screenHeight * 0.4f;
		float boxWidth = screenWidth *0.3f;
		float boxPosX = (screenWidth * 0.5f) - (boxWidth/2);
		float boxPosY = (screenHeight * 0.6f) - (boxHeight/2);

		GUI.Box(new Rect(boxPosX, boxPosY, boxWidth, boxHeight),"");

		if(GUI.Button(new Rect(buttonPosX, (screenHeight * 0.5f) - (buttonHeight/2) , buttonWidth, buttonHeight ), "Game 1"))
		{
			Properties.ServerAddress = "localhost:5055";
			Properties.CloudAssisted = false;
			GameObject.Find (".Game Properties").GetComponent<AdminPanel>().serverIP = Properties.ServerAddress;
			GameObject.Find (".Game Properties").GetComponent<Login>().Connect();
			Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(buttonPosX, (screenHeight * 0.625f) - (buttonHeight/2), buttonWidth, buttonHeight ), "Game 2"))
		{
			Properties.ServerAddress = "137.135.181.176:5055";
			Properties.CloudAssisted = true;
			GameObject.Find (".Game Properties").GetComponent<AdminPanel>().serverIP = Properties.ServerAddress;
			GameObject.Find (".Game Properties").GetComponent<Login>().Connect();
			Application.LoadLevel(1);
		}



		adminToggle = GUI.Toggle(new Rect(buttonPosX - 25, screenHeight * 0.75f, 100, 15), adminToggle, " Admin Mode");

		Properties.AdminMode = adminToggle;
	}
}
