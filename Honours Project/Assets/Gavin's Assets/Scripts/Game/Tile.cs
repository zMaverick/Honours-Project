using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	private float x;
	private float y;
	private bool occupied;
	private GameObject occupier;

	public Color defaultColour;
	public Color occupiedColour = new Color(0.776f, 0f, 0f, 0.212f);

	public float X
	{
		get{ return x; }
		set{ x = value; }
	}

	public float Y
	{
		get{ return y; }
		set{ y = value; }
	}

	public bool Occupied
	{
		get { return occupied; }
		set { occupied = value; }
	}

	public GameObject Occupier
	{
		get { return occupier; }
		set { occupier = value; }
	}

#region Neighbours

	public GameObject[] neighbours = new GameObject[8];

	public GameObject Bottom_Left
	{
		get { return neighbours[0]; }
	}
	public GameObject Left
	{
		get { return neighbours[1]; }
	}
	public GameObject Top_Left
	{
		get { return neighbours[2]; }
	}

	public GameObject Bottom
	{
		get { return neighbours[3]; }
	}
	public GameObject Top
	{
		get { return neighbours[4]; }
	}

	public GameObject Bottom_Right
	{
		get { return neighbours[5]; }
	}
	public GameObject Right
	{
		get { return neighbours[6]; }
	}
	public GameObject Top_Right
	{
		get { return neighbours[7]; }
	}
#endregion

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < neighbours.Length; i++)
		{
			neighbours[i] = FindNeighbors(i);
		}
	}
	
	public void OccupyingUnit (GameObject occupierObject) 
	{
		occupied = true;
		occupier = occupierObject;
		occupier.name = occupier.tag+" ("+x+"-"+y+")";
		renderer.material.color = occupiedColour;
		CloudCommunication();
	}
	
	private GameObject FindNeighbors(int number)
	{
		int tempX = (int)x;
		int tempY = (int)y;

		try
		{
			if (number <= 2)
			{
				if(number == 0)
				{
					return Grid.gridNodes[tempX - 1, tempY - 1];
				}
				else if (number == 1)
				{
					return Grid.gridNodes[tempX - 1, tempY];
				}
				else
				{
					return Grid.gridNodes[tempX - 1, tempY + 1];
				}
			}
			else if(number >2 && number <= 4)
			{
				if (number == 3)
				{
					return Grid.gridNodes[tempX, tempY - 1];
				}
				else
				{
					return Grid.gridNodes[tempX, tempY + 1];
				}
			}
			else
			{
				if(number == 5)
				{
					return Grid.gridNodes[tempX + 1, tempY - 1];
				}
				else if (number == 6)
				{
					return Grid.gridNodes[tempX + 1, tempY];
				}
				else
				{
					return Grid.gridNodes[tempX + 1, tempY + 1];
				}
			}
		}
		catch
		{
			return null;
		}
		
	}

	void Selected(bool selected)
	{
		gameObject.renderer.enabled = selected;
	}

	void CursorHover(bool selected)
	{
		//GameObject.Find ("GUI").SendMessage("Selector", this.gameObject);
		gameObject.renderer.enabled = selected;

		/*
		foreach(GameObject neighbour in neighbours)
		{
			try
			{
				if(selected)
				{
					neighbour.renderer.enabled = true;
				}
				else
				{
					neighbour.renderer.enabled = false;
				}
			}
			catch
			{
				return;
			}
		}
		*/
	}

	void Deleted()
	{
		occupied = false;
		occupier = null;
		renderer.material.color = defaultColour;
		CloudCommunication();
	}

	void CloudCommunication()
	{
		object[] message = new object[3];
		message[0] = occupied;
		message[1] = (int)x;
		message[2] = (int)y;

		GameObject.Find (".Game Properties").GetComponent<Login>().photon.SendRequest(2,1,message, false);
	}

}
