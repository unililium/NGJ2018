using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour {
	
	private Rigidbody rb;
	[Range(0f, 5f)]
	public float speed;
	private Vector3 currentAngle;
	private Vector3 targetAngle = new Vector3(0f, 180f, 0f);

	private bool turning = false;
	void Start () {
		rb = GetComponent<Rigidbody>();
		currentAngle = transform.eulerAngles;
		Vector3 movement = new Vector3 (1f, 0.0f, 0f);
	}
	
	void FixedUpdate () {
	}

    void Turn() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime);
	}

	void Move() {
		Vector3 movement = new Vector3 (1f, 0.0f, 0f);
		rb.AddForce (movement * speed); 
	}

	void OnCOllisionEnter(Collision collision) {
		ConstantForce constForce = GetComponent<ConstantForce>();
		constForce.relativeForce = new Vector3(0f, 0f, 0f);
		Turn();
	}
}
