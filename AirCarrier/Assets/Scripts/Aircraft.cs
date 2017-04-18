﻿using UnityEngine;
using System.Collections;

public class Aircraft : MonoBehaviour {

    Airport mAirport = null;
    [SerializeField]
    TrajectoryPoint mCurTargetPoint;
    [SerializeField]
    LineRender mLineRender;

    AircraftInfoGUI mAircraftGui = null;





    float mCurSpeed = 0f;
    float mTargetSpeed = 2f;
    float mCurAngularSpeed = GameConstants.kAircraftMaxAngleSpeed;

    Trajectory mLandingTrajectory;
    Trajectory mRunAwayTrajecotry;

    

    BaseState mBaseState = new BaseState();
    FlyState mFlyState = new FlyState();
    LandingState mLandingState = new LandingState();
    RunawayState mRunawayState = new RunawayState();
    BaseState mCurState = null;

	// Use this for initialization
	void Start () {
        mCurState = mBaseState;
        mBaseState.setData(this);
        mRunawayState.setData(this);
        mFlyState.setData(this);
        mLandingState.setData(this);
        mLineRender.getPoints().Add(new Vector3());
        mLineRender.getPoints().Add(new Vector3());
	}
	
	// Update is called once per frame
	void Update () {

        mCurState.update();
        mAircraftGui.updateSpeed(mCurSpeed);
        mCurTargetPoint = mCurState.getCurTargetPoint();
        if(mCurTargetPoint != null)
        {
            mLineRender.getPoints()[0] = transform.position;
            mLineRender.getPoints()[1] = mCurTargetPoint.transform.position;
        }
        
    }

    public void setAngularSpeed(float angularSpeed)
    {
        mCurAngularSpeed = angularSpeed;
    }

    public float getAngularSpeed()
    {
        return mCurAngularSpeed;
    }

    public void setCurSpeed(float speed)
    {
        mCurSpeed = speed;
    }

    public float getCurSpeed()
    {
        return mCurSpeed;
    }

    public void setTargetSpeed(float targetSpeed)
    {
        mTargetSpeed = targetSpeed;
    }

    public float getTargetSpeed()
    {
        return mTargetSpeed;
    }

    public void updateFlytimeGui(float flyTime)
    {
        mAircraftGui.updateFlytime(flyTime, GameConstants.kAircraftFlytime);
    }

    
    public void setOnFly(Airport airport, TrajectoryPoint firstPoint)
    {
        transform.parent = null;
        mAirport = airport;
        mCurState = mFlyState;
        mFlyState.initiate(firstPoint);
        mAircraftGui.enableCameraButton(true);
    }
    
    public void trySetLandingState()
    {
        if (mAirport.isLandingAvailable())
        {

            mCurState = mLandingState;
            mAirport.occupyLandingLane();
            mLandingTrajectory = mAirport.getLandingTajectory();
            mLandingState.initiate(mLandingTrajectory.getPoint(0));
            
        }
        
    }

    public void setLandedState()
    {
        mCurState = mBaseState;
        mTargetSpeed = 0f;
        mCurSpeed = 0f;
        mAirport.landAircraft(this);
    }

    public void setAircraftGui(AircraftInfoGUI infoGui)
    {
        mAircraftGui = infoGui;
    }

    

    

    
}
