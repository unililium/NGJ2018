using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suction : MonoBehaviour {    

    public Transform animSpawnPos;
    public Transform foodDispenserPos;
    public GameObject sphereTravelPrefab;
    public KeyCode suctionKey;
    public GameObject foodPrefab;
    public int amountOfFood;
    public float foodChunkPeriod;

    GameObject[] smallFish;
    private int foodChunks = 0;
    private bool chunking = false;

    // Use this for initialization
    void Start () {
        if (suctionKey == KeyCode.None)
        {
            suctionKey = KeyCode.Space;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (foodChunks == 0)
        {
            if (Input.GetKeyDown(suctionKey))
            {
                EatAFish();
            }
        }
        else
        {
            if (!chunking)
            {
                chunking = true;
                StartCoroutine(ChunkFood());                
            }
        }
    }

    public void EatAFish() {
        smallFish = GameObject.FindGameObjectsWithTag("smallFish");

        foreach (GameObject fish in smallFish) {
            if(fish.GetComponent<Prone>().insideSuctionRange == true) {
                Destroy(fish);
                //TODO: Start Suction animation and send out food on other end
                GameObject orb = Instantiate(sphereTravelPrefab, animSpawnPos.position, animSpawnPos.rotation, gameObject.transform.parent);
                orb.GetComponent<FoodAnimScript>().StartFoodAnim(this);
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "smallFish") {
            other.gameObject.GetComponent<Prone>().insideSuctionRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "smallFish") {
            other.gameObject.GetComponent<Prone>().insideSuctionRange = false;
        }
    }

    public void StartChunkingFood()
    {
        foodChunks = amountOfFood;
    }

    IEnumerator ChunkFood()
    {
        Debug.Log("Chunking");
        yield return new WaitForSeconds(foodChunkPeriod);
        Debug.Log("Dispensing");
        Instantiate<GameObject>(foodPrefab, foodDispenserPos.transform.position, foodDispenserPos.transform.rotation);
        foodChunks--;
        chunking = false;
    }
} 