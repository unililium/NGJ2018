using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour {
	
	private Rigidbody rb;
	[Header("Movement speed"), Range(0f, 20f)]
	public float speed;
	[Header("Rotation speed"), Range(0f, 20f)]
	public float turnSpeed;	
	public bool turningR = false;
	public bool turningL = false;

	
	void FixedUpdate () {
		if(turningR) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,180,0), turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
		} else if(turningL) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,0), turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else {
            if(transform.rotation.eulerAngles.y < 10) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            } else {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
