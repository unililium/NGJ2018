using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suction : MonoBehaviour {    

    public Transform animSpawnPos;
    public Transform foodDispenserPos;
    public GameObject sphereTravelPrefab;
    public KeyCode suctionKey;
    public GameObject foodPrefab;    
    public float foodChunkPeriod;
    public float maxEnergyPerChunk;

    GameObject[] smallFish;
    private Queue<float> energyPerChunk = new Queue<float>();
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
        if (energyPerChunk.Count == 0) // It can be easily modified to work continuously
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
                GameObject orb = Instantiate(sphereTravelPrefab, animSpawnPos.position, animSpawnPos.rotation, gameObject.transform.parent);
                EatFoodScript eatenFishStats = fish.GetComponent<EatFoodScript>();
                float energyEaten = eatenFishStats ? eatenFishStats.Energy : 0f;
                orb.GetComponent<FoodAnimScript>().StartFoodAnim(this, energyEaten);
                Destroy(fish);
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "smallFish") {
            other.gameObject.GetComponent<Prone>().insideSuctionRange = true;
<<<<<<< HEAD
=======
            // Debug.Log("Fish collision");
>>>>>>> f96937e70102774b3295eb857af7d7d4f99b47b4
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "smallFish") {
            other.gameObject.GetComponent<Prone>().insideSuctionRange = false;
<<<<<<< HEAD
=======
            // Debug.Log("Fish exit");
>>>>>>> f96937e70102774b3295eb857af7d7d4f99b47b4
        }
    }

    public void StartChunkingFood(float totalEnergy)
    {
        int foodChunksCount = Mathf.CeilToInt(totalEnergy / maxEnergyPerChunk);
        for (int i = 0; i < foodChunksCount; i++)
        {
            energyPerChunk.Enqueue(totalEnergy / foodChunksCount);
        }
    }

    IEnumerator ChunkFood()
    {
        Debug.Log("Chunking");
        yield return new WaitForSeconds(foodChunkPeriod);
        Debug.Log("Dispensing");
        GameObject food = Instantiate<GameObject>(foodPrefab, foodDispenserPos.transform.position, foodDispenserPos.transform.rotation);
        food.GetComponent<Nutrient>().energy = energyPerChunk.Dequeue();
        chunking = false;
    }
} 