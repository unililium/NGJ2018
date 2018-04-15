using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianFish : MonoBehaviour {

	[Header("Movement speed"), Range(0f, 20f)]
	public float speed;
	[Header("Rotation duration"), Range(0f, 20f)]
	public float rotationDuration;
    private float turnStart;
    bool facingR;
    bool turningR;
	bool turningL;    

    private void Start()
    {
        GetComponent<Prone>().enabled = false;
        GetComponent<RebellionBehaviour>().enabled = false;
        GetComponent<FishBehaviour>().enabled = false;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<FloatWhileFalling>().rotationRange /= 10;
        turningL = false;
        facingR = turningR = true;
        turnStart = Time.fixedTime;
    }

    void FixedUpdate()
    {
        if (!turningL && !turningR)
        {
            if (Input.GetAxis("Horizontal") > 0 && !facingR)
            {
                turningR = true;
                facingR = true;
                turnStart = Time.fixedTime;
            }
            else if (Input.GetAxis("Horizontal") < 0 && facingR)
            {
                turningL = true;
                facingR = false;
                turnStart = Time.fixedTime;
            }
            else
            {
                transform.position += Input.GetAxis("Horizontal") * speed * Vector3.right * Time.fixedDeltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
        else
        {
            float progress = Mathf.Clamp((Time.fixedTime - turnStart) / this.rotationDuration, 0f, 1f);
            if (turningL)
            {
                transform.rotation = Quaternion.Euler(0, progress * -180, 0);
            }
            else if (turningR)
            {
                transform.rotation = Quaternion.Euler(0, (1 - progress) * -180, 0);
            }
            if (progress >= 1f)
            {
                turningL = false;
                turningR = false;
            }
        }
    }

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other) {
        RebellionBehaviour rebellion = other.gameObject.GetComponent<RebellionBehaviour>();
        if (rebellion) {
			rebellion.DiveDown();
		}
	}
}
