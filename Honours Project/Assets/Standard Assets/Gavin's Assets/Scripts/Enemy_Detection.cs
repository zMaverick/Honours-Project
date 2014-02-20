using UnityEngine;
using System.Collections;

public class Enemy_Detection : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnTriggerEnter (Collider enemy)
	{
		if (enemy.gameObject.name == "Enemy") 
		{
			SendMessageUpwards("Target", enemy.gameObject);
		}
	}

}
