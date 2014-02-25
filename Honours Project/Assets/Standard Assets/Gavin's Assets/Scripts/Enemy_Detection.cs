using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Detection : MonoBehaviour 
{
	public float radius;
	private bool enemyActive = false;
	public List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		enemies.Clear();

		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);

		foreach(Collider hitObject in hitColliders)
		{
			if(hitObject.gameObject.tag == "Enemy")
			{
				enemies.Add(hitObject.gameObject);
			}
		}

		if(enemies.Count > 0)
		{
			SendMessageUpwards("Target", enemies[0]);
			enemyActive = true;
		}
		else
		{
			if(enemyActive)
			{
				SendMessageUpwards("ResetTurret");
				enemyActive = false;
			}

		}


	}

}
