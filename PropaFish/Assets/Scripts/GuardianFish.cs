using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianFish : MonoBehaviour {

	[Header("Movement speed"), Range(0f, 20f)]
	public float speed;
	[Header("Rotation speed"), Range(0f, 20f)]
	public float turnSpeed;
	int facingR = 1;
	bool turningR;
	bool turningL;

	void Update () {
		if(Input.GetAxis("Horizontal") < 0 && facingR == 1) {
			turningR = true;
			facingR = -1;
			StartCoroutine(TurningWait());
		} else if(Input.GetAxis("Horizontal") > 0 && facingR == -1) { 
			turningL = true;
			facingR = 1;
			StartCoroutine(TurningWait());
		}
		if(turningR) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,180,0), turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
		} else if(turningL) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,0), turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
		}

		transform.position += Input.GetAxis("Horizontal") * speed * Vector3.right * Time.fixedDeltaTime;
	}

	IEnumerator TurningWait() {
        yield return new WaitForSeconds(0.5f);
        turningL = false;
		turningR = false;
    }

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Fish") {
			other.gameObject.GetComponent<RebellionBehaviour>().DiveDown();
			Debug.Log("collision");
		}
	}
}
