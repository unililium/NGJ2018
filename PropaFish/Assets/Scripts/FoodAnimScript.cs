using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAnimScript : MonoBehaviour
{

    Animator anim;
    Suction suction;

    private float energyEaten;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            anim.SetTrigger("Active");
        }
    }

    public void StartFoodAnim(Suction suction, float energyEaten)
    {
        this.suction = suction;
        this.energyEaten = energyEaten;
        StartCoroutine(StartFoodDelay());
    }

    public void EndFood()
    {
        suction.StartChunkingFood(energyEaten);
        Debug.Log("Play food falling");
        Destroy(gameObject);
    }

    IEnumerator StartFoodDelay()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Active");
    }
}
