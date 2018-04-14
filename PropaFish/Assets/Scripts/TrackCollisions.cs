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

    void OnTriggerEnter(Collider other)
    {
        collided.Add(other.gameObject);
        Debug.Log(other.gameObject.name + " fell");
    }

    void OnTriggerExit(Collider other)
    {
        collided.Remove(other.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        collided.Add(collision.collider.gameObject);
        Debug.Log(collision.collider.gameObject.name + " fell");
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
