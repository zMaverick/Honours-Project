/*using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Logging;

public class Connector : MonoBehaviour
{   
	private SmartFox smartFox;
	private bool shuttingDown = false;
	
	public string serverName = "127.0.0.1";
	public int serverPort = 9933;
	public string zone = "BasicExamples";
	public bool debug = true;
	
	public GUISkin sfsSkin;
	
	private string username = "Test";
	private string loginErrorMessage = "";	

	
	void OnApplicationQuit() 
	{
		shuttingDown = true;
	}
	
	void FixedUpdate() 
	{
		smartFox.ProcessEvents();
	}

	public void Awake()
	{
		if (SmartFoxConnection.IsInitialized) 
		{
			smartFox = SmartFoxConnection.Connection;
		} 
		else 
		{
			smartFox = new SmartFox(debug);
		}
		
		// Register callback delegate
		smartFox.AddEventListener(SFSEvent.CONNECTION, OnConnection);
		smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener(SFSEvent.LOGIN, OnLogin);
		smartFox.AddEventListener(SFSEvent.UDP_INIT, OnUdpInit);
		
		smartFox.AddLogListener(LogLevel.DEBUG, OnDebugMessage);
		
		smartFox.Connect(serverName, serverPort);
	}

	public void Start()
	{
		Debug.Log("Sending login request");
		smartFox.Send(new LoginRequest(username, "", zone));
	}
	
	private void UnregisterSFSSceneCallbacks() 
	{
		// This should be called when switching scenes, so callbacks from the backend do not trigger code in this scene
		smartFox.RemoveAllEventListeners();
	}

	//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
	// SFS2X event handlers
	//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
	
	public void OnConnection(BaseEvent evt) 
	{
		bool success = (bool)evt.Params["success"];
		string error = (string)evt.Params["error"];
		
		Debug.Log("On Connection callback got: " + success + " (error : <" + error + ">)");
		
		loginErrorMessage = "";
		if (success) {
			SmartFoxConnection.Connection = smartFox;
		} else {
			loginErrorMessage = error;
		}
	}
	
	public void OnConnectionLost(BaseEvent evt) 
	{
		loginErrorMessage = "Connection lost / no connection to server";
		UnregisterSFSSceneCallbacks();
	}
	
	public void OnDebugMessage(BaseEvent evt) 
	{
		string message = (string)evt.Params["message"];
		Debug.Log("[SFS DEBUG] " + message);
	}
	
	public void OnLogin(BaseEvent evt) 
	{
		bool success = true;
		if (evt.Params.Contains("success") && !(bool)evt.Params["success"]) {
			// Login failed - lets display the error message sent to us
			loginErrorMessage = (string)evt.Params["errorMessage"];
			Debug.Log("Login error: "+loginErrorMessage);
		} else {
			// Startup up UDP
			Debug.Log("Login ok");
			smartFox.InitUDP(serverName, serverPort);
		}			
	}
	
	public void OnUdpInit(BaseEvent evt) 
	{
		if (evt.Params.Contains("success") && !(bool)evt.Params["success"]) {
			loginErrorMessage = (string)evt.Params["errorMessage"];
			Debug.Log("UDP error: "+loginErrorMessage);
		} else {
			Debug.Log("UDP ok");
			
			// On to the lobby
			loginErrorMessage = "";
			UnregisterSFSSceneCallbacks();
			Application.LoadLevel("Game");
		}
	} 
}
*/