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
	private float header;

	public GUI_Placement buttonHolderLeft;
	public GUI_Placement buttonHolderMiddle;
	public GUI_Placement buttonHolderRight;

	private float unitButtonHeight;
	private float unitButtonWidth;

	public float unitActionHeight;
	public float unitActionWidth;

	private bool unitSpawned = false;
	private Selector selector;
	private Tile tileOver;
	private string cancelText = "Cancel";
	private bool adminToggle;

	public bool UnitSpawning
	{
		get { return unitSpawned; }
	}
	
	void Start() 
	{
		adminToggle = Properties.AdminMode;
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
						if(!selector.overGUI)
						{
							spawnedUnit.SendMessage ("TileOver", tileOver.gameObject);
							spawnedUnit.SendMessage ("Spawned", false);
							tileOver.OccupyingUnit(spawnedUnit);
							unitSpawned = false;
						}
					}
				}
			}
			else if(selector.MouseOverObject == spawnedUnit)
			{
				if(tileOver.Occupied == false)
				{
					if(Input.GetMouseButtonDown(1))
					{
						if(!selector.overGUI)
						{
							spawnedUnit.SendMessage ("TileOver", tileOver.gameObject);
							spawnedUnit.SendMessage ("Spawned", false);
							tileOver.OccupyingUnit(spawnedUnit);
							unitSpawned = false;
						}
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
		header = buttonHolderRight.Border;

		unitButtonWidth = (buttonHolderRight.InnerWidth/2)- (buttonHolderRight.Border/2);
		unitButtonHeight = buttonHolderRight.InnerHeight - header;

		unitActionHeight = ((buttonHolderRight.InnerHeight) - ((buttonHolderLeft.Border/2) + header))/2;
		unitActionWidth = ((buttonHolderRight.InnerHeight) - ((buttonHolderLeft.Border/2) + header));

		if(unitSpawned || selector.Unit_Selected)
		{
			unitButtonsActive = true;
		}
		else
		{
			unitButtonsActive = false;
		}

		if(GUI.Button(new Rect(buttonHolderRight.LeftPosition, buttonHolderRight.TopPosition + header, unitButtonWidth, unitButtonHeight), "Turret"))
		{
			if(!unitSpawned)
			{
				SpawnUnit(1);
			}
		}
		if(GUI.Button(new Rect(buttonHolderRight.RightPosition - ((buttonHolderRight.InnerWidth/2) - (buttonHolderRight.Border/2)), buttonHolderRight.TopPosition + header, unitButtonWidth, unitButtonHeight), "Block"))
		{
			if(!unitSpawned)
			{
				SpawnUnit(2);
			}
		}

		if(GUI.Button(new Rect( buttonHolderLeft.LeftPosition, buttonHolderLeft.TopPosition + header, unitButtonHeight, unitButtonHeight),"Main Menu"))
		{
			Application.LoadLevel(0);
		}

		GUI.Label(new Rect(buttonHolderLeft.CenterX - 54, buttonHolderLeft.CenterY - 25, 100, 25)," Enemy Kills: "+Properties.EnemiesKilled);
		GUI.Label(new Rect(buttonHolderLeft.CenterX - 50, buttonHolderLeft.CenterY, 130, 25), "Enemies Passed: "+Properties.EnemiesPassed);

		adminToggle = GUI.Toggle(new Rect(buttonHolderLeft.CenterX - 54, buttonHolderLeft.CenterY + 25, 100, 15), adminToggle, " Admin Mode");
		
		Properties.AdminMode = adminToggle;


		GUI.enabled = unitButtonsActive;

		if(GUI.Button(new Rect((buttonHolderLeft.RightPosition - unitActionWidth), buttonHolderLeft.TopPosition + header, unitActionWidth, unitActionHeight), "Rotate"))
		{
			if(unitSpawned)
			{
				spawnRotation *= Quaternion.Euler(new Vector3(0f,90f,0f));
			}
		}

		if(GUI.Button(new Rect((buttonHolderLeft.RightPosition - unitActionWidth), buttonHolderLeft.BottomPosition - unitActionHeight, unitActionWidth, unitActionHeight), cancelText))
		{
			if(unitSpawned)
			{
				spawnedUnit.SendMessage("Destroy", false);
				Destroy(spawnedUnit);
				unitSpawned = false;
			}
			else if(selector.Unit_Selected)
			{
				if(selector.UnitSelected.tag != "Enemy")
				{
					selector.UnitSelected.SendMessage("Destroy", true);
					Destroy(selector.UnitSelected);
					selector.SelectionDestroyed();
				}
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
			if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Delete))
			{
				spawnedUnit.SendMessage("Destroy", false);
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

		if(selector.Unit_Selected)
		{
			if(Input.GetKeyDown(KeyCode.Delete))
			{
				if(selector.UnitSelected.tag != "Enemy")
				{
					selector.UnitSelected.SendMessage("Destroy", true);
					Destroy(selector.UnitSelected);
					selector.SelectionDestroyed();
				}
			}

			cancelText = "Delete";
		}
		else
		{
			cancelText = "Cancel";
		}

	}
}
