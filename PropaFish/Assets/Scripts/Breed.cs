using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breed : MonoBehaviour {

    public GameObject eggPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            MakeABaby();
        }
    }

    public void MakeABaby()
    {
        Debug.Log("Made a baby");
        GameObject egg = Instantiate<GameObject>(eggPrefab, transform.position + Vector3.down, transform.rotation);
    }
}
