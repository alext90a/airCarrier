using UnityEngine;
using System.Collections;

public class Aircraft : MonoBehaviour {

    
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
    Trajectory mFlyTrajectory;

    Airport mAirport = null;


    BaseState mBaseState = new BaseState();
    FlyState mFlyState = new FlyState();
    LandingState mLandingState = new LandingState();
    RunawayState mRunawayState = new RunawayState();
    BaseState mCurState = null;

    private void Awake()
    {
        mCurState = mBaseState;
        mBaseState.setData(this);
        mRunawayState.setData(this);
        mFlyState.setData(this);
        mLandingState.setData(this);
        mLineRender.getPoints().Add(new Vector3());
        mLineRender.getPoints().Add(new Vector3());
    }

    // Use this for initialization
    void Start () {
        
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

    public void setOnRunaway(TrajectoryPoint firstPoint, Airport airport, Trajectory flyTrajectory)
    {
        gameObject.SetActive(true);
        mCurState = mRunawayState;
        mRunawayState.initiate(firstPoint);
        mFlyTrajectory = flyTrajectory;
        mAirport = airport;
        transform.position = firstPoint.transform.position;
        
    }
    
    public void setOnFly()
    {
        transform.parent = null;
        mCurState = mFlyState;
        mFlyState.initiate(mFlyTrajectory.getPoint(0));
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
        gameObject.SetActive(false);
    }

    public void setAircraftGui(AircraftInfoGUI infoGui)
    {
        mAircraftGui = infoGui;
        mAircraftGui.setNameText(name);
    }

    

    

    
}
