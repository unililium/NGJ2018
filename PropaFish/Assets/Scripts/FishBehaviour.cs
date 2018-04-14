using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour {
	
	private Rigidbody rb;
	[Range(0f, 5f)]
	public float speed;
	private Vector3 currentAngle;
	private Vector3 targetAngle = new Vector3(0f, 180f, 0f);
	private ConstantForce constForce;
	public bool turningR = false;
	public bool turningL = false;
	private Quaternion previousRotation; 

	void Start () {
		previousRotation = transform.rotation;
	}
	
	void FixedUpdate () {
		if(turningR) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,180,0), 10 * Time.deltaTime);
		} else if(turningL) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,0), 10 * Time.deltaTime);
		} else {
			Move();
		}
	}

    IEnumerator TurningWait() {
        yield return new WaitForSeconds(0.5f);
        turningL = false;
		turningR = false;
    }

	void Move() {
		transform.position += speed * transform.right * Time.fixedDeltaTime;

		// Random vertical mvmt
		float random = Random.Range(-5f, 5f);
		if(random > -1f && random < 1f) transform.position += speed * random * transform.up * Time.fixedDeltaTime;
	}

	Quaternion GetOppositeRotation(Quaternion rotation) {
		Quaternion result = new Quaternion(0f, 0f, 0f, 1.0f);

		if(rotation == new Quaternion(0f, 0f, 0f, 1.0f)) result = new Quaternion(0f, 1.0f, 0f, 0f);
		if(rotation == new Quaternion(0f, 1.0f, 0f, 0f)) result = new Quaternion(0f, 0f, 0f, 1.0f);
		return result; 
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.tag == "WallR") {
			turningR = true;
		} else if(collider.tag == "WallL") {
			turningL = true;
		}
		StartCoroutine(TurningWait());
	}
}
