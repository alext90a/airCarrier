using UnityEngine;
using System.Collections;

public class AirCarrierController : MonoBehaviour {



    [SerializeField]
    AircarrierGUI mGui = null;
    [SerializeField]
    Airport mAirport = null;

    float mCurSpeed = 0f;
    float mCurTargetSpeed = 0f;
    float mCurRotation = 0f;
    Vector3 mCurPowerDir = new Vector3();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(mAirport.isLandingAvailable())
        {
            processInput();
        }
        else
        {
            mCurRotation = 0f;
        }
        
        if(Mathf.Abs(mCurTargetSpeed - mCurSpeed) > 0.005)
        {
            if (mCurSpeed < mCurTargetSpeed)
            {
                mCurSpeed += GameConstants.kAircraftAcceleration * Time.deltaTime;
            }
            else
            {
                mCurSpeed -= GameConstants.kAircraftAcceleration * Time.deltaTime;
            }
                
        }
        else
        {
            mCurSpeed = mCurTargetSpeed;
        }


        if (Mathf.Abs(mCurSpeed) < Mathf.Epsilon)
        {
            mCurSpeed = 0f;
        }

        
        if(Mathf.Abs(mCurRotation) < Mathf.Epsilon)
        {
            mCurRotation = 0f;
        }

        transform.Rotate(new Vector3(0f, mCurRotation * Time.deltaTime, 0f));
        transform.position += transform.forward * mCurSpeed * Time.deltaTime;

        mGui.updatedSpeed(mCurSpeed, mCurTargetSpeed);
        mGui.updateRotaiton(mCurRotation);
	}

    void processInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            mCurTargetSpeed += GameConstants.kCarrierIncreaseStepSpeed;
            if (mCurTargetSpeed > GameConstants.kCarrierMaxForwardSpeed)
            {
                mCurTargetSpeed = GameConstants.kCarrierMaxForwardSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            mCurTargetSpeed -= GameConstants.kCarrierIncreaseStepSpeed;
            if (mCurTargetSpeed < GameConstants.kCarrierMaxBackwardSpeed)
            {
                mCurTargetSpeed = GameConstants.kCarrierMaxBackwardSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            mCurRotation -= GameConstants.kCarrierRotationStepSpeed;
            if (mCurRotation < -GameConstants.kCarrierMaxRoationSpeed)
            {
                mCurRotation = -GameConstants.kCarrierMaxRoationSpeed;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mCurRotation += GameConstants.kCarrierRotationStepSpeed;
            if (mCurRotation > GameConstants.kCarrierMaxRoationSpeed)
            {
                mCurRotation = GameConstants.kCarrierMaxRoationSpeed;
            }
        }
    }
}
