using UnityEngine;
using System.Collections;

public class Turret_AI : MonoBehaviour {

	public enum TurretState
	{
		STOP,
		IDLE,
		ATTACK,
	}

	private Quaternion defaultHeadRotation = Quaternion.identity;
	private Quaternion defaultGunRotation = Quaternion.identity;

	private bool turretReset;

	public TurretState currentState;

	public float scanSpeed;
	public float scanAngle;

	public GameObject head;
	public GameObject gun;
	public GameObject spawn;
	public GameObject bullet;

	private GameObject enemyTarget;

	//public Transform target;
	public float maxAngle = 35.0f;
	private Quaternion baseRotation;
	private Quaternion targetRotation;

	private int fireCounter;
	private double lastFireTime;
	public double fireRate;
	public GameObject muzzleFlash;

	void Start () 
	{
		currentState = TurretState.STOP;
		turretReset = false;
		muzzleFlash = transform.FindChild("turret_Head/turret_Gun/turret_Gun_MuzzleFlash").gameObject;
		baseRotation = head.transform.localRotation;
	}
	

	void Update () 
	{
		switch(currentState)
		{
			case TurretState.STOP:
			{
				ResetTurret();
				
				break;
			}
			case TurretState.IDLE:
			{
				Scan ();
				
				if(Input.GetKeyDown(KeyCode.Return))
				{
					currentState = TurretState.ATTACK;
				}

				break;
			}
			case TurretState.ATTACK:
			{
				Attack ();

				if(Input.GetKeyDown(KeyCode.Return))
				{
					currentState = TurretState.STOP;
				}

				break;
			}
		}
	}


	void ResetTurret()
	{

		head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, defaultHeadRotation, 100.0f*Time.deltaTime);
		gun.transform.localRotation = Quaternion.RotateTowards(gun.transform.localRotation, defaultGunRotation, 100.0f*Time.deltaTime);

		if((head.transform.rotation == defaultHeadRotation) && (gun.transform.rotation == defaultGunRotation))
		{
			currentState = TurretState.IDLE;
		}
		else
		{
			currentState = TurretState.STOP;
		}

		muzzleFlash.SetActive(false);
	}

	void Scan()
	{
		Vector3 tempHeadRotation = head.transform.localEulerAngles;
		float angle = Mathf.LerpAngle(360.0f-scanAngle/2, scanAngle/2, Mathf.PingPong(Time.time / scanSpeed, 1.0f));
		tempHeadRotation.y = angle;
		head.transform.localEulerAngles = tempHeadRotation;
	}

	void Attack()
	{
		bool headLocked;
		bool gunLocked;

		Vector3 gunTarget = enemyTarget.transform.position - gun.transform.position;
		Vector3 headTarget = enemyTarget.transform.position - head.transform.position;
		gunTarget.x = 0;
		headTarget.y = 0;

		Quaternion gunLook = Quaternion.LookRotation (gunTarget);
		Quaternion headLook = Quaternion.LookRotation (headTarget);


		if (Quaternion.Angle (gunLook, Quaternion.identity) <= 30) 
		{
			gunLocked = true;
			gun.transform.localRotation = gunLook;
		} 
		else 
		{
			gunLocked = false;
		}

		if(Quaternion.Angle (headLook, Quaternion.identity) <= scanAngle/2) 
		{
			headLocked = true;
			head.transform.localRotation = headLook;
		}
		else
		{
			headLocked = false;
		}

		if(!headLocked && !gunLocked)
		{
			currentState = TurretState.STOP;
		}

		Fire();
	}



	void Target(GameObject target)
	{
		currentState = TurretState.ATTACK;
		enemyTarget = target;

	}

	void Fire()
	{
		double currentTime = Time.time * 100;

		if(currentTime - lastFireTime >= fireRate)
		{
			muzzleFlash.SetActive(true);
			Instantiate(bullet, spawn.transform.position, gun.transform.rotation);
			lastFireTime = currentTime;
		}
		if(currentTime - lastFireTime >= 5)
		{
			muzzleFlash.SetActive(false);
		}
	}
}
