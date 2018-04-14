using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suction : MonoBehaviour {

    GameObject[] smallFish;

    public Transform animSpawnPos;
    public GameObject sphereTravelPrefab;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")) {
            EatAFish();
        }
	}

    public void EatAFish() {
        smallFish = GameObject.FindGameObjectsWithTag("smallFish");

        foreach (GameObject fish in smallFish) {
            if(fish.GetComponent<Prone>().insideSuctionRange == true) {
                Destroy(fish);
                //TODO: Start Suction animation and send out food on other end
                GameObject orb = Instantiate(sphereTravelPrefab, animSpawnPos.position, animSpawnPos.rotation);
                orb.transform.parent = gameObject.transform.parent;
                orb.GetComponent<FoodAnimScript>().StartFoodAnim();
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "smallFish") {
            other.gameObject.GetComponent<Prone>().insideSuctionRange = true;
            Debug.Log("Fish collision");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "smallFish") {
            other.gameObject.GetComponent<Prone>().insideSuctionRange = false;
            Debug.Log("Fish exit");
        }
    }


}
