using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour 
{
	private GameObject objectOver;

	private RaycastHit lastHit;
	public string previousHit = "";

	private GameObject unitSelected;
	private GameObject unitOver;
	private GameObject lastSelected;
	private _GUI gui;

	public GameObject MouseOverObject
	{
		get { return objectOver; }
	}

	public GameObject UnitSelected
	{
		get { return unitSelected; }
	}

	void Start() 
	{
		objectOver = new GameObject();
		lastSelected = new GameObject();

		gui = this.GetComponent<_GUI>();

	}

	void Update()
	{
		if (unitOver != null && !gui.UnitSpawning)
		{
			if(Input.GetMouseButton(0))
			{
				unitSelected = unitOver;
				UnitSelection();
			}
		}

		Raycaster();
	}

	void Raycaster()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//Does the RayCast Hit something?
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

	void UnitSelection()
	{
		if(unitSelected != lastSelected)
		{
			unitSelected.SendMessageUpwards("Selected", true);
			lastSelected.SendMessageUpwards("Selected", false);
		}
		else if(unitSelected == lastSelected)
		{
			if(unitSelected == objectOver)
			{
				unitSelected.SendMessageUpwards("Selected", true);
			}
			else
			{
				unitSelected.SendMessageUpwards("Selected", false);
			}
		}
		lastSelected = unitSelected;
	}

	void Select(GameObject selected)
	{
		unitOver = selected;
	}

}