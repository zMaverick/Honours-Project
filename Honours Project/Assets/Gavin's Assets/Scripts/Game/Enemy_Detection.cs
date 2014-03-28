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
			GameObject currentTarget;
			currentTarget = CalculateTarget();
			SendMessageUpwards("Target", currentTarget);
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

	GameObject CalculateTarget()
	{
		float[] enemyDistance = new float[enemies.Count];

		for(int i = 0; i < enemies.Count; i++)
		{
			enemyDistance[i] = Vector3.Distance(enemies[i].transform.position, this.transform.position);
		}

		GameObject target = enemies[Lowest(enemyDistance)];

		return target;
	}

	int Lowest(float[] list)
	{
		int lowestIndex = -1;
		float lowestValue = 500f;

		for(int i = 0; i < list.Length; i++)
		{
			if(list[i] < lowestValue)
			{
				lowestValue = list[i];
				lowestIndex = i;
			}
		}

		return lowestIndex;
	}

}
