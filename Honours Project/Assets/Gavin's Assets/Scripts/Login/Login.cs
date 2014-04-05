using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using System;

public class Login : MonoBehaviour 
{
	public PhotonConnector photon;
	private PhotonPeer peer;
	public string serverAddress = "137.135.181.176:5055";
	public bool connected = false;

	public Vector2 startPoint = new Vector2(0,0);
	public Vector2 endPoint= new Vector2(19, 19);
	public bool gridGenerated = false;
	private List<GameObject> requesters = new List<GameObject>();

	//Use this for initialization
	void Start () 
	{
		Properties.ServerAddress = serverAddress;
		photon = new PhotonConnector();
		peer = new PhotonPeer(photon, false);
		Connect();
	}
	
	// Update is called once per frame
	void Update () 
	{
		try
		{
			if(connected)
			{
				photon.Update();

			}
			else
			{
				Connect();

			}
		}
		catch(Exception e)
		{
			Debug.Log(e);
		}

		if(Properties.Status.Equals("Connected"))
		{
			if(!gridGenerated)
			{
				object[] gridGen = new object[2];
				gridGen[0] = 20;
				gridGen[1] = 20;
				
				photon.SendRequest(1, 1, gridGen, false);
				gridGenerated = true;
			}
		}

	}

	public void Connect()
	{
		if(Properties.Status == "Connected")
		{
			photon.Disconnect();
		}

		photon.Initialize(peer, Properties.ServerAddress, "Pathfinding", this);
		gridGenerated = false;
		connected = true;
	}

	void OnApplicationQuit()
	{
		try
		{
			photon.Disconnect();
		}
		catch(Exception e)
		{
			Debug.Log(e);
		}
	}

	public void RequestPath(Vector2 start, Vector2 goal, GameObject requester)
	{
		requesters.Add(requester);

		object[] path = new object[4];

		path[0] = (int)start.x;
		path[1] = (int)start.y;
		path[2] = (int)goal.x;
		path[3] = (int)goal.y;

		photon.SendRequest(3, 1, path, false);
	}

	public void NewPath(int[] messages, int requestID)
	{
		Vector2[] path = new Vector2[messages.Length/2];

		for (int i = 0; i < path.Length; i++)
		{
			path[i] = new Vector2(messages[i], messages[(path.Length)+i]);
		}

		requesters[requestID].SendMessage("Path", path);
	}

	public void DebugMessage(string message)
	{
		Debug.Log(message);
	}
}
