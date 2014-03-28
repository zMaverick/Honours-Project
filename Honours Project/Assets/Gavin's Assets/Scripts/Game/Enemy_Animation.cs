using UnityEngine;
using System.Collections;

public class Enemy_Animation : MonoBehaviour {

	public AnimationClip activateAnim;
	public AnimationClip forwardAnim;
	public AnimationClip backAnim;
	public AnimationClip leftAnim;
	public AnimationClip rightAnim;
	// Use this for initialization
	void OnEnable () 
	{	
		/*
		animation[activateAnim.name].enabled = true;
		animation[activateAnim.name].weight = 1;
		animation[activateAnim.name].time = 0;
		animation[activateAnim.name].speed = 1;
		
		animation[forwardAnim.name].layer = 1;
		animation[forwardAnim.name].enabled = true;
		animation[forwardAnim.name].weight = 0;
		animation[backAnim.name].layer = 1;
		animation[backAnim.name].enabled = true;
		animation[backAnim.name].weight = 0;
		animation[leftAnim.name].layer = 1;
		animation[leftAnim.name].enabled = true;
		animation[leftAnim.name].weight = 0;
		animation[rightAnim.name].layer = 1;
		animation[rightAnim.name].enabled = true;
		animation[rightAnim.name].weight = 0;
*/
		animation[activateAnim.name].wrapMode = WrapMode.Once;
		animation[activateAnim.name].enabled = true;
		animation[activateAnim.name].weight = 1;
		animation[activateAnim.name].time = 0;
		animation[activateAnim.name].speed = 1;
	}

	void OnDisable()
	{
		/*
		animation[activateAnim.name].enabled = true;
		animation[activateAnim.name].weight = 1;
		animation[activateAnim.name].normalizedTime = 1;
		animation[activateAnim.name].speed = -1;
		animation.CrossFade(activateAnim.name, 0.3f, PlayMode.StopAll);
		*/
	}

	// Update is called once per frame

	void Update()
	{
		if(!animation.isPlaying)
		{
			animation.CrossFade(forwardAnim.name);
		}

	}
	void OldUpdate () 
	{
		Vector3 direction = -transform.parent.right;
		direction.y = 0;
		
		float walkWeight = direction.magnitude;
		
		animation[forwardAnim.name].speed = walkWeight;
		animation[rightAnim.name].speed = walkWeight;
		animation[backAnim.name].speed = walkWeight;
		animation[leftAnim.name].speed = walkWeight;
		
		float angle = Mathf.DeltaAngle (HorizontalAngle(transform.forward), HorizontalAngle (direction));
		
		if (walkWeight > 0.01) 
		{
			float w;

			if (angle < -90) 
			{
				w = Mathf.InverseLerp (-180f, -90f, angle);
				animation[forwardAnim.name].weight = 0;
				animation[rightAnim.name].weight = 0;
				animation[backAnim.name].weight = 1 - w;
				animation[leftAnim.name].weight = 1;
			}
			else if (angle < 0) 
			{
				w = Mathf.InverseLerp (-90f, 0f, angle);
				animation[forwardAnim.name].weight = w;
				animation[rightAnim.name].weight = 0;
				animation[backAnim.name].weight = 0;
				animation[leftAnim.name].weight = 1 - w;
			}
			else if (angle < 90) 
			{
				w = Mathf.InverseLerp (0f, 90f, angle);
				animation[forwardAnim.name].weight = 1 - w;
				animation[rightAnim.name].weight = w;
				animation[backAnim.name].weight = 0;
				animation[leftAnim.name].weight = 0;
			}
			else 
			{
				w = Mathf.InverseLerp (90f, 180f, angle);
				animation[forwardAnim.name].weight = 0;
				animation[rightAnim.name].weight = 1 - w;
				animation[backAnim.name].weight = w;
				animation[leftAnim.name].weight = 0;
			}
		}
	}

	float HorizontalAngle(Vector3 direction) 
	{
		return Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
	}

}
