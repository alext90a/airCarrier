using UnityEngine;
using System.Collections;

public class FlyState : BaseState {

    float mTimeSinceFlyStart = 0f;
    float mTimeSinceNewTrajectoryPointAdded = 0f;
    float mMaximumTimeWithoutSpeedBreak = 1f;
    float mMaximumTimeToPointTravel = 1f;

    public override void update()
    {
        mTimeSinceNewTrajectoryPointAdded += Time.deltaTime;
        if (mAircraft.getCurSpeed() < mAircraft.getTargetSpeed())
        {
            mAircraft.setCurSpeed(mAircraft.getCurSpeed() + GameConstants.kAircraftAcceleration * Time.deltaTime);
        }
        else 
        {
            mAircraft.setCurSpeed(mAircraft.getCurSpeed() - GameConstants.kAircraftAcceleration * Time.deltaTime);
        }

        mAircraft.transform.forward = Vector3.RotateTowards(mAircraft.transform.forward,
            mCurTargetPoint.transform.position - mAircraft.transform.position,
            Mathf.Deg2Rad * mAircraft.getAngularSpeed() * Time.deltaTime, 0f).normalized;

        mAircraft.transform.position = mAircraft.transform.position + mAircraft.transform.forward * mAircraft.getCurSpeed() * Time.deltaTime;
        float distance = (mCurTargetPoint.transform.position - mAircraft.transform.position).magnitude;

        if (distance < 0.5f)
        {

            setNextTrajectoryPoint(mCurTargetPoint.getNext());

        }
        else
        {
            if (mTimeSinceNewTrajectoryPointAdded >= mMaximumTimeToPointTravel)
            {
                setNextTrajectoryPoint(mCurTargetPoint.getNext());
            }
            else if (mTimeSinceNewTrajectoryPointAdded >= mMaximumTimeWithoutSpeedBreak)
            {
                mAircraft.setTargetSpeed(GameConstants.kAircraftMinSpeed);
            }
        }






        mTimeSinceFlyStart += Time.deltaTime;
        mAircraft.updateFlytimeGui(GameConstants.kAircraftFlytime - mTimeSinceFlyStart);
        
        if (mTimeSinceFlyStart > GameConstants.kAircraftFlytime)
        {
            mAircraft.trySetLandingState();
            
        }
    }

    public void initiate(TrajectoryPoint trajectoryPoint)
    {
        mCurTargetPoint = trajectoryPoint;
        calculateTravelValues();
        mTimeSinceFlyStart = 0f;
    }

    void setNextTrajectoryPoint(TrajectoryPoint destinationPoint)
    {
        mCurTargetPoint = destinationPoint;

        calculateTravelValues();
    }

    void calculateTravelValues()
    {
        mAircraft.setTargetSpeed(mCurTargetPoint.getSpeed());

        mTimeSinceNewTrajectoryPointAdded = 0f;
        mMaximumTimeWithoutSpeedBreak = 2 * (mCurTargetPoint.transform.position - mAircraft.transform.position).magnitude / GameConstants.kAircraftMinSpeed;
        mMaximumTimeToPointTravel = 2 * mMaximumTimeWithoutSpeedBreak;
    }
}
