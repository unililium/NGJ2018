using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAnimScript : MonoBehaviour {

    Animator anim;

    void Start() {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update() {
        if (Input.GetKeyDown("a")) {
            anim.SetTrigger("Active");
        }
    }

    public void StartFoodAnim() {
        StartCoroutine(StartFoodDelay());
    }

    public void EndFood() {
        //TODO: Activate whatever Mauro is doing
        Debug.Log("Play food falling");
        Destroy(gameObject);
    }

    IEnumerator StartFoodDelay() {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Active");
    }
}
