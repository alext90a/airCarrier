using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputSystem : MonoBehaviour
{
    [Inject]private IControlledUnit _controlledUnit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        Application.Quit();
	    }

	    if (Input.GetKeyDown(KeyCode.W))
	    {
            _controlledUnit.IncreaseSpeed();
	        
	    }

	    if (Input.GetKeyDown(KeyCode.S))
	    {
            _controlledUnit.DecreaseSpeed();
	        
	    }

	    if (Input.GetKeyDown(KeyCode.A))
	    {
            _controlledUnit.RotateLeft();
	        
	    }
	    if (Input.GetKeyDown(KeyCode.D))
	    {
            _controlledUnit.RotateRight();
	        
	    }
	    if (Input.GetKeyDown(KeyCode.H))
	    {
	        _controlledUnit.LaunchAircraft();
	    }
    }
}
