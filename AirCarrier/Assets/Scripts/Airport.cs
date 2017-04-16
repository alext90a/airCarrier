using UnityEngine;
using System.Collections.Generic;

public class Airport : MonoBehaviour {

    [SerializeField]
    Trajectory mPatrolTrajectory;
    [SerializeField]
    Transform mRunawayStart;
    [SerializeField]
    Transform mRunawayEnd;
    [SerializeField]
    Aircraft[] mAircrafts;

    LinkedList<Aircraft> mAvailableAircrafts = new LinkedList<Aircraft>();
    float mTimeSinceLastLaunch = GameConstants.kTimeBetweenAircaftLaunch;
    Aircraft mCurAircraftOnRunaway = null;
    float mTimeSinceAirOnRunaway = 0f;
	// Use this for initialization
	void Start ()
    {
	
        for(int i=0; i<mAircrafts.Length; ++i)
        {
            if(mAircrafts[i] == null)
            {
                continue;
            }
            mAvailableAircrafts.AddLast(mAircrafts[i]);
        }
	}

    // Update is called once per frame
    void Update()
    {

        mTimeSinceLastLaunch += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.H))
        {
            launchAircraft();
        }


        if (mCurAircraftOnRunaway != null)
        {
            checkRunawayAircaft();
        }
    }

    void checkRunawayAircaft()
    {
        mTimeSinceAirOnRunaway += Time.deltaTime;
        float delta = mTimeSinceAirOnRunaway / GameConstants.kTimeBetweenAircaftLaunch;
        if (delta >= 1f)
        {
            mCurAircraftOnRunaway.setOnFly(this, mPatrolTrajectory.getPoint(0));
            mCurAircraftOnRunaway = null;
            return;
        }
        mCurAircraftOnRunaway.transform.position = Vector3.Lerp(mRunawayStart.position, mRunawayEnd.position, delta);
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
        mCurAircraftOnRunaway = mAvailableAircrafts.First.Value;
        mAvailableAircrafts.RemoveFirst();
        
    }


}
