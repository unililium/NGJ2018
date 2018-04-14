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
	public bool turning = false;
	private Quaternion previousRotation; 

	void Start () {
		previousRotation = transform.rotation;
	}
	
	void FixedUpdate () {
		if(turning) {
			Turn();
			if(transform.rotation.x == -previousRotation.x) turning = false; 
		} else {
			Move();
		}
	}

    void Turn() {
		//transform.Rotate(Vector3.right * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), 2 * Time.deltaTime);
	}

	void Move() {
		transform.position += speed * transform.right * Time.fixedDeltaTime;
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("trigger");
		previousRotation = transform.rotation;
		turning = true;
	}
}
