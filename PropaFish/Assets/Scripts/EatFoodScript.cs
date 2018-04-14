using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFoodScript : MonoBehaviour {

    public bool isEating = false;

    public float size;    
    public float postEatingBreakDuration;
    public float fullnessDecreaseSpeed; // fullness lost per second
    public float minEnergyBeforeDeath;    

    private float energy;
    private Vector3 scaleFactor;
    private ShowHunger showHunger;
    private bool agony = false;

    public float Energy
    {
        get { return energy; }
        set { this.energy = value; }
    }

    public float Fullness
    {
        get { return energy / size; }
    }

	// Use this for initialization
	void Start () {
        scaleFactor = transform.localScale / size;
        energy = size;
        showHunger = GetComponent<ShowHunger>();
    }
	
	// Update is called once per frame
	void Update () {
        if (agony)
        {
            return;
        }
        energy -= size * fullnessDecreaseSpeed * Time.deltaTime;
        if (energy <= minEnergyBeforeDeath)
        {
            agony = true;
            Die();
        }
        transform.localScale =  size * scaleFactor;
        if (showHunger)
        {
            showHunger.NotifyFullness(this.Fullness);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //If it colllides with food and isnt eating
        if(!agony && collision.gameObject.tag == "food" && !isEating) {
            Debug.Log("Food registered");
            isEating = true;
            Nutrient nutrient = collision.gameObject.GetComponent<Nutrient>();
            if (nutrient)
            {
                energy += nutrient.energy;
                if (energy > size)
                {
                    size = energy;
                }
            }
            Destroy(collision.gameObject);
            StartCoroutine(StopEating());
        }
    }

    public void Die()
    {
        gameObject.AddComponent<Rigidbody>().useGravity = true;
        GetComponent<FloatWhileFalling>().enabled = true; // slowly floats down
        GetComponent<HatchOrDecompose>().enabled = true; // and decomposes
        GetComponent<FishBehaviour>().enabled = false; // and stops swimming
        GetComponent<Breed>().enabled = false; // and stops reproducing :-S
        Birth youth = GetComponent<Birth>();
        if (youth) { // this is sad :-(
            youth.enabled = false; // and stops being young - or becomes young forever, actually
        }
    }

    IEnumerator StopEating() {
        yield return new WaitForSeconds(postEatingBreakDuration);
        isEating = false;
    }
}
