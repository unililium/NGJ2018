﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour {

    public float incubationTime;
    public GameObject babyFishPrefab;

    private FloatWhileFalling floatWhileFalling;

    // Use this for initialization
    void Start () {
        floatWhileFalling = GetComponentInChildren<FloatWhileFalling>();
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
                }
                else
                {
                    Debug.LogError("This egg has no fish embryo in it");
                }
                Destroy(this.gameObject);
            }
        }
	}
}
