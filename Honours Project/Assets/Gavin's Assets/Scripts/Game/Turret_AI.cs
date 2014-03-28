using UnityEngine;
using System.Collections;

public class Turret_AI : MonoBehaviour {

	public enum TurretState
	{
		SPAWN,
		STOP,
		IDLE,
		ATTACK,
	}

	private Quaternion defaultHeadRotation = Quaternion.identity;
	private Quaternion defaultGunRotation = Quaternion.identity;
	private Quaternion headForward;
	private Quaternion gunForward;
	private bool turretReset;

	public TurretState currentState;

	public float scanSpeed;
	public float scanAngle;

	public GameObject head;
	public GameObject headMain;
	public GameObject body;
	public GameObject gun;
	public GameObject gunMain;
	public GameObject spawn;
	public GameObject bullet;

	public Material defaultMaterial;
	public Material selectMaterial;

	private GameObject enemyTarget;

	//public Transform target;
	public float maxAngle = 35.0f;
	private Quaternion baseRotation;
	private Quaternion targetRotation;

	private int fireCounter;
	private double lastFireTime;
	public double fireRate;
	private GameObject muzzleFlash;
	private bool turretActive = false;
	public GameObject tileObject;

	private float locationX;
	private float locationY;

	private int kills = 4;
	
	public int Kills
	{
		get{ return kills; }
	}

	public float X
	{
		get{ return locationX; }
	}

	public float Y
	{
		get{ return locationY; }
	}

	void Start() 
	{
		currentState = TurretState.SPAWN;
		turretReset = false;
		muzzleFlash = transform.FindChild("turret_Head/turret_Gun/turret_Gun_MuzzleFlash").gameObject;
		muzzleFlash.SetActive(false);
		baseRotation = head.transform.localRotation;
	}
	

	void Update() 
	{

		if(turretActive)
		{
			switch(currentState)
			{
				case TurretState.SPAWN:
				{
					break;
				}
				case TurretState.STOP:
				{
					ResetTurret();
					
					break;
				}
				case TurretState.IDLE:
				{
					Scan ();
					
					break;
				}
				case TurretState.ATTACK:
				{
					Attack ();
					break;
				}
			}
		}
	}


	void ResetTurret()
	{

		head.transform.localRotation = Quaternion.RotateTowards(head.transform.localRotation, defaultHeadRotation, 100.0f*Time.deltaTime);
		gun.transform.localRotation = Quaternion.RotateTowards(gun.transform.localRotation, defaultGunRotation, 100.0f*Time.deltaTime);

		if((head.transform.localRotation == defaultHeadRotation) && (gun.transform.localRotation == defaultGunRotation))
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
		//muzzleFlash.SetActive(false);
		Vector3 tempHeadRotation = head.transform.localEulerAngles;
		float angle = Mathf.LerpAngle(360.0f-scanAngle/2, scanAngle/2, Mathf.PingPong(Time.time / scanSpeed, 1.0f));
		tempHeadRotation.y = angle;
		head.transform.localEulerAngles = tempHeadRotation;
	}

	void Attack()
	{
		bool headLocked;
		bool gunLocked = false;
		Vector3 gunTarget;
		Vector3 headTarget;

		try
		{
			gunTarget = enemyTarget.transform.position - gun.transform.position;
			headTarget = enemyTarget.transform.position - head.transform.position;
			//gunTarget.x = 0;
			headTarget.y = 0;
		}
		catch
		{
			currentState = TurretState.STOP;
			return;
		}

		Quaternion gunLook = Quaternion.LookRotation (gunTarget);
		Quaternion headLook = Quaternion.LookRotation (headTarget);


		if(Quaternion.Angle(headLook, headForward) <= scanAngle/2)
		{
			headLocked = true;
			head.transform.rotation = headLook;

			if (Quaternion.Angle (gunLook, gunForward ) <= 45) 
			{
				gunLocked = true;
				gun.transform.rotation = gunLook;
			} 
			else 
			{
				gunLocked = false;
			}

			Fire();
		}
		else
		{
			headLocked = false;

			currentState = TurretState.STOP;

			if(!headLocked && !gunLocked)
			{
				currentState = TurretState.STOP;
			}

		}
	}
	
	void Target(GameObject target)
	{
		enemyTarget = target;

		Vector3 headTarget = enemyTarget.transform.position - head.transform.position;
		Quaternion headLook = Quaternion.LookRotation (headTarget);
				
		if(Quaternion.Angle(headLook, headForward) <= scanAngle/2)
		{
			currentState = TurretState.ATTACK;
		}
	}

	void Fire()
	{
		double currentTime = Time.time * 100;
		RaycastHit target;

		Debug.DrawLine(gunMain.transform.position, enemyTarget.transform.position, Color.red);
		Debug.DrawRay(spawn.transform.position, spawn.transform.rotation * Vector3.forward, Color.green);

		if(Physics.Raycast (spawn.transform.position, spawn.transform.rotation * Vector3.forward, out target, 100.0f))
	 	{
			if(target.collider.tag == "Enemy")
			{
				if(currentTime - lastFireTime >= fireRate)
				{
					muzzleFlash.SetActive(true);
					Instantiate(bullet, spawn.transform.position, gun.transform.rotation);
					lastFireTime = currentTime;
				}
			}
		}

		if(currentTime - lastFireTime >= 5)
		{
			muzzleFlash.SetActive(false);
		}
	}

	void Spawned(bool isSpawned)
	{
		if(isSpawned)
		{
			Selected(isSpawned);
			currentState = TurretState.SPAWN;
			turretActive = false;
		}
		else
		{
			Selected(isSpawned);
			currentState = TurretState.STOP;
			headForward = head.transform.rotation;
			gunForward = gun.transform.rotation;
			turretActive = true;
		}


	}
	void TileOver(GameObject tileOn)
	{
		tileObject = tileOn;
		locationX = tileOn.GetComponent<Tile>().X;
		locationY = tileOn.GetComponent<Tile>().Y;
	}

	void Selected(bool isSelected)
	{
		if(isSelected)
		{
			headMain.renderer.material = selectMaterial;
			body.renderer.material = selectMaterial;
			gunMain.renderer.material = selectMaterial;
		}
		else
		{
			headMain.renderer.material = defaultMaterial;
			body.renderer.material = defaultMaterial;
			gunMain.renderer.material = defaultMaterial;
		}
	}

	void CursorHover(bool mouseHover)
	{
		if(turretActive)
		{
			GameObject.Find ("GUI").SendMessage("Select", this.gameObject);
		}
	}

	public void Destroy(bool ifSpawned)
	{
		if(ifSpawned)
		{
			tileObject.BroadcastMessage("Deleted");
		}
	}
}
