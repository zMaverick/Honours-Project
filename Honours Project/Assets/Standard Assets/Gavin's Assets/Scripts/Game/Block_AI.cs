using UnityEngine;
using System.Collections;

public class Block_AI : MonoBehaviour {

	public GameObject topMain;
	public GameObject baseMain;
	public GameObject antennaMain;

	public Material selectMaterial;
	public Material defaultMaterial;

	private bool blockActive = false;
	public GameObject tileObject;

	private float locationX;
	private float locationY;
	
	public float X
	{
		get{ return locationX; }
	}
	
	public float Y
	{
		get{ return locationY; }
	}

	void Spawned(bool isSpawned)
	{
		if(isSpawned)
		{
			Selected(isSpawned);
			blockActive = false;
		}
		else
		{
			Selected(isSpawned);
			blockActive = true;
		}
	}


	void Selected(bool isSelected)
	{
		if(isSelected)
		{
			topMain.renderer.material = selectMaterial;
			baseMain.renderer.material = selectMaterial;
			antennaMain.renderer.material = selectMaterial;
		}
		else
		{
			topMain.renderer.material = defaultMaterial;
			baseMain.renderer.material = defaultMaterial;
			antennaMain.renderer.material = defaultMaterial;
		}
	}
	
	void CursorHover(bool mouseHover)
	{
		if(blockActive)
		{
			GameObject.Find ("GUI").SendMessage("Select", this.gameObject);
		}
	}

	void TileOver(GameObject tileOn)
	{
		tileObject = tileOn;
		locationX = tileOn.GetComponent<Tile>().X;
		locationY = tileOn.GetComponent<Tile>().Y;
	}

	public void Destroy(bool ifSpawned)
	{
		if(ifSpawned)
		{
			Debug.Log(tileObject.name);
			tileObject.BroadcastMessage("Deleted");
		}
	}
}
