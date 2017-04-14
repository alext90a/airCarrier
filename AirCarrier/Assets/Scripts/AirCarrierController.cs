using UnityEngine;
using System.Collections;

public class AirCarrierController : MonoBehaviour {


    [SerializeField]
    Rigidbody mRigidbody = null;

    float mCurSpeed = 0f;
    float mCurRotation = 0f;
    Vector3 mCurPowerDir = new Vector3();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if(Input.GetKeyDown(KeyCode.W))
        {
            mCurSpeed += GameConstants.kCarrierIncreaseStepSpeed;
            if(mCurSpeed > GameConstants.kCarrierMaxForwardSpeed)
            {
                mCurSpeed = GameConstants.kCarrierMaxForwardSpeed;
            }
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            mCurSpeed -= GameConstants.kCarrierIncreaseStepSpeed;
            if(mCurSpeed < GameConstants.kCarrierMaxBackwardSpeed)
            {
                mCurSpeed = GameConstants.kCarrierMaxBackwardSpeed;
            }
        }

        if(Mathf.Abs(mCurSpeed)<0.5f)
        {
            mCurSpeed = 0f;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            mCurRotation -= GameConstants.kCarrierRotationStepSpeed;
            if(mCurRotation < -GameConstants.kCarrierMaxRoationSpeed)
            {
                mCurRotation = -GameConstants.kCarrierMaxRoationSpeed;
            }
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            mCurRotation += GameConstants.kCarrierRotationStepSpeed;
            if(mCurRotation > GameConstants.kCarrierMaxRoationSpeed)
            {
                mCurRotation = GameConstants.kCarrierMaxRoationSpeed;
            }
        }
        if(Mathf.Abs(mCurRotation) < 0.5f)
        {
            mCurRotation = 0f;
        }

        transform.Rotate(new Vector3(0f, mCurRotation * Time.deltaTime, 0f));
        transform.position += transform.forward * mCurSpeed * Time.deltaTime;
	}
}
