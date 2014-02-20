using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour {


	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.Translate (0, 0, speed * Time.deltaTime);
	}
	void Damage(GameObject damager)
	{
	}
}
