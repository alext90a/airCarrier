using UnityEngine;
using System.Collections;

public class Aircraft : MonoBehaviour {

    Airport mAirport = null;
    [SerializeField]
    TrajectoryPoint mCurTargetPoint;
    AircraftInfoGUI mAircraftGui = null;

    bool mIsOnFly = false;
    bool mIsOnLanding = false;



    float mCurSpeed = 0f;
    float mTargetSpeed = 2f;
    float mTimeSinceFlyStart = 0f;
    float mCurAngularSpeed = GameConstants.kAircraftMaxAngleSpeed;

    Trajectory mLandingTrajectory;

    float mTimeSinceNewTrajectoryPointAdded = 0f;
    float mMaximumTimeWithoutSpeedBreak = 1f;
    float mMaximumTimeToPointTravel = 1f;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (mIsOnFly)
        {

            mTimeSinceNewTrajectoryPointAdded += Time.deltaTime;
            if(mCurSpeed < mTargetSpeed)
            {
                mCurSpeed += GameConstants.kAircraftAcceleration * Time.deltaTime;
            }
            else
            {
                mCurSpeed -= GameConstants.kAircraftAcceleration * Time.deltaTime;
            }
            
            transform.forward = Vector3.RotateTowards(transform.forward, mCurTargetPoint.transform.position - transform.position, Mathf.Deg2Rad * mCurAngularSpeed * Time.deltaTime, 0f).normalized;

            transform.position = transform.position + transform.forward * mCurSpeed * Time.deltaTime;
            float distance = (mCurTargetPoint.transform.position - transform.position).magnitude;
            
            if (distance <0.5f)
            {

                setNextTrajectoryPoint();

            }
            else
            {
                if (mTimeSinceNewTrajectoryPointAdded >= mMaximumTimeToPointTravel)
                {
                    setNextTrajectoryPoint();
                }
                else if (mTimeSinceNewTrajectoryPointAdded >= mMaximumTimeWithoutSpeedBreak)
                {
                    mTargetSpeed = GameConstants.kAircraftMinSpeed;
                }
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
            transform.position = Vector3.MoveTowards(transform.position, mCurTargetPoint.transform.position, mCurSpeed * Time.deltaTime);
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

        mAircraftGui.updateSpeed(mCurSpeed);
	}

    public void setOnFly(Airport airport, TrajectoryPoint firstPoint)
    {
        transform.parent = null;
        mAirport = airport;
        mCurTargetPoint = firstPoint;
        mIsOnFly = true;

        mAircraftGui.enableCameraButton(true);
        mTimeSinceFlyStart = 0f;

    }


    public void setAircraftGui(AircraftInfoGUI infoGui)
    {
        mAircraftGui = infoGui;
    }

    static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    private void OnRenderObject()
    {
        if(mCurTargetPoint == null)
        {
            return;
        }

        CreateLineMaterial();
        lineMaterial.SetPass(0);
        GL.PushMatrix();
        //GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Color(Color.green);

        GL.Vertex3(transform.position.x, transform.position.y, transform.position.z);
        GL.Vertex3(mCurTargetPoint.transform.position.x, mCurTargetPoint.transform.position.y, mCurTargetPoint.transform.position.z);


        GL.End();
        GL.PopMatrix();
    }

    void setNextTrajectoryPoint()
    {
        mCurTargetPoint = mCurTargetPoint.getNext();

        mTargetSpeed = mCurTargetPoint.getSpeed();

        mTimeSinceNewTrajectoryPointAdded = 0f;
        mMaximumTimeWithoutSpeedBreak = 2 * (mCurTargetPoint.transform.position - transform.position).magnitude / GameConstants.kAircraftMinSpeed;
        mMaximumTimeToPointTravel = 2 * mMaximumTimeWithoutSpeedBreak;
    }
}
