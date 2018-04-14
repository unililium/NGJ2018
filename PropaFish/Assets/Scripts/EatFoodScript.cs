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

    private void OnTriggerEnter(Collider other)
    {
        TryAndEat(other.gameObject);       
    }

    private void OnCollisionEnter(Collision collision) {
        TryAndEat(collision.collider.gameObject);
    }

    private void TryAndEat(GameObject collidingObject) {
        //If it colllides with food and isnt eating
        if(!agony && collidingObject.tag == "food" && !isEating) {
            Debug.Log("Food registered");
            isEating = true;
            Nutrient nutrient = collidingObject.GetComponent<Nutrient>();
            if (nutrient)
            {
                energy += nutrient.energy;
                if (energy > size)
                {
                    size = energy;
                }
            }
            Destroy(collidingObject);
            StartCoroutine(StopEating());
        }
    }

    public void Die()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
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
