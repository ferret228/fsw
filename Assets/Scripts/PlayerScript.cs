using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour {

	public Guid guid;
	public Vector2 speed = new Vector2(50, 50);
	private Vector3 target1; 
	private float degree;
	private float localDegree;
	public Thruster thruster;
	public ParticleSystem strafeLeft;
	public ParticleSystem strafeRight;
	public GameObject explosion;
	public bool isAlive = true;

	private bool leftPress = false;
	private bool rightPress = false;
	private NewWeaponScript weapon;

	public void CustomDestroy()
	{
		isAlive = false;
		weapon = null;
		strafeLeft = null;
		strafeRight = null;
		thruster = null;

		Destroy(Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation), 3.0f);
		Destroy(gameObject);
	}

	void Start()
	{
		guid = Guid.NewGuid();
		MiniMapObjects.Instance.Add(new MiniMapObjects.MiniMapObject(transform, guid, MiniMapObjects.MinimapObjectType.ship, Globals.Origin.player, "player"));

		//Controls.KeyNum += HandleKeyNum;
		//Controls.KeyX += HandleKeyX;
		//Controls.KeyZ += HandleKeyZ;
		//Controls.AxisH += HandleAxisH;
		//Controls.Shoot += HandleShoot;

		weapon = GetComponent<NewWeaponScript>();
	}

	void HandleKeyNum (int obj)
	{
		if(isAlive) {
			//weapon.RocketShot(obj);
		}
	}

	void HandleKeyZ (bool obj)
	{
		if(isAlive) {
			strafeLeft.maxParticles = 20;
			leftPress = true;
			localDegree--;
			degree--;;
		}
	}
	
	void HandleKeyX (bool obj)
	{
		if(isAlive) {
			strafeRight.maxParticles = 20;
			rightPress = true;
			localDegree++;
			degree++;
		}
	}

	void HandleShoot (bool obj)
	{
		if(isAlive) {
			if (weapon != null)
			{
				weapon.Attack();
			}
		}
	}
	
	void HandleAxisH (float obj)
	{
		if(isAlive) {
			if(obj>0){
				degree--;
			}
		
			if(obj<0){
				degree++;
			}
			Globals.degree = new Vector3(0,0,degree);
		}
	}

	void Update () {
		if(isAlive) {
			if(!leftPress) {
				strafeLeft.maxParticles = 0;
			}

			if(!rightPress) {
				strafeRight.maxParticles = 0;
			}

			if(thruster!=null){
				thruster.parentSpeed = Globals.playerSpeed;
			}

			leftPress = false;
			rightPress = false;
		}
	}
	
	void FixedUpdate() {
		if(isAlive) {
			gameObject.transform.Translate(Vector2.up * (Globals.playerSpeed * Time.deltaTime));
			gameObject.transform.eulerAngles = new Vector3(0,0,localDegree);
		}
	}
}
