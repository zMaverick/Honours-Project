using UnityEngine;
using System.Collections;

public class Turret_Bullet : MonoBehaviour 
{
	public float bulletSpeed;
	public float lifeTime;

	private float spawnTime = 0.0f;

	// Use this for initialization
	void OnEnable () 
	{
		spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += transform.forward * bulletSpeed * Time.deltaTime;

		if(Time.time > spawnTime + lifeTime)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "Enemy")
		{
			collider.gameObject.SendMessageUpwards ("Damage", "Turret");
			Destroy (gameObject);
		}
	}
	void CursorHover(bool selected)
	{
		return;
	}
}
