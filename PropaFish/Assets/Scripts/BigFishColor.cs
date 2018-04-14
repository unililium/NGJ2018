using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFishColor : MonoBehaviour {

    Renderer rend;
    Shader shader;

    GameObject body;

    int currentLevel;

	// Use this for initialization
	void Start () {
        body = transform.Find("Body").gameObject;
        rend = body.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Standard");
        currentLevel = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("v")) {
            FoodUP();
        }
        if (Input.GetKeyDown("b")) {
            FoodDown();
        }
    }

    public void FoodUP() {
        //Take the level, and make it go up
        switch (currentLevel) {
            case 0:
                rend.material.color = new Color(.2f, 0, 0, 1);
                transform.localScale *= 1.2f;
                currentLevel++;
                break;
            case 1:
                rend.material.color = new Color(.4f, 0, 0, 1);
                transform.localScale *= 1.2f;
                currentLevel++;
                break;
            case 2:
                rend.material.color = new Color(.6f, 0, 0, 1);
                transform.localScale *= 1.2f;
                currentLevel++;
                break;
            case 3:
                rend.material.color = new Color(.8f, 0, 0, 1);
                transform.localScale *= 1.2f;
                currentLevel++;
                break;
            case 4:
                rend.material.color = new Color(1, 0, 0, 1);
                currentLevel++;
                break;
            case 5:
                Debug.Log("End of the line");
                break;
        } 
    }

    public void FoodDown() {

        //Take the level, and make it go up
        switch (currentLevel) {
            case 0:
                Debug.Log("Die");
                break;
            case 1:
                rend.material.color = new Color(0, 0, 0, 1);
                currentLevel--;
                break;
            case 2:
                rend.material.color = new Color(.2f, 0, 0, 1);
                transform.localScale *= .8f;
                currentLevel--;
                break;
            case 3:
                rend.material.color = new Color(.4f, 0, 0, 1);
                transform.localScale *= .8f;
                currentLevel--;
                break;
            case 4:
                rend.material.color = new Color(.6f, 0, 0, 1);
                transform.localScale *= .8f;
                currentLevel--;
                break;
            case 5:
                rend.material.color = new Color(.8f, 0, 0, 1);
                transform.localScale *= .8f;
                currentLevel--;
                break;
        }
    }
}
