using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PanzerController : MonoBehaviour
{
    [Inject]public IPanzer _panzer;

	// Use this for initialization
	void Start ()
	{
	    _panzer.Fire();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
