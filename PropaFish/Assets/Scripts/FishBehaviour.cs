using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour {
	
	[Header("Movement speed"), Range(0f, 20f)]
	public float speed;
	[Header("Rotation duration"), Range(0.01f, 10f)]
	public float rotationDuration;
	[Header("Perlin Noise Multiplicator"), Range(1f, 5f)]
	public float perlinNoiseMultiplicator;
	public bool turningR = false;
	public bool turningL = false;
    float turnStart;
	float perlinX;
	float perlinY;

    void Start() {
		perlinX = Random.Range(0f, 1f);
		perlinY = Random.Range(0f, 1f); 
	}

	void FixedUpdate () {
        if (turningR || turningL)
        {
            float progress = Mathf.Clamp(Time.fixedTime - turnStart / this.rotationDuration, 0f, 1f);
            Debug.Log(progress);
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
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            Move();
        }
    }

	void Move() {
		transform.position += speed * transform.right * Time.fixedDeltaTime;

		// Random vertical mvmt
		float random = perlinNoiseMultiplicator * (Mathf.PerlinNoise(perlinX, perlinY) - 0.5f);
		perlinX += Time.fixedDeltaTime;
        //if(random > -0.2f && random < 0.2f)
        transform.position += speed * random * Vector3.up * Time.fixedDeltaTime;
        float clampedY = Mathf.Clamp(transform.position.y, Aquarium.GetGroundY(), Aquarium.GetWaterY());
        transform.position = new Vector3(transform.position.x, clampedY, 0);
    }

    void OnTriggerEnter(Collider collider)
    {
        CollideWithWall(collider.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollideWithWall(collision.collider.gameObject);
    }

    private void CollideWithWall(GameObject wall) {
		if (wall.tag == "WallR" && !turningL) {
			turningL = true;
            turnStart = Time.fixedTime;
        } else if (wall.tag == "WallL" && !turningR) {
			turningR = true;
            turnStart = Time.fixedTime;
        }
    }    
}
