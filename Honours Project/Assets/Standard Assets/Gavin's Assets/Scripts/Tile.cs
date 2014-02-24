using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	private float x;
	private float y;

	public GameObject[] neighbours = new GameObject[8];

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

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < neighbours.Length; i++)
		{
			neighbours[i] = FindNeighbors(i);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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

	void CursorHover(bool selected)
	{
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
}
