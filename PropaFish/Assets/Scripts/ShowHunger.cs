using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHunger : MonoBehaviour {

    List<Renderer> rends = new List<Renderer>();
    List<Color> initialColors = new List<Color>();

	// Use this for initialization
	void Start () {
        rends.AddRange(GetComponentsInChildren<Renderer>());
        // rend.material.shader = Shader.Find("Standard");
        foreach (Renderer rend in rends)
        {
            initialColors.Add(rend.material.color);
        }        
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void NotifyFullness(float fullness)
    {
        // rend.material.color = new Color(1 - fullness, 0, 0, 1);
        for (int i = 0; i < rends.Count; i++)
        {
            Renderer rend = rends[i];
            Color initialColor = initialColors[i];
            rend.material.color = new Color(initialColor.r, initialColor.g * fullness, initialColor.b * fullness, initialColor.a);
        }
    }
}
