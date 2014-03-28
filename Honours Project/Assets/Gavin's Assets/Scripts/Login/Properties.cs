using UnityEngine;
using System.Collections;

public class Properties : MonoBehaviour 
{
	public static bool CloudAssisted
	{
		get;
		set;
	}
	public static bool AdminMode
	{
		get;
		set;
	}

	void Awake()
	{
		AdminMode = false;
	}
}
