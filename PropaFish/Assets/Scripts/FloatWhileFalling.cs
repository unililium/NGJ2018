using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatWhileFalling : MonoBehaviour {
    public float floatingInstability;
    public float floatingRange;

    private float perlin;

	// Use this for initialization
	void Start () {
        perlin = UnityEngine.Random.Range(0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Aquarium.IsTouchingGround(this.gameObject))
        {
            Destroy(this);
        }
        else
        {
            float sampleX = Mathf.PerlinNoise(Time.time * floatingInstability, perlin) - 0.5f;
            // float sampleZ = Mathf.PerlinNoise(perlin, Time.time * floatingSpeed);
            this.GetComponent<Rigidbody>().AddForce(new Vector3(sampleX, 0, 0) * floatingRange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
