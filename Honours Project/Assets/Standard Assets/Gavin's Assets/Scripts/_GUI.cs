using UnityEngine;
using System.Collections;

public class _GUI : MonoBehaviour {

	public Texture unitTexture;
	public GameObject unit1;

	private GameObject spawnedUnit;

	private float screenHeight;
	private float screenWidth;
	private GameObject selectedTile;
	private RaycastHit lastHit;
	private string previousHit = "";
	private bool unitSpawned = false;

	// Use this for initialization
	void Start() 
	{
		selectedTile = new GameObject();

		screenHeight = Screen.height;
		screenWidth = Screen.width;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (unitSpawned)
		{
			spawnedUnit.transform.position = new Vector3 (selectedTile.transform.position.x, 0.8f ,selectedTile.transform.position.z);


			if(Input.GetMouseButton(1))
		   	{
				spawnedUnit.SendMessage ("Selected", false);
				unitSpawned = false;
			}
		}

		Raycaster();
	}

	void OnGUI() 
	{

		if(GUI.Button(new Rect(screenWidth/2, screenHeight - (10+50), 50, 50), unitTexture))
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
			spawnedUnit.SendMessage ("Selected", true);
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
				string currentHit = hit.collider.gameObject.transform.parent.name;
				previousHit = lastHit.collider.gameObject.transform.parent.name;
				
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
			selectedTile = hit.collider.gameObject;
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


}
