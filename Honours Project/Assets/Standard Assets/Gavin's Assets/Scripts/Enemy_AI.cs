using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour 
{
	public GameObject turretBullet;
	public int health;


	public float speed;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.Translate (0, 0, speed * Time.deltaTime);

		if(health <=0)
		{
			Destroy (gameObject);
		}
	}
	void Damage(string damager)
	{
		if(damager == "Turret")
		{
			Debug.Log (health);
			health = health - 1;
		}
	}
}
