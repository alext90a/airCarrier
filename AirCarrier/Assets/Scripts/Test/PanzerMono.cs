using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanzerMono : MonoBehaviour, IPanzer
{
    private int _counter = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire()
    {
        ++_counter;
        Debug.Log("PanzerMono fire! " + _counter.ToString());
    }
}
