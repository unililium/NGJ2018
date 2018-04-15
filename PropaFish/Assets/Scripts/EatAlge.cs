using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAlge : MonoBehaviour {

    public float averageLunchInterval;
    public bool stop;

    private float startDelay;
    private float nextDelay;
    private EatFoodScript stomach;    

	// Use this for initialization
	void Start () {
        stomach = GetComponent<EatFoodScript>();
        StartCoroutine(EatTheAlge());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator EatTheAlge() {
        do
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f) * averageLunchInterval);
            if (Aquarium.IsBelowPrivilegeLine(this.gameObject) && AlgeStack.EatOne())
            {
                // Debug.Log("Alga eaten by " + this.gameObject.name);
                stomach.AcquireEnergy(1);
            }            
        } while (!stop);
    }


}
