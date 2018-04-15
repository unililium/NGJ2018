using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgeStack : MonoBehaviour {

    public int algeAmount;
    public float refreshVariable;

    private float minWeedHeight;
    private float weedScale;
    private bool stop;

    private static AlgeStack instance;

    // Use this for initialization
    void Start () {
        if (instance)
        {
            Destroy(this.gameObject);
            Debug.LogError("We only need one AlgeStack");
        }
        else
        {
            instance = this;
            float initialWeedHeight = this.transform.localScale.y;
            minWeedHeight = initialWeedHeight / 5;
            weedScale = (initialWeedHeight - minWeedHeight) / algeAmount;
            InvokeRepeating("RefreshAlg", refreshVariable, refreshVariable);
        }        
	}

    public static bool EatOne()
    {
        if (!instance)
        {
            Debug.LogWarning("Trying to eat algae but no stack instance exists");
            return false;
        }
        if (instance.algeAmount > 0)
        {
            instance.algeAmount--;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update () {
        Vector3 currentScale = this.transform.localScale;
        Vector3 targetScale = new Vector3(currentScale.x, minWeedHeight + (algeAmount * weedScale), currentScale.z);
        this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, targetScale, 0.01f);
	}

    public void RefreshAlg() {
        algeAmount++;
        // Debug.Log("New alga!");
    }
}
