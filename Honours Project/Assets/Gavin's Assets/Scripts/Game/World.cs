using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {



	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerExit(Collider col)
	{
		Destroy(col.transform.root.gameObject);
	}
}
