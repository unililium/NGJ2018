using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebellionBehaviour : MonoBehaviour {

	[Header("Movement speed"), Range(0f, 20f)]
	public float speed;
	[Header("Rotation speed"), Range(0f, 20f)]
	public float turnSpeed;
	[Header("Colldown"), Range(5f, 40f)]
	public float cooldown;
	private float timeStamp;
	private bool rebellionStarted = false;
	private bool turning = false;
	private Vector3 savedPos;
	private Quaternion savedRot;

	void Update() {
		if(Input.GetButtonDown("Fire1") && timeStamp <= Time.time) {
			timeStamp = Time.time + cooldown;
			rebellionStarted = true;
			StartCoroutine(RandomWaitForTurning());
			savedPos = transform.position;
			savedRot = transform.rotation;

			gameObject.GetComponent<FishBehaviour>().enabled = false;
		}
		if(rebellionStarted) {
			
			if(turning) {	
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0,90), turnSpeed * Time.deltaTime);
				StartCoroutine(TurningWait());
			}
			else Move();
			
		}
	}

	void Move() {
		transform.position += speed * transform.right * Time.fixedDeltaTime;

		// Random vertical mvmt
		float random = Random.Range(-5f, 5f);
		//if(random > -1f && random < 1f) transform.position += speed * random * transform.up * Time.fixedDeltaTime;
 	    //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}

	IEnumerator TurningWait() {
        yield return new WaitForSeconds(0.5f);
        turning = false;
    }

	IEnumerator RandomWaitForTurning() {
        yield return new WaitForSeconds(Random.Range(0f, 0.4f));
		turning = true;
    }

}
