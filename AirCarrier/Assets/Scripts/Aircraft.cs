using UnityEngine;
using System.Collections;

public class Aircraft : MonoBehaviour {

    Airport mAirport = null;
    TrajectoryPoint mCurTargetPoint;
    AircraftInfoGUI mAircraftGui = null;

    bool mIsOnFly = false;
    bool mIsOnLanding = false;

    Vector3 mCurTargetPos = new Vector3();
    Vector3 mCurStartPos = new Vector3();
    float mTimeToTarget = 0f;
    float mTimeSinceTargetDefine = 0f;
    float mSpeed = 2f;
    float mTimeSinceFlyStart = 0f;

    Trajectory mLandingTrajectory;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (mIsOnFly)
        {
            transform.position = Vector3.MoveTowards(transform.position, mCurTargetPos, mSpeed * Time.deltaTime);
            if(transform.position == mCurTargetPos)
            {
                transform.forward = (mCurTargetPoint.getNext().transform.position - transform.position).normalized;
                mCurTargetPoint = mCurTargetPoint.getNext();
                mCurTargetPos = mCurTargetPoint.transform.position;
            }

            mTimeSinceFlyStart += Time.deltaTime;
            mAircraftGui.updateFlytime(GameConstants.kAircraftFlytime - mTimeSinceFlyStart, GameConstants.kAircraftFlytime);
            if(mTimeSinceFlyStart > GameConstants.kAircraftFlytime)
            {
                if(mAirport.isLandingAvailable())
                {
                    mIsOnFly = false;
                    mIsOnLanding = true;
                    mAirport.occupyLandingLane();
                    mLandingTrajectory = mAirport.getLandingTajectory();
                    mCurTargetPoint = mLandingTrajectory.getPoint(0);
                    transform.forward = (mCurTargetPoint.transform.position - transform.position).normalized;
                }
            }
        }

        if(mIsOnLanding)
        {
            transform.position = Vector3.MoveTowards(transform.position, mCurTargetPoint.transform.position, mSpeed * Time.deltaTime);
            if(transform.position == mCurTargetPoint.transform.position)
            {
                if(mCurTargetPoint.getNext()!= null)
                {
                    transform.forward = (mCurTargetPoint.getNext().transform.position - transform.position).normalized;
                }
                mCurTargetPoint = mCurTargetPoint.getNext();
                if(mCurTargetPoint == null)
                {
                    mIsOnLanding = false;
                    mAirport.landAircraft(this);
                }
            }
        }

        mAircraftGui.updateSpeed(mSpeed);
	}

    public void setOnFly(Airport airport, TrajectoryPoint firstPoint)
    {
        transform.parent = null;
        mAirport = airport;
        mCurTargetPoint = firstPoint;
        mIsOnFly = true;
        defineTimeToTarget();
        mCurTargetPos = mCurTargetPoint.transform.position;
        mCurStartPos = transform.position;
        mTimeSinceTargetDefine = 0f;
        mAircraftGui.enableCameraButton(true);
        mTimeSinceFlyStart = 0f;
        transform.forward = mCurTargetPoint.getDir();
    }

    void defineTimeToTarget()
    {
        mTimeToTarget = (mCurTargetPoint.transform.position - transform.position).magnitude / mSpeed;
    }

    public void setAircraftGui(AircraftInfoGUI infoGui)
    {
        mAircraftGui = infoGui;
    }
}
