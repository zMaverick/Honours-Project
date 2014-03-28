using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour 
{
	private GameObject objectOver;

	private RaycastHit lastHit;
	public string previousHit = "";

	public GameObject unitSelected;
	public GameObject unitOver;
	public GameObject lastSelected;
	public bool unitHovering;
	private _GUI gui;
	public bool unit_Selected = false;
	public GUI_Placement mainBackground;
	public GUI_Placement mapBackground;
	public bool overGUI = false;

	public GameObject MouseOverObject
	{
		get { return objectOver; }
		set { objectOver = value; }
	}

	public GameObject UnitSelected
	{
		get { return unitSelected; }
	}

	public bool Unit_Selected
	{
		get { return unit_Selected; }
	}

	void Start() 
	{
		objectOver = new GameObject();
		//lastSelected = new GameObject();

		gui = this.GetComponent<_GUI>();

	}

	void Update()
	{
		if(!overGUI)
		{
			if (unitOver != null && !gui.UnitSpawning)
			{
				if(Input.GetMouseButtonDown(0))
				{
					Debug.Log ("WORKS");
					unitSelected = unitOver;
					UnitSelection();
				}
			}
		}

		unitHovering = false;
	}

	void LateUpdate()
	{
		Rect mainBar = new Rect(mainBackground.LeftPosition, mainBackground.TopPosition, mainBackground.Width, mainBackground.Height);
		Rect mapBar = new Rect(mapBackground.LeftPosition, mapBackground.TopPosition, mapBackground.Width, mapBackground.Height);

		Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

		if(mainBar.Contains(mousePos) || mapBar.Contains(mousePos))
		{
			overGUI = true;
		}
		else
		{
			overGUI = false;
		}
		Raycaster();
	}

	void Raycaster()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//Does the RayCast Hit something?
		if(overGUI)
		{
			try
			{
				lastHit.collider.gameObject.SendMessageUpwards("CursorHover", false);
			}
			catch
			{
				return;
			}
		}
		else
		{
			if (Physics.Raycast(ray, out hit, 100))
			{
				//Is it the First Ever Hit?
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

					if(lastHit.collider != null)
					{
						if(lastHit.collider.gameObject.transform.parent != null)
						{
							previousHit = lastHit.collider.gameObject.transform.parent.name;
						}
						else
						{
							previousHit = lastHit.collider.gameObject.transform.name;
						}
					}
					else
					{
						previousHit = "DestroyedObject";
						lastHit = hit;
					}

					if(currentHit != previousHit)
					{
						hit.collider.gameObject.SendMessageUpwards("CursorHover", true);
						lastHit.collider.gameObject.SendMessageUpwards("CursorHover", false);
					}
					else
					{
						hit.collider.gameObject.SendMessageUpwards("CursorHover", true);
					}
				}
				else
				{
					hit.collider.gameObject.SendMessageUpwards("CursorHover", true);
					lastHit = hit;
					previousHit = lastHit.collider.gameObject.transform.name;
				}

				lastHit = hit;
				objectOver = hit.collider.gameObject;
			}
			else
			{
				try
				{
					lastHit.collider.gameObject.SendMessageUpwards("CursorHover", false);
				}
				catch
				{
					return;
				}
			}
		}
	}

	void UnitSelection()
	{
		if(lastSelected == null)
		{
			Debug.Log ("First Object Selected?");
			unitSelected.SendMessageUpwards("Selected", true);
			unit_Selected = true;
		}
		else
		{
			if(unitSelected != lastSelected && unitHovering)
			{
				Debug.Log ("Different Object Selected");
				unitSelected.SendMessageUpwards("Selected", true);
				lastSelected.SendMessageUpwards("Selected", false, SendMessageOptions.DontRequireReceiver);
				unit_Selected = true;
			}
			else if(unitSelected == lastSelected)
			{
				if(unitSelected == objectOver.transform.root.gameObject)
				{
					Debug.Log ("Same Object Selected");
					unitSelected.SendMessageUpwards("Selected", true);
					unit_Selected = true;
				}
				else
				{
					Debug.Log ("No Object Selected");
					unitSelected.SendMessageUpwards("Selected", false);

					unit_Selected = false;
				}
			}
		}
		lastSelected = unitSelected;
	}

	void Select(GameObject selected)
	{
		unitOver = selected;
		unitHovering = true;
	}

	public void SelectionDestroyed()
	{
		unit_Selected = false;
		unitSelected = null;
		lastSelected = null;
	}
}