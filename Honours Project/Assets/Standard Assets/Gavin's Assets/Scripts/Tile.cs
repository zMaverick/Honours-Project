using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CursorHover(bool selected)
	{
		gameObject.renderer.enabled = selected;
	}
}
