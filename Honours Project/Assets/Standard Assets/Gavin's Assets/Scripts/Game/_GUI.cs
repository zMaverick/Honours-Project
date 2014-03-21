using UnityEngine;
using System.Collections;

public class _GUI : MonoBehaviour {

	public Texture unitTexture;
	public GameObject unit1;
	public GameObject unit2;

	private GameObject spawnedUnit;
	private Quaternion spawnRotation = Quaternion.identity;

	private float screenHeight;
	private float screenWidth;

	public Transform buttonHolderLeft;
	public Transform buttonHolderMiddle;
	public Transform buttonHolderRight;

	public float unitButtonHeight;
	public float unitButtonWidth;

	public float unitActionHeight;
	public float unitActionWidth;

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
		if (unitSpawned)
		{
			if(selector.MouseOverObject.tag == "Tile")
			{
				tileOver = selector.MouseOverObject.GetComponent<Tile>();
				if(tileOver.Occupied == false)
				{
					spawnedUnit.transform.position = new Vector3 (selector.MouseOverObject.transform.position.x, spawnedUnit.transform.position.y ,selector.MouseOverObject.transform.position.z);

					if(Input.GetMouseButtonDown(1))
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
				if(tileOver.Occupied == false)
				{
					if(Input.GetMouseButtonDown(1))
					{
						spawnedUnit.SendMessage ("Spawned", false);
						tileOver.Occupied = true;
						tileOver.Occupier = spawnedUnit;
						unitSpawned = false;
					}
				}
			}

			spawnedUnit.transform.rotation = spawnRotation;

		}

		InputManager();
	}

	void OnGUI() 
	{
		bool unitButtonsActive;
		/*
		 * 
		private float screenHeight;
		private float screenWidth;
		
		public GameObject buttonHolderLeft;
		public GameObject buttonHolderMiddle;
		public GameObject buttonHolderRight;

		*/

		Vector2 bhLeftCentre = new Vector2((buttonHolderLeft.position.x * screenWidth), 
		                                   (screenHeight - (buttonHolderLeft.position.y * screenHeight)));
		Vector2 bhMiddleCentre = new Vector2((buttonHolderMiddle.position.x * screenWidth), 
		                                     (screenHeight - (buttonHolderMiddle.position.y * screenHeight)));
		Vector2 bhRightCentre = new Vector2((buttonHolderRight.position.x * screenWidth), 
		                                    (screenHeight - (buttonHolderRight.position.y * screenHeight)));


		if(unitSpawned || selector.Unit_Selected)
		{
			unitButtonsActive = true;
		}
		else
		{
			unitButtonsActive = false;
		}

		if(GUI.Button(new Rect(screenWidth/2 -30, screenHeight - (50+50), unitButtonWidth, unitButtonHeight), "Turret"))
		{
			if(!unitSpawned)
			{
				SpawnUnit(1);
			}
		}
		if(GUI.Button(new Rect(screenWidth/2 + 30, screenHeight - (50+50), unitButtonWidth, unitButtonHeight), "Block"))
		{
			if(!unitSpawned)
			{
				SpawnUnit(2);
			}
		}

		GUI.enabled = unitButtonsActive;

		if(GUI.Button(new Rect((bhLeftCentre.x - (unitActionWidth/2) - 55), bhLeftCentre.y - (unitActionHeight/2), unitActionWidth, unitActionHeight), "Rotate"))
		{
			if(unitSpawned)
			{
				spawnRotation *= Quaternion.Euler(new Vector3(0f,90f,0f));
			}
		}

		if(GUI.Button(new Rect((bhLeftCentre.x - (unitActionWidth/2) + 55), bhLeftCentre.y - (unitActionHeight/2), unitActionWidth, unitActionHeight), "Cancel"))
		{
			if(unitSpawned)
			{
				Destroy(spawnedUnit);
				unitSpawned = false;
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
		if(unitSpawned)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				spawnRotation *= Quaternion.Euler(new Vector3(0f,90f,0f));
			}
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Destroy(spawnedUnit);
				unitSpawned = false;
			}
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				SpawnUnit(1);
			}
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				SpawnUnit(2);
			}
		}

	}
}
