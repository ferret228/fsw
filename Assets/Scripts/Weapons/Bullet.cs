using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 1f;

	public enum bulletType{
		spaceship,
		saucer,
	}

	public bulletType type;

	private Vector3 screenSW;
	private Vector3 screenNE;
	private float destroyPadding = 1f;

	// Use this for initialization
	void Start () {
		screenSW = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.localPosition.y));
		screenNE = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.localPosition.y));
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.AddForce(transform.up * speed);

		if(transform.localPosition.x < screenSW.x - destroyPadding ||
		   transform.localPosition.x > screenNE.x + destroyPadding ||
		   transform.localPosition.y < screenSW.y - destroyPadding ||
		   transform.localPosition.y > screenNE.y + destroyPadding){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		switch(type){
		case bulletType.spaceship:
			if(other.gameObject.tag == "Rock" || other.gameObject.tag == "Saucer"){
				Destroy(gameObject);
			}
			break;
		case bulletType.saucer:
			if(other.gameObject.tag == "Player"){
				Destroy(gameObject);
			}
			break;
		}
	}
}
