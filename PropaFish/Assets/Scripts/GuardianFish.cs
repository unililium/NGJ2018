using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianFish : MonoBehaviour {

	[Header("Movement speed"), Range(0f, 20f)]
	public float speed;
	[Header("Rotation speed"), Range(0f, 20f)]
	public float turnSpeed;
	float prevAxis;
	bool turningR;
	bool turningL;

	void Update () {
		Debug.Log(Input.GetAxis("Horizontal")*prevAxis);
		if(Input.GetAxis("Horizontal") * prevAxis < 0) {
			if(Input.GetAxis("Horizontal") < 0) turningL = true;
			if(Input.GetAxis("Horizontal") > 0) turningR = true;
		}
		transform.position += Input.GetAxis("Horizontal") * speed * transform.right * Time.fixedDeltaTime;
		prevAxis = Input.GetAxis("Horizontal");
	}

	void FixedUpdate () {
		if(turningR) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,180,0), turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
		} else if(turningL) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,0), turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}

	IEnumerator TurningWait() {
        yield return new WaitForSeconds(0.5f);
        turningL = false;
		turningR = false;
    }
}
