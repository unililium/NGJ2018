using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour {
	
	[Header("Movement speed"), Range(0f, 20f)]
	public float speed;
	[Header("Rotation speed"), Range(0f, 20f)]
	public float turnSpeed;
	[Header("Perlin Noise Multiplicator"), Range(1f, 5f)]
	public float perlinNoiseMultiplicator;
	public bool turningR = false;
	public bool turningL = false;
	float perlinX;
	float perlinY;

	void Start() {
		perlinX = Random.Range(0f, 100f);
		perlinY = Random.Range(0f, 100f); 
	}
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
		float random = perlinNoiseMultiplicator * (Mathf.PerlinNoise(perlinX, perlinY) - 0.47f);
		perlinX += 0.1f; 
		//if(random > -0.2f && random < 0.2f)
		transform.position += speed * random * transform.up * Time.fixedDeltaTime;
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
