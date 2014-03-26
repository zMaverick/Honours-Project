using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour 
{
	public GameObject turretBullet;
	private int health = 100;
	public GameObject body;
	public Material defaultMaterial;
	public Material strongMaterial;
	public Material weakMaterial;
	public Material selectMaterial;
	public Renderer[] childRenders;
	private bool alreadyWeak = false;
	private bool selected = false;

	public int Health
	{
		get { return health; }
	}

	public float speed;
	// Use this for initialization
	void Start () 
	{
		defaultMaterial = strongMaterial;
		childRenders = GetComponentsInChildren<Renderer>();
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
		if(!alreadyWeak)
		{
			if(health <= 50)
			{
				defaultMaterial = weakMaterial;

				if(!selected)
				{
					Selected(false);
				}

				alreadyWeak = true;
			}
			else
			{
				defaultMaterial = strongMaterial;
			}
		}
	}

	void Selected(bool isSelected)
	{
		selected = isSelected;
		if(isSelected)
		{
			foreach(Renderer child in childRenders)
			{
				child.material = selectMaterial;
			}
		}
		else
		{
			foreach(Renderer child in childRenders)
			{
				child.material = defaultMaterial;
			}
		}
	}
	
	void CursorHover(bool mouseHover)
	{
		GameObject.Find ("GUI").SendMessage("Select", this.gameObject);
	}
}
