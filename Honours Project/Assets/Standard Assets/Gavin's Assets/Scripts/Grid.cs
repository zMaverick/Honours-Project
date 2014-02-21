using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public int grid_Length;
	public int grid_Width;

	public Vector3 grid_Position;

	public float spacing;
	public GameObject[,] gridNodes;

	public GameObject gNode;

	// Use this for initialization
	void Start () 
	{
		gridNodes = new GameObject[grid_Length, grid_Width];	//2D array of groups of Nodes

		for(int x = 0; x < gridNodes.GetLength(0); x++)
		{
			for(int z = 0; z < gridNodes.GetLength(1); z++)
			{
				GameObject node = (GameObject)Instantiate(gNode);

				node.transform.parent = this.transform;
				node.transform.localPosition = new Vector3(x * spacing, 0f, z * spacing);
				node.name = "Node "+x+"-"+z;

				gridNodes[x,z] = node;
			}
		}

		float gridLength = grid_Length * spacing;
		float gridWidth = grid_Width * spacing;

		transform.position = new Vector3(grid_Position.x +0.5f - gridLength/2, grid_Position.y, grid_Position.z +0.5f - grid_Width/2);

	}
	
	// Update is called once per frame
	void Update () 
	{
		float gridLength = grid_Length * spacing;
		float gridWidth = grid_Width * spacing;
		
		transform.position = new Vector3(grid_Position.x +0.5f - gridLength/2, grid_Position.y, grid_Position.z +0.5f - grid_Width/2);
	}
}
