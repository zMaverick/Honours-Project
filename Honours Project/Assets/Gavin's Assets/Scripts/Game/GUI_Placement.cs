using UnityEngine;
using System.Collections;

public class GUI_Placement : MonoBehaviour {

	private float centreX;
	private float centreY;
	private float leftPos;
	private float rightPos;
	private float topPos;
	private float bottomPos;

	private float height;
	private float width;
	private float innerHeight;
	private float innerWidth;

	public bool withBorder = true;

	private float border;

	public float CenterX
	{
		get{ return centreX; }
	}
	public float CenterY
	{
		get{ return centreY; }
	}

	public float Border
	{
		get{ return border; }
	}

	public float LeftPosition
	{
		get{ return leftPos; }
	}

	public float RightPosition
	{
		get{ return rightPos; }
	}

	public float TopPosition
	{
		get{ return topPos; }
	}

	public float BottomPosition
	{
		get{ return bottomPos; }
	}

	public float InnerHeight
	{
		get{ return innerHeight; }
	}	

	public float InnerWidth
	{
		get{ return innerWidth; }
	}
	
	public float Height
	{
		get{ return height; }
	}
	public float Width
	{
		get{ return width; }
	}


	void Start () 
	{
		float screenHeight = Screen.height;
		float screenWidth = Screen.width;

		width = transform.lossyScale.x * screenWidth;
		height = transform.lossyScale.y * screenHeight;

		if(withBorder)
		{
			border = width * 0.05f;
		}
		else
		{
			border = 0f;
		}

		centreX = (transform.position.x * screenWidth);
		centreY = (screenHeight - (transform.position.y * screenHeight));

		leftPos = ((centreX - (width/2)) + border);
		rightPos = ((centreX + (width/2)) - border);

		bottomPos = ((centreY + (height/2)) - border);
		topPos = ((centreY - (height/2)) + border);

		innerWidth = width - (border*2);
		innerHeight = height - (border*2);;


	}

}
