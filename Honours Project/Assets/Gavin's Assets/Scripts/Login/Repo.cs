using UnityEngine;
using System.Collections;

public class Repo : MonoBehaviour 
{
	public string repoUrl;

	void OnMouseDown()
	{
		Application.OpenURL(repoUrl);
	}
}
