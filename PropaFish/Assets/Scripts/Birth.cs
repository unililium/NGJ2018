using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birth : MonoBehaviour {

    public float youthDuration;

	// Use this for initialization
	void Start () {
        GetComponent<Breed>().enabled = false; // Sexual maturity comes later
    }
	
	// Update is called once per frame
	void Update () {
		if (youthDuration > 0)
        {
            youthDuration -= Time.deltaTime;            
        }
        else
        {
            GetComponent<Breed>().enabled = true;
            Destroy(this);
        }
	}
}
