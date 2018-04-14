using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopInAir : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Aquarium.IsInWater(this.gameObject))
        {
            Destroy(this.gameObject);
        }
    }
}
