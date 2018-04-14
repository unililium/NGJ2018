using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchOrDecompose : MonoBehaviour {

    public float incubationTime;
    public GameObject babyFishPrefab; // none if it just has to decompose

    private FloatWhileFalling floatWhileFalling;

    // Use this for initialization
    void Start () {
        floatWhileFalling = GetComponent<FloatWhileFalling>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!floatWhileFalling)
        {
            incubationTime -= Time.deltaTime;
            if (incubationTime <= 0)
            {
                if (babyFishPrefab)
                {
                    GameObject babyFish = Instantiate<GameObject>(babyFishPrefab, this.transform);
                    babyFish.name = "Fish born @ " + Time.time;
                } // else, just decompose
                Destroy(this.gameObject);
            }
        }
	}
}
