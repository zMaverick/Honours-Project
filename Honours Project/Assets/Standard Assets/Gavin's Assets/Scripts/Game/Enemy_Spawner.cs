using UnityEngine;
using System.Collections;

public class Enemy_Spawner : MonoBehaviour {

	public GameObject enemy;
	private GameObject currentEnemy;
	public int spawnTime;
	private double lastTime = 0;
	private int count = 1;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		double currentTime = Time.time;

		if(currentTime - lastTime >= spawnTime)
		{
			currentEnemy = (GameObject)Instantiate(enemy, this.transform.position, this.transform.rotation);
			currentEnemy.name = "Enemy "+count;
			count++;
			lastTime = currentTime;
		}

	}
}
