using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour 
{
	public GameObject turretBullet;
	private int health = 100;
	public GameObject body;
	public Material defaultMaterial;
	public Material selectMaterial;

	public int Health
	{
		get { return health; }
	}

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
			health = health - 1;
		}
	}

	void Selected(bool isSelected)
	{
		if(isSelected)
		{
			body.renderer.material = selectMaterial;
		}
		else
		{
			body.renderer.material = defaultMaterial;
		}
	}
	
	void CursorHover(bool mouseHover)
	{
		GameObject.Find ("GUI").SendMessage("Select", this.gameObject);
	}
}
