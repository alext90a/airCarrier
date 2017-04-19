using UnityEngine;
using System.Collections.Generic;

public class Airport : MonoBehaviour {

    [SerializeField]
    Trajectory mRunawayTrajectory;
    [SerializeField]
    Trajectory mPatrolTrajectory;
    [SerializeField]
    Trajectory mLandingTrajectory;
    [SerializeField]
    GameObject mAircarftPrefab = null;
    

    [SerializeField]
    AircraftGUI mAirportGUI;

    LinkedList<Aircraft> mAvailableAircrafts = new LinkedList<Aircraft>();
    float mTimeSinceLastLaunch = GameConstants.kTimeBetweenAircaftLaunch;
    bool mIsAircarftOnRunaway = false;
    float mTimeSinceAirOnRunaway = 0f;
    bool mIsLandingLaneAvailable = true;

    private void Awake()
    {
        for(int i=0; i< GameConstants.kAircaftsAmount; ++i)
        {
            Aircraft aircraft = (GameObject.Instantiate(mAircarftPrefab, transform)as GameObject).GetComponent<Aircraft>();
            aircraft.gameObject.name = mAircarftPrefab.name + " " + (i + 1).ToString();
            aircraft.setHeightEchelon(i * GameConstants.kEchelonDistance);
            mAvailableAircrafts.AddLast(aircraft);

        }
    }
	// Use this for initialization
	void Start ()
    {
	
        foreach(Aircraft curAircraft in mAvailableAircrafts)
        {            
            curAircraft.setAircraftGui(mAirportGUI.getNextInfoGui());
        }

        mAirportGUI.setAvailableAircraft(mAvailableAircrafts.Count);
	}

    // Update is called once per frame
    void Update()
    {

        mTimeSinceLastLaunch += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.H))
        {
            launchAircraft();
            mAirportGUI.setAvailableAircraft(mAvailableAircrafts.Count);
        }


        mTimeSinceAirOnRunaway += Time.deltaTime;

    }



    void launchAircraft()
    {
        if (mAvailableAircrafts.Count == 0)
        {
            Debug.Log("No available aircrafts");
            return;
        }

        if (mTimeSinceLastLaunch < GameConstants.kTimeBetweenAircaftLaunch)
        {
            Debug.Log("Previous carrier is on launch");
            return;
        }
        mTimeSinceLastLaunch = 0f;
        mTimeSinceAirOnRunaway = 0f;
        mAvailableAircrafts.First.Value.setOnRunaway(mRunawayTrajectory.getPoint(0), this, mPatrolTrajectory);
        mAvailableAircrafts.RemoveFirst();
        
    }

    public bool isLandingAvailable()
    {
        return mIsLandingLaneAvailable;
    }

    public void occupyLandingLane()
    {
        mIsLandingLaneAvailable = false;
    }

    public void landAircraft(Aircraft aircraft)
    {
        mIsLandingLaneAvailable = true;
        aircraft.transform.parent = null;
        aircraft.transform.parent = transform;
        mAvailableAircrafts.AddFirst(aircraft);
        mAirportGUI.setAvailableAircraft(mAvailableAircrafts.Count);
    }

    public Trajectory getLandingTajectory()
    {
        return mLandingTrajectory;
    }

}
