using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCollisions : MonoBehaviour {

    private List<GameObject> collided = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        collided.Add(collision.collider.gameObject);
    }

    void OnCollisionExit(Collision collision)
    {
        collided.Remove(collision.collider.gameObject);
    }

    public bool IsColliding(GameObject gameObject)
    {
        return collided.Contains(gameObject);
    }
}
