using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAlge : MonoBehaviour {

    public float startDelay;
    public float nextDelay;

    AlgeStack algeStack;

    bool stop;

	// Use this for initialization
	void Start () {
        startDelay = Random.Range(4.0f, 10.5f);
        nextDelay = Random.Range(3.0f, 8.5f);

        algeStack = GameObject.Find("AlgeS").GetComponent<AlgeStack>();
        StartCoroutine(EatTheAlge());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator EatTheAlge() {
        yield return new WaitForSeconds(startDelay);
        while(stop == false) {
            if(algeStack.algeAmount > 0) {
                algeStack.algeAmount--;
                Debug.Log("Eaten once");
            } else {
                Debug.Log("No food");
            }
            yield return new WaitForSeconds(nextDelay);
        }
    }


}
