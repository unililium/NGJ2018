using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquarium : MonoBehaviour {

    public TrackCollisions ground;
    public Transform waterLine;
    public Transform privilegeLine;

    private static Aquarium instance;

    // Use this for initialization
    void Start () {
        if (instance)
        {
            Destroy(this.gameObject);
            Debug.LogError("We only need one Aquarium");
        }
        else
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static bool IsInWater(GameObject gameObject) {
        if (!instance)
        {
            return false;
        }
        return (gameObject.transform.position.y <= instance.waterLine.position.y);
    }

    public static bool IsBelowPrivilegeLine(GameObject gameObject)
    {
        if (!instance)
        {
            return false;
        }
        return (gameObject.transform.position.y < instance.privilegeLine.position.y);
    }

    public static bool IsTouchingGround(GameObject gameObject)
    {
        if (!instance)
        {
            return false;
        }
        return (instance.ground.IsColliding(gameObject));
    }
}
