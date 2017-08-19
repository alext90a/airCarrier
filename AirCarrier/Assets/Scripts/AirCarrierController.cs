using UnityEngine;
using System.Collections;
using System;
using Zenject;

public class AirCarrierController : MonoBehaviour, IControlledUnit {



    [Inject]
    AircarrierGUI mGui = null;
    [Inject]
    Airport mAirport = null;

    float mCurSpeed = 0f;
    float mCurTargetSpeed = 0f;
    float mCurRotation = 0f;
    Vector3 mCurPowerDir = new Vector3();
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {



        if(!mAirport.isLandingAvailable())
        {
            mCurRotation = 0f;
        }  
        
        if(Mathf.Abs(mCurTargetSpeed - mCurSpeed) > 0.005)
        {
            if (mCurSpeed < mCurTargetSpeed)
            {
                mCurSpeed += GameConstants.kAircarrierAcceleration * Time.deltaTime;
            }
            else
            {
                mCurSpeed -= GameConstants.kAircarrierAcceleration * Time.deltaTime;
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

    public void IncreaseSpeed()
    {
        if (!mAirport.isLandingAvailable())
        {
            return;
        }
        mCurTargetSpeed += GameConstants.kCarrierIncreaseStepSpeed;
        if (mCurTargetSpeed > GameConstants.kCarrierMaxForwardSpeed)
        {
            mCurTargetSpeed = GameConstants.kCarrierMaxForwardSpeed;
        }
    }

    public void DecreaseSpeed()
    {
        if (!mAirport.isLandingAvailable())
        {
            return;
        }
        mCurTargetSpeed -= GameConstants.kCarrierIncreaseStepSpeed;
        if (mCurTargetSpeed < GameConstants.kCarrierMaxBackwardSpeed)
        {
            mCurTargetSpeed = GameConstants.kCarrierMaxBackwardSpeed;
        }
    }

    public void RotateLeft()
    {
        if (!mAirport.isLandingAvailable())
        {
            return;
        }
        mCurRotation -= GameConstants.kCarrierRotationStepSpeed;
        if (mCurRotation < -GameConstants.kCarrierMaxRoationSpeed)
        {
            mCurRotation = -GameConstants.kCarrierMaxRoationSpeed;
        }
    }

    public void RotateRight()
    {
        if (!mAirport.isLandingAvailable())
        {
            return;
        }
        mCurRotation += GameConstants.kCarrierRotationStepSpeed;
        if (mCurRotation > GameConstants.kCarrierMaxRoationSpeed)
        {
            mCurRotation = GameConstants.kCarrierMaxRoationSpeed;
        }
    }

    public void LaunchAircraft()
    {
        mAirport.launchAircraft();
    }
}
