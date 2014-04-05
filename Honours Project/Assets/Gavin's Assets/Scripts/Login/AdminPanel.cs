using UnityEngine;
using System.Collections;

public class AdminPanel : MonoBehaviour 
{
	private float screenHeight;
	private float screenWidth;

	public string serverIP;
	private string spawnTime;
	private bool multiGoal;
	private bool multiStart;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(transform.gameObject);

		serverIP = Properties.ServerAddress;
		spawnTime = Properties.SpawnTime.ToString();
		multiGoal = Properties.ComplexGoal;
		multiStart = Properties.ComplexStart;
	}

	void Update()
	{

	}

	void OnGUI () 
	{
		screenHeight = Screen.height;
		screenWidth = Screen.width;

		float boxHeight = screenHeight * 0.1f;
		float boxWidth = screenWidth;
		float boxPosX = 0f;
		float boxPosY = 0f;

		if(Properties.AdminMode)
		{
			GUI.Box(new Rect(boxPosX, boxPosY, boxWidth, boxHeight),"Admin Panel");

			GUI.Label(new Rect(5, 5, 500, 20), "Status: "+Properties.Status);
			GUI.Label(new Rect(5, 25, 70, 20), "IP Address: ");
			serverIP = GUI.TextField(new Rect(80, 25, 135, 20), serverIP);
			GUI.Label(new Rect(5, 45, 500, 20), "Latency: "+Properties.ServerPing);

			if(GUI.Button(new Rect(screenWidth * 0.2f, 25, boxHeight + 30, boxHeight - 30), "Connect"))
			{
				Connnect();
			}

			if(Application.loadedLevel == 1)
			{
				multiStart = GUI.Toggle(new Rect(screenWidth * 0.5f, 25, 115, 20), multiStart, " Multiple Spawns");
				multiGoal = GUI.Toggle(new Rect(screenWidth * 0.5f, 45, 105, 20), multiGoal, " Multiple Goals");

				GUI.Label(new Rect(screenWidth * 0.65f, 25, 150, 20), "Active Enemies: "+Properties.EnemiesSpawned);

				GUI.Label(new Rect(screenWidth * 0.65f, 45, 80, 20), "Spawn Time: ");
				spawnTime = GUI.TextField(new Rect(screenWidth * 0.65f + 80, 45, 30, 20), spawnTime);

				if(GUI.Button(new Rect(screenWidth * 0.75f, 25, boxHeight + 30, boxHeight - 30), "Apply"))
				{
					Apply();
				}

				if(GUI.Button(new Rect(screenWidth * 0.85f, 25, boxHeight + 30, boxHeight - 30), "Spawn Enemy"))
				{
					GameObject.Find ("Enemy Spawner").GetComponent<Enemy_Spawner>().SpawnEnemy();
				}
		}
		}
	}

	void Apply()
	{
		Properties.SpawnTime = int.Parse(spawnTime);
		Properties.ComplexStart = multiStart;
		Properties.ComplexGoal = multiGoal;
	}

	void Connnect()
	{
		Properties.ServerAddress = serverIP;
		this.gameObject.GetComponent<Login>().Connect();
	}
}
