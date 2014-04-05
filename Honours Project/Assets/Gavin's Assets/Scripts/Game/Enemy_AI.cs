using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public float speed = 10f;

	private Vector2 startNode;
	private Vector2 goalNode;

	private List<Vector3> myPath = new List<Vector3>();
	private bool pathRecieved = false;

	public int Health
	{
		get { return health; }
	}
	// Use this for initialization
	void Start () 
	{
		defaultMaterial = strongMaterial;
		childRenders = GetComponentsInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(health <=0)
		{
			Properties.EnemiesKilled++;
			Properties.EnemiesSpawned--;
			Destroy (gameObject);
		}

		if(myPath.Count > 0)
		{
			transform.position = Vector3.MoveTowards(transform.position, myPath[0], speed * Time.deltaTime);
			Vector3 dir = transform.position - myPath[0];
			if(dir.magnitude > 0f)
			{
				transform.rotation = Quaternion.LookRotation (-dir);
			}

			if(transform.position == myPath[0])
			{
				myPath.RemoveAt(0);
			}
			pathRecieved = true;

		}
		if(myPath.Count == 0 && pathRecieved)
		{
			Properties.EnemiesPassed++;
			Properties.EnemiesSpawned--;
			Destroy (gameObject);
		}



		for(int i = 0; i < myPath.Count; i++)
		{
			if(i > 0)
			{
				Debug.DrawLine(myPath[i-1], myPath[i]);
			}
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

	void StartPosition(Vector2 startPos)
	{
		startNode = startPos;
	}

	void FindPath(Vector2 goalPos)
	{
		goalNode = goalPos;
		GameObject.Find (".Game Properties").GetComponent<Login>().RequestPath(startNode, goalNode, this.gameObject);
	}

	void Path(Vector2[] path)
	{
		Vector2[] tempPath = path;
		System.Array.Reverse(tempPath);

		for(int i = 0; i < tempPath.Length; i++)
		{
			GameObject pathNode = GameObject.Find("Node "+(int)tempPath[i].x+"-"+(int)tempPath[i].y);
			myPath.Add(new Vector3(pathNode.transform.position.x, transform.position.y ,pathNode.transform.position.z));
		}

		GameObject endNode = GameObject.Find("Node "+(int)goalNode.x+"-"+(int)goalNode.y);
		myPath.Add(new Vector3(endNode.transform.position.x, transform.position.y ,endNode.transform.position.z));

	}
}
