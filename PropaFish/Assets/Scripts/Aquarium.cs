using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquarium : MonoBehaviour {

    public TrackCollisions ground;
    public Transform waterLine;
    public Transform privilegeLine;

    private static Aquarium instance;
    private static float groundY = float.NaN;
    private static float waterY = float.NaN;

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
            groundY = this.ground.transform.position.y;
            waterY = this.waterLine.position.y;
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

    public static float GetWaterY()
    {
        return waterY;
    }

    public static float GetGroundY()
    {
        return groundY;
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
