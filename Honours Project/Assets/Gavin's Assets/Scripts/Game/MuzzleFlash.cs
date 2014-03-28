using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {

	// Use this for initialization
	void OnEnable () 
	{
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		float randZ = Random.Range(0f, 90.0f);
		float randScale = Random.Range(0.8f,1.5f);
		Vector3 randRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, randZ);
		
		transform.localScale = new Vector3(randScale, randScale, randScale * 2);
		transform.localRotation = Quaternion.Euler(randRotation);

	}
}
