using UnityEngine;
using System.Collections;

public class Enemy_Spawner : MonoBehaviour {

	public GameObject enemy;
	private GameObject currentEnemy;
	private double lastTime = 0;
	private int count = 0;

	// Use this for initialization
	void Start () 
	{
	}

	void OnLevelWasLoaded()
	{
		lastTime = Time.time;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		double currentTime = Time.time;

		if(currentTime - lastTime >= Properties.SpawnTime)
		{
			if(Properties.SpawnTime > 0)
			{
				SpawnEnemy();
				lastTime = currentTime;
			}
		}
	}

	public void SpawnEnemy()
	{
		Vector2 spawnPos = Properties.StartPoints[Random.Range (0,Properties.StartPoints.Length - 1)];
		Vector2 goalPos = Properties.GoalPoints[Random.Range (0,Properties.GoalPoints.Length - 1)];

		Vector3 spawnNode = GameObject.Find ("Node "+(int)spawnPos.x+"-"+(int)spawnPos.y).transform.position;

		Vector3 spawnPoint = new Vector3(spawnNode.x, this.transform.position.y, spawnNode.z);

		currentEnemy = (GameObject)Instantiate(enemy, spawnPoint, this.transform.rotation);
		currentEnemy.name = "Enemy "+(count+1);
		currentEnemy.SendMessage("StartPosition", spawnPos);
		currentEnemy.SendMessage("FindPath", goalPos);
		count++;
		Properties.EnemiesSpawned = count;
	}
}
