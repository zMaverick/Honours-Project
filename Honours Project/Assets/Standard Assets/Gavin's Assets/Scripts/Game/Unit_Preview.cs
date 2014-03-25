using UnityEngine;
using System.Collections;

public class Unit_Preview : MonoBehaviour 
{
	enum Preview
	{
		TURRET,
		BLOCK,
		ENEMY,
		NONE
	}

	private Preview prev = Preview.NONE;

	public GameObject textTop;
	public GameObject textBot;
	public GameObject textTopMid;
	public GameObject textBotMid;

	public GameObject header;
	public Selector selector;

	public GameObject preTurret;
	public GameObject preBlock;
	public GameObject preEnemy;
	private GameObject unit;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		preTurret.SetActive(false);
		preBlock.SetActive(false);
		preEnemy.SetActive(false);

		if(selector.Unit_Selected)
		{
			unit = selector.UnitSelected;

			if(unit.tag == "Turret")
			{
				prev = Preview.TURRET;
			}
			else if(unit.tag == "Enemy")
			{
				prev = Preview.ENEMY;
			}
			else
			{
				prev = Preview.BLOCK;
			}
		}
		else
		{
			prev = Preview.NONE;
		}

		switch(prev)
		{
			case Preview.TURRET:
			{
				Turret_AI ai = unit.GetComponent<Turret_AI>();

				header.guiText.text = unit.name;
				textTop.guiText.text = "Type: Turret";
				textTopMid.guiText.text = "Location: "+ai.X+", "+ai.Y;
				textBotMid.guiText.text = "Kills: "+ai.Kills;
				textBot.guiText.text = "State: "+unit.GetComponent<Turret_AI>().currentState;

				preTurret.SetActive(true);
				break;
			}
			case Preview.ENEMY:
			{
				Enemy_AI ai = unit.GetComponent<Enemy_AI>();

				header.guiText.text = unit.name;
				textTop.guiText.text = "Type: Enemy";
				textTopMid.guiText.text = "Location: Unknown";
				textBotMid.guiText.text = "Health: "+ai.Health;
				textBot.guiText.text = "Threat: Minimal";

				preEnemy.SetActive(true);
				break;
			}
			case Preview.BLOCK:
			{
				Block_AI ai = unit.GetComponent<Block_AI>();

				header.guiText.text = unit.name;
				textTop.guiText.text = "Type: Block";
				textTopMid.guiText.text = "Location: "+ai.X+", "+ai.Y;
				textBotMid.guiText.text = "Kills: N/A";
				textBot.guiText.text = "State: N/A";

				preBlock.SetActive(true);
				break;
			}
			case Preview.NONE:
			{
				header.guiText.text = "Selected Unit";
				textTop.guiText.text = "Type: -";
				textTopMid.guiText.text = "Position: -";
				textBotMid.guiText.text = "Kills: -";
				textBot.guiText.text = "State: -";
				break;
			}
		}
	}
}
