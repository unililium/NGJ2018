using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchOrDecompose : MonoBehaviour {

    public float incubationTime;
    public GameObject babyFishPrefab; // none if it just has to decompose

    private FloatWhileFalling floatWhileFalling;

    // Use this for initialization
    void Start () {
        floatWhileFalling = GetComponent<FloatWhileFalling>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!floatWhileFalling)
        {
            incubationTime -= Time.deltaTime;
            if (incubationTime <= 0)
            {
                if (babyFishPrefab)
                {
                    GameObject babyFish = Instantiate<GameObject>(babyFishPrefab, this.transform.position, Quaternion.identity);
                    babyFish.name = "Fish born @ " + Time.time;
                    EatFoodScript babyStats = babyFish.GetComponent<EatFoodScript>();
                    babyStats.Energy = this.GetComponent<Nutrient>().energy;
                    babyFish.transform.localScale /= 2;
                    babyStats.size /= 2;
                    Birth birth = babyFish.GetComponent<Birth>();
                    if (birth)
                    {
                        birth.enabled = true;
                    }
                } // else, just decompose
                Destroy(this.gameObject);
            }
        }
	}
}
