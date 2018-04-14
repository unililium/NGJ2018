using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prone : MonoBehaviour {

    private static List<Prone> suckables = new List<Prone>();
    public bool insideSuctionRange;
    

	// Use this for initialization
	void Start () {
        suckables.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        suckables.Remove(this);
    }

    public static List<GameObject> GetSuckables()
    {
        List<GameObject> sucked = new List<GameObject>();
        foreach (Prone prone in suckables)
        {
            if (prone.insideSuctionRange)
            {
                sucked.Add(prone.gameObject);
            }
        }
        return sucked;
    }
}
