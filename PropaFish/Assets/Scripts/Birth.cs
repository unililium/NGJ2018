using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birth : MonoBehaviour {

    public float youthDuration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (youthDuration > 0)
        {
            youthDuration -= Time.deltaTime;
            // TODO Does anything special newborn fish do
        }
        else
        {
            Destroy(this);
        }
	}
}
