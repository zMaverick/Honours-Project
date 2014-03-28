using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private Vector3 moveVector;
	public float speed = 20;
	
	public KeyCode[] input_Up = new KeyCode[2];
	public KeyCode[] input_Down = new KeyCode[2];
	public KeyCode[] input_Left = new KeyCode[2];
	public KeyCode[] input_Right = new KeyCode[2];

	private GameObject scrollAngle;

	// Use this for initialization
	void Start () 
	{
		scrollAngle = new GameObject();
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveVector = Vector3.zero;

		MouseScroll();

		foreach(KeyCode key in input_Up)
		{
			if(Input.GetKey(key))
			{
				moveVector.z = 1;
			}
		}
		foreach(KeyCode key in input_Down)
		{
			if(Input.GetKey(key))
			{
				moveVector.z = -1;
			}
		}
		foreach(KeyCode key in input_Left)
		{
			if(Input.GetKey(key))
			{
				moveVector.x = -1;
			}
		}
		foreach(KeyCode key in input_Right)
		{
			if(Input.GetKey(key))
			{
				moveVector.x = 1;
			}
		}

		MouseMovement();

		if(moveVector != Vector3.zero)
		{
			moveVector.Normalize();
			gameObject.transform.Translate(moveVector * speed * Time.deltaTime);
		}

		ClampCamera();
	}

	void ClampCamera()
	{
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -500f, 500f), Mathf.Clamp(transform.position.y, 7f, 20f), Mathf.Clamp(transform.position.z, -500f, 500f));
	}

	void MouseScroll()
	{
		if(transform.position.y == 7 && Input.GetAxis("Mouse ScrollWheel") > 0 || transform.position.y == 20 && Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			return;
		}
		else
		{
			float scrollWheel = Input.GetAxis ("Mouse ScrollWheel") * -20;
			float angleX = Camera.main.transform.localEulerAngles.x;

			scrollAngle.transform.position = transform.position;
			scrollAngle.transform.eulerAngles = new Vector3(angleX, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
			scrollAngle.transform.Translate(Vector3.back * scrollWheel);

			this.transform.position = scrollAngle.transform.position;
		}
	}

	void MouseMovement()
	{
		Rect screenRect = new Rect(0,0, Screen.width, Screen.height);
		if (!screenRect.Contains(Input.mousePosition))
		{
			return;
		}

		float boundry = 10f;
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;

		if(mouseX >= Screen.width - boundry)
		{
			moveVector.x = 1;
		}
		if(mouseX <= boundry)
		{
			moveVector.x = -1;
		}
		if(mouseY >= Screen.height - boundry)
		{
			moveVector.z = 1;
		}
		if(mouseY <= boundry)
		{
			moveVector.z = -1;
		}
	}

}
