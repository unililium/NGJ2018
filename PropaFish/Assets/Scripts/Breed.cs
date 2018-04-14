using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breed : MonoBehaviour {

    public GameObject eggPrefab;
    public float minDistanceBetweenBirths;
    public float maxDistanceBetweenBirths;

    private float birthDate;

	// Use this for initialization
	void Start () {
        PlanToMakeABaby();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > birthDate)
        {
            MakeABaby();
            PlanToMakeABaby();
        }
    }

    public void MakeABaby()
    {        
        GameObject egg = Instantiate<GameObject>(eggPrefab, transform.position + Vector3.down, transform.rotation);
        float babyEnergy = GetComponent<EatFoodScript>().Energy / 3;
        GetComponent<EatFoodScript>().Energy -= babyEnergy;
        egg.GetComponent<Nutrient>().energy = babyEnergy;
    }

    private void PlanToMakeABaby()
    {
        birthDate = Time.time + UnityEngine.Random.Range(minDistanceBetweenBirths, maxDistanceBetweenBirths);
    }
}
