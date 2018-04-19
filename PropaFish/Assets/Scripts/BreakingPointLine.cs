using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPointLine : MonoBehaviour {

    public int totalFishOver;

    public bool gameDone;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(totalFishOver >= 5 && gameDone == false) {
            gameDone = true;
            Debug.Log("VIVA LA REVOLUTION!");
            GetComponent<AudioSource>().Play();
            StartCoroutine(KingDead());
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "smallFish" && other.gameObject.name != "Guardian" && other.gameObject.name != "King") {
            if(other.gameObject.GetComponent<Prone>().hasTriggered == false) {
                other.gameObject.GetComponent<Prone>().hasTriggered = true;
                totalFishOver++;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "smallFish" && other.gameObject.name != "Guardian" && other.gameObject.name != "King") {
            if (other.gameObject.GetComponent<Prone>().hasTriggered == true) {
                other.gameObject.GetComponent<Prone>().hasTriggered = false;
                totalFishOver--;
            }
        }
    }

    IEnumerator KingDead() {
        Debug.Log("King is dead, all hail the rebellion");
        GameObject.Find("Rebel").GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(6);
        GameObject.Find("Rebel").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("GameController").GetComponent<EndGameScript>().GameOver();
    }

    private void OnGUI() {
        GUI.color = Color.black;
        GUI.skin.label.fontSize = 18;
        GUI.Label(new Rect(100, 100 - 35, 200, 80), "Rebel infiltration: " + totalFishOver + "/5");
    }


}
