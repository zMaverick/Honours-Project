using UnityEngine;
using System.Collections;

public class _GUI : MonoBehaviour {

	public Texture unitTexture;
	public GameObject unit1;
	public GameObject unit2;

	private GameObject spawnedUnit;

	private float screenHeight;
	private float screenWidth;

	private bool unitSpawned = false;
	private Selector selector;
	private Tile tileOver;

	public bool UnitSpawning
	{
		get { return unitSpawned; }
	}
	
	void Start() 
	{
		selector = this.GetComponent<Selector>();
		screenHeight = Screen.height;
		screenWidth = Screen.width;
	}
	
	// Update is called once per frame
	void Update()
	{
		//Debug.Log (objectOver.name);
		if (unitSpawned)
		{
			//tileOver = selector.MouseOverObject.GetComponent<Tile>();

			Debug.Log ("Spawned");
			if(selector.MouseOverObject.tag == "Tile")
			{
				tileOver = selector.MouseOverObject.GetComponent<Tile>();
				Debug.Log ("Tile");
				if(tileOver.Occupied == false)
				{
					spawnedUnit.transform.position = new Vector3 (selector.MouseOverObject.transform.position.x, spawnedUnit.transform.position.y ,selector.MouseOverObject.transform.position.z);

					if(Input.GetMouseButton(1))
				   	{
						spawnedUnit.SendMessage ("Spawned", false);
						tileOver.Occupied = true;
						tileOver.Occupier = spawnedUnit;
						unitSpawned = false;
					}
				}
			}
			else if(selector.MouseOverObject == spawnedUnit)
			{
				Debug.Log ("Unit");
				if(tileOver.Occupied == false)
				{
					if(Input.GetMouseButton(1))
					{
						spawnedUnit.SendMessage ("Spawned", false);
						tileOver.Occupied = true;
						tileOver.Occupier = spawnedUnit;
						unitSpawned = false;
					}
				}
			}
		}

		InputManager();
	}

	void OnGUI() 
	{
		if(GUI.Button(new Rect(screenWidth/2 -30, screenHeight - (50+50), 50, 50), "1"))
		{
			if(!unitSpawned)
			{
				SpawnUnit(1);
			}
		}
		if(GUI.Button(new Rect(screenWidth/2 + 30, screenHeight - (50+50), 50, 50), "2"))
		{
			if(!unitSpawned)
			{
				SpawnUnit(2);
			}
		}
	}

	void SpawnUnit(int unit)
	{
		if (unit == 1)
		{
			spawnedUnit = (GameObject)Instantiate(unit1);
		}
		else if(unit == 2)
		{
			spawnedUnit = (GameObject)Instantiate(unit2);
		}

		spawnedUnit.SendMessage ("Spawned", true);
		unitSpawned = true;
	}

	void InputManager()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(!unitSpawned)
			{
				SpawnUnit(1);
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			if(!unitSpawned)
			{
				SpawnUnit(2);
			}
		}
	}
}
