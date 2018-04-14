using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFoodScript : MonoBehaviour {

    public bool isEating = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        //If it colllides with food and isnt eating
        if(collision.gameObject.tag == "food" && !isEating) {
            Debug.Log("Food registered");
            isEating = true;
            Destroy(collision.gameObject);

            //Here change what to do based on who eats it. 
            if(gameObject.tag == "smallFish") {
                //Do something
                GetComponent<FishBehaviour>().speed += 10;
            }

            if(gameObject.tag == "KingFish") {
                //DO something
                gameObject.GetComponent<BigFishColor>().FoodUP();
            }

            StartCoroutine(StopEating());
        }
    }

    IEnumerator StopEating() {
        yield return new WaitForSeconds(2);
        Debug.Log("Done Eating");
        isEating = false;
    }
}
