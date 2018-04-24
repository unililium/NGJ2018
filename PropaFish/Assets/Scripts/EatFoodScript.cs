using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFoodScript : MonoBehaviour {

    public bool isEating = false;

    public float size;
    public float maxSize;    
    public float postEatingBreakDuration;
    public float fullnessDecreaseSpeed; // fullness lost per second
    public float minEnergyBeforeDeath;    

    private float energy;
    private Vector3 scaleFactor;
    private ShowHunger showHunger;
    private bool agony = false;

    public bool isKing;
    public bool isGuard;

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
        Eat(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Eat(collision.collider.gameObject);
    }

    private void Eat(GameObject food)
    {
        //If it colllides with food and isnt eating
        if(!agony && food.gameObject.tag == "food" && !isEating) {
            isEating = true;
            Nutrient nutrient = food.GetComponent<Nutrient>();
            if (nutrient)
            {
                AcquireEnergy(nutrient.energy);
            }
            if (isKing || isGuard)
            {
                //GetComponent<AudioSource>().Play();
                GetComponents<AudioSource>()[0].Play();
            }
            Destroy(food);
            StartCoroutine(StopEating());
        }
    }

    public void AcquireEnergy(float energyIntroduced)
    {
        energy += energyIntroduced;
        if (energy > size)
        {
            size = Mathf.Clamp(energy, 0f, maxSize);
            energy = Mathf.Clamp(energy, 0f, size);
        }
    }

    public void Die()
    {
        transform.Translate(Vector3.forward * 3);
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<FloatWhileFalling>().enabled = true; // slowly floats down
        GetComponent<HatchOrDecompose>().enabled = true; // and decomposes
        GetComponent<FishBehaviour>().enabled = false; // and stops swimming
        GetComponent<EatAlge>().stop = true; // and stops eating algae
        GetComponent<Breed>().enabled = false; // and stops reproducing :-S
        Birth youth = GetComponent<Birth>();
        if (youth) { // this is sad :-(
            youth.enabled = false; // and stops being young - or becomes young forever, actually
        }
        if(gameObject.GetComponent<Prone>().hasTriggered) {
            if(GameObject.Find("BreakingPointLine").GetComponent<BreakingPointLine>().totalFishOver > 0) {
                GameObject.Find("BreakingPointLine").GetComponent<BreakingPointLine>().totalFishOver--;
            }
        }

        if(isKing) {
            StartCoroutine(KingDead());
        }
    }

    IEnumerator StopEating() {
        yield return new WaitForSeconds(postEatingBreakDuration);
        isEating = false;
    }

    IEnumerator KingDead() {
        Debug.Log("King is dead, all hail the rebellion");
        GameObject.Find("DeadKing").GetComponent<SpriteRenderer>().enabled = true;
        GetComponents<AudioSource>()[1].Play();
        yield return new WaitForSeconds(6);
        GameObject.Find("DeadKing").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("GameController").GetComponent<EndGameScript>().GameOver();
    }
}
