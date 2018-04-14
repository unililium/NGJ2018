using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFoodScript : MonoBehaviour {

    public bool isEating = false;

    public float size;    
    public float postEatingBreakDuration;
    public float fullnessDecreaseSpeed; // fullness lost per second

    private float energy;
    private Vector3 initialScale;
    private ShowHunger showHunger;

    public float Energy
    {
        get { return energy; }
    }

    public float Fullness
    {
        get { return energy / size; }
    }

	// Use this for initialization
	void Start () {
        initialScale = transform.localScale;
        energy = size;
        showHunger = GetComponent<ShowHunger>();
    }
	
	// Update is called once per frame
	void Update () {
        energy -= size * fullnessDecreaseSpeed * Time.deltaTime;
        transform.localScale = initialScale * size;
        if (showHunger)
        {
            showHunger.NotifyFullness(this.Fullness);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //If it colllides with food and isnt eating
        if(collision.gameObject.tag == "food" && !isEating) {
            Debug.Log("Food registered");
            isEating = true;
            Nutrient nutrient = collision.gameObject.GetComponent<Nutrient>();
            if (nutrient)
            {
                energy += nutrient.energy;
            }
            Destroy(collision.gameObject);
            StartCoroutine(StopEating());
        }
    }

    IEnumerator StopEating() {
        yield return new WaitForSeconds(postEatingBreakDuration);
        isEating = false;
    }
}
