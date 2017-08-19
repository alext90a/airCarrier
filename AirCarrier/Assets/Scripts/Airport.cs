using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class Airport : MonoBehaviour {

    [SerializeField]
    Trajectory mRunawayTrajectory = null;
    [SerializeField]
    Trajectory mPatrolTrajectory = null;
    [SerializeField]
    Trajectory mLandingTrajectory = null;
    [SerializeField]
    GameObject mAircarftPrefab = null;
    /*
    [SerializeField]
    AircraftGUIMono _mAirportGuiMono = null;
    */

    [Inject] private IAircraftGUI _aircraftGui;

    LinkedList<Aircraft> mAvailableAircrafts = new LinkedList<Aircraft>();
    float mTimeSinceLastLaunch = GameConstants.kTimeBetweenAircaftLaunch;
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
            curAircraft.setAircraftGui(_aircraftGui.getNextInfoGui());
        }

        _aircraftGui.setAvailableAircraft(mAvailableAircrafts.Count);
	}

    // Update is called once per frame
    void Update()
    {
        mTimeSinceLastLaunch += Time.deltaTime;
        mTimeSinceAirOnRunaway += Time.deltaTime;
    }



    public void launchAircraft()
    {
        if (mAvailableAircrafts.Count == 0)
        {
            _aircraftGui.setAirportMessage(GameConstants.kNoAircraftText);
            return;
        }

        if (mTimeSinceLastLaunch < GameConstants.kTimeBetweenAircaftLaunch)
        {
            _aircraftGui.setAirportMessage(GameConstants.kLaunchTimePeriodText);
            return;
        }
        mTimeSinceLastLaunch = 0f;
        mTimeSinceAirOnRunaway = 0f;
        mAvailableAircrafts.First.Value.setOnRunaway(mRunawayTrajectory.getPoint(0), this, mPatrolTrajectory);
        mAvailableAircrafts.RemoveFirst();

        _aircraftGui.setAvailableAircraft(mAvailableAircrafts.Count);

    }

    public bool isLandingAvailable()
    {
        return mIsLandingLaneAvailable;
    }

    public void occupyLandingLane()
    {
        mIsLandingLaneAvailable = false;
        _aircraftGui.showLandingMessage(true);
    }

    public void landAircraft(Aircraft aircraft)
    {
        mIsLandingLaneAvailable = true;
        aircraft.transform.parent = null;
        aircraft.transform.parent = transform;
        mAvailableAircrafts.AddFirst(aircraft);
        _aircraftGui.setAvailableAircraft(mAvailableAircrafts.Count);
        _aircraftGui.showLandingMessage(false);
    }

    public Trajectory getLandingTajectory()
    {
        return mLandingTrajectory;
    }

}
