using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgeStack : MonoBehaviour {

    public int algeAmount = 20;
    public float refreshVariable;

    bool stop;

	// Use this for initialization
	void Start () {
        algeAmount = 20;
        refreshVariable = 10;

        InvokeRepeating("RefreshAlg", 10f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RefreshAlg() {
        algeAmount += 20;
        Debug.Log("New Alge!");
    }

}
