using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public int grid_Length;
	public int grid_Width;

	public float spacing;

	public List<Vector3> gridList = new List<Vector3>();
	//public Vector3[,] gridLines = new Vector3[10, 10];
	public Vector3[,] gridLines;

	Mesh mesh;

	// Use this for initialization
	void Start () 
	{
		gridLines = new Vector3[grid_Length, grid_Width];	//2D array of groups of Lines

		for(int x = 0; x < grid_Width; x++)
		{
			for(int z = 0; z < grid_Length; z++)
			{
				gridList.Add(new Vector3(x * spacing, 0, z * spacing));
			}
		}

		for(int x = 0; x < gridLines.GetLength(0); x++)
		{
			for(int z = 0; z < gridLines.GetLength(1); z++)
			{
				gridLines[x,z] = new Vector3(x * spacing, 0, z * spacing);
				Debug.Log (gridLines[x,z]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int x = 0; x < gridLines.GetLength(0); x++)
		{
			for(int z = 0; z < gridLines.GetLength(1); z++)
			{
				if(x == gridLines.GetLength(0) - 1)
				{
					return;
				}
				
				Debug.DrawLine(gridLines[x, z], gridLines[x+1, z], Color.green);
				Debug.DrawLine(gridLines[z, x], gridLines[z, x+1], Color.green);
			}
		}

	}
}
