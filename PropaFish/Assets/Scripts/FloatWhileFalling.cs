using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatWhileFalling : MonoBehaviour {
    public static float DRAG_IN_AIR = 0f;

    public float dragInWater;
    public float floatingInstability;
    public float floatingRange;
    public float rotationRange;

    private float perlin;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        perlin = UnityEngine.Random.Range(0.0f, 1.0f);
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!Aquarium.IsInWater(this.gameObject))
        {
            rb.drag = DRAG_IN_AIR;
            return;
        }
        rb.drag = dragInWater;
        if (Aquarium.IsTouchingGround(this.gameObject))
        {
            Destroy(this);
        }
        else
        {
            float sampleX = Mathf.PerlinNoise(Time.time * floatingInstability, perlin) - 0.5f;
            float sampleZRot = Mathf.PerlinNoise(Time.time * floatingInstability, perlin / 2) - 0.5f;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(sampleX, 0, 0) * floatingRange);
            this.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, sampleZRot) * rotationRange);
        }
    }
}
