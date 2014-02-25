using UnityEngine;
using System.Collections;

public class _GUI : MonoBehaviour {

	public Texture unitTexture;
	public GameObject unit1;

	private GameObject spawnedUnit;

	private float screenHeight;
	private float screenWidth;
	private GameObject objectOver;
	private RaycastHit lastHit;
	private string previousHit = "";
	private bool unitSpawned = false;

	private GameObject unitOver;
	private GameObject lastUnitOver;

	private Tile tileOver;
	// Use this for initialization
	void Start() 
	{
		objectOver = new GameObject();
		lastUnitOver = new GameObject();

		screenHeight = Screen.height;
		screenWidth = Screen.width;
	}
	
	// Update is called once per frame
	void Update()
	{
		//Debug.Log (objectOver.name);
		if (unitSpawned)
		{
			if(objectOver.transform.name == "tile_Main")
			{
				tileOver = objectOver.GetComponent<Tile>();

				if(tileOver.Occupied == false)
				{
					spawnedUnit.transform.position = new Vector3 (objectOver.transform.position.x, 0.8f ,objectOver.transform.position.z);

					if(Input.GetMouseButton(1))
				   	{
						spawnedUnit.SendMessage ("Spawned", false);
						tileOver.Occupied = true;
						tileOver.Occupier = spawnedUnit;
						unitSpawned = false;
					}
				}
				Debug.Log (objectOver.name);
			}
			else if(objectOver == spawnedUnit)
			{
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

		if(Input.GetMouseButton(0))
		{
			if(unitOver != lastUnitOver)
			{
				unitOver.SendMessage("Selected", true);
				lastUnitOver.SendMessage("Selected", false);
			}
			else if(unitOver == lastUnitOver)
			{
				if(unitOver == objectOver)
				{
					unitOver.SendMessage("Selected", true);
				}
				else
				{
					unitOver.SendMessage("Selected", false);
				}
			}
			lastUnitOver = unitOver;
		}

		Raycaster();
	}

	void OnGUI() 
	{

		if(GUI.Button(new Rect(screenWidth/2, screenHeight - (10+50), 50, 50), "1"))
		{
			if(!unitSpawned)
			{
				SpawnUnit(1);
			}
		}
	}

	void SpawnUnit(int unit)
	{
		if (unit == 1)
		{
			spawnedUnit = (GameObject)Instantiate(unit1);
			spawnedUnit.SendMessage ("Spawned", true);
		}

		unitSpawned = true;
	}

	void Raycaster()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit, 100))
		{
			if(previousHit.Length > 0)
			{
				string currentHit;

				if(hit.collider.gameObject.transform.parent != null)
				{
					currentHit = hit.collider.gameObject.transform.parent.name;
				}
				else
				{
					currentHit = hit.collider.gameObject.transform.name;
				}

				if(lastHit.collider.gameObject.transform.parent != null)
				{
					previousHit = lastHit.collider.gameObject.transform.parent.name;
				}
				else
				{
					previousHit = lastHit.collider.gameObject.transform.name;
				}

				if(currentHit != previousHit)
				{
					hit.collider.gameObject.SendMessage("CursorHover", true);
					lastHit.collider.gameObject.SendMessage("CursorHover", false);
				}
				else
				{
					hit.collider.gameObject.SendMessage("CursorHover", true);
				}
			}
			else
			{
				hit.collider.gameObject.SendMessage("CursorHover", true);
				lastHit = hit;
				previousHit = lastHit.collider.gameObject.transform.parent.name;
			}
			lastHit = hit;
			objectOver = hit.collider.gameObject;
		}
		else
		{
			try
			{
				lastHit.collider.gameObject.SendMessage("CursorHover", false);
			}
			catch
			{
				return;
			}
		}
	}

	void Selector(GameObject selected)
	{
		unitOver = selected;
	}



}
