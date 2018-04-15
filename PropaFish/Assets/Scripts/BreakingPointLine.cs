using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPointLine : MonoBehaviour {

    public int fishPassed;

    public bool gameDone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(fishPassed >= 5 && gameDone == false) {
            gameDone = true;
            Debug.Log("VIVA LA REVOLUTION!");
            GetComponent<AudioSource>().Play();
            GameObject.Find("GameController").GetComponent<EndGameScript>().GameOver();
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "smallFish" && other.gameObject.name != "Guardian") {
            if(other.gameObject.GetComponent<Prone>().hasTriggered == false) {
                other.gameObject.GetComponent<Prone>().hasTriggered = true;
                fishPassed++;
            }
        }
    }


}
