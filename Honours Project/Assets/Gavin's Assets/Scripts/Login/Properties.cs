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

	public static string ServerAddress
	{
		get;
		set;
	}

	public static string Status
	{
		get;
		set;
	}
	public static int ServerPing
	{
		get;
		set;
	}

	public static bool ComplexStart
	{
		get;
		set;
	}

	public static bool ComplexGoal
	{
		get;
		set;
	}


	public static Vector2[] StartPoints
	{
		get
		{
			if(ComplexStart)
			{
				return complexStarts;
			}
			else
			{
				return simpleStart;
			}
		}
	}

	public static Vector2[] GoalPoints
	{
		get
		{
			if(ComplexGoal)
			{
				return complexGoals;
			}
			else
			{
				return simpleGoal;
			}
		}
	}

	public static int EnemiesSpawned
	{
		get;
		set;
	}

	public static int EnemiesPassed
	{
		get;
		set;
	}

	public static int EnemiesKilled
	{
		get;
		set;
	}

	public static int SpawnTime
	{
		get;
		set;
	}

	private static Vector2[] complexGoals = new Vector2[20];
	private static Vector2[] complexStarts = new Vector2[20];

	private static Vector2[] simpleGoal = new Vector2[1];
	private static Vector2[] simpleStart = new Vector2[1];

	private int simpleGoalIndex;
	private int simpleStartIndex;

	void Awake()
	{
		SpawnTime = 10;
		ComplexGoal = true;
		ComplexStart = true;

		AdminMode = false;
		complexGoals = new Vector2[20]{ new Vector2(0,0), new Vector2(1,0), new Vector2(2,0), new Vector2(3,0), new Vector2(4,0), new Vector2(5,0), new Vector2(6,0), new Vector2(7,0), new Vector2(8,0), new Vector2(9,0), new Vector2(10,0), new Vector2(11,0), new Vector2(12,0), new Vector2(13,0), new Vector2(14,0), new Vector2(15,0), new Vector2(16,0), new Vector2(17,0), new Vector2(18,0), new Vector2(19,0) };
		complexStarts = new Vector2[20]{ new Vector2(0,19), new Vector2(1,19), new Vector2(2,19), new Vector2(3,19), new Vector2(4,19), new Vector2(5,19), new Vector2(6,19), new Vector2(7,19), new Vector2(8,19), new Vector2(9,19), new Vector2(10,19), new Vector2(11,19), new Vector2(12,19), new Vector2(13,19), new Vector2(14,19), new Vector2(15,19), new Vector2(16,19), new Vector2(17,19), new Vector2(18,19), new Vector2(19,19) }; 

		simpleGoalIndex = Random.Range(0, 19);
		simpleStartIndex = Random.Range(0, 19);

		simpleGoal[0] = complexGoals[simpleGoalIndex];
		simpleStart[0] = complexStarts[simpleStartIndex];
	}
}
