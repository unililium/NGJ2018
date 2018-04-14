using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour {
	
	[Range(0f, 5f)]
	public float speed;
	private Vector3 currentAngle;
	private Vector3 targetAngle = new Vector3(90f, 0f, 0f);

	private bool moving;
	void Start () {
		currentAngle = transform.eulerAngles;
		moving = true;	
	}
	
	void Update () {
		Move();	
	}

	void Turn() {
		Vector3 currentAngle = transform.eulerAngles;

        transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));
		moving = true;
    }

	void Move() {
		if(moving)
        	transform.position = transform.position + speed * transform.right * Time.fixedDeltaTime;
	}

	void OnCollisionEnter(Collision collision)
    {
		Debug.Log("collision");
		moving = false;
        Turn();
	}
}
