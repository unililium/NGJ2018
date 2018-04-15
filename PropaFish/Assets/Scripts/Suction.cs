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
                Debug.Log("Suction key pressed");
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
        foreach (GameObject fish in Prone.GetSuckables()) {
            float energyEaten = 0f;
            EatFoodScript eatenFishStats = fish.GetComponent<EatFoodScript>();
            Nutrient otherNutrientStats = fish.GetComponent<Nutrient>();
            if (eatenFishStats)
            {
                energyEaten = eatenFishStats ? eatenFishStats.Energy : 0f;
            }
            else if (otherNutrientStats)
            {
                energyEaten = otherNutrientStats.energy;
            }
            if (energyEaten > 0f) {
                GameObject orb = Instantiate(sphereTravelPrefab, animSpawnPos.position, animSpawnPos.rotation, gameObject.transform.parent);                
                orb.GetComponent<FoodAnimScript>().StartFoodAnim(this, energyEaten);
            }
            Destroy(fish); // or whatever object with Prone but containing no energy it may be...
            //GetComponent<AudioSource>().Play();
            GetComponents<AudioSource>()[0].Play();
        }
    }

    private void OnTriggerEnter(Collider other) {
        Prone prone = other.gameObject.GetComponent<Prone>();
        if (prone)
        {
            prone.insideSuctionRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        Prone prone = other.gameObject.GetComponent<Prone>();
        if (prone)
        {
            prone.insideSuctionRange = false;
        }
    }

    public void StartChunkingFood(float totalEnergy)
    {
        int foodChunksCount = Mathf.CeilToInt(totalEnergy / maxEnergyPerChunk);
        for (int i = 0; i < foodChunksCount; i++)
        {
            energyPerChunk.Enqueue(totalEnergy / foodChunksCount);
        }
        GetComponents<AudioSource>()[1].Play();
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