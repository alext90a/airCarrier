using UnityEngine;
using System.Collections;

public class Aircraft : MonoBehaviour {

    Airport mAirport = null;
    TrajectoryPoint mCurTargetPoint;
    

    bool mIsOnFly = false;

    Vector3 mCurTargetPos = new Vector3();
    Vector3 mCurStartPos = new Vector3();
    float mTimeToTarget = 0f;
    float mTimeSinceTargetDefine = 0f;
    float mSpeed = 2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (mIsOnFly)
        {
            mTimeSinceTargetDefine += Time.deltaTime;
            float delta = mTimeSinceTargetDefine / mTimeToTarget;
            transform.position = Vector3.Lerp(mCurStartPos, mCurTargetPos, mTimeSinceTargetDefine / mTimeToTarget);
            if(delta>=1f)
            {
                mCurStartPos = transform.position;
                mCurTargetPoint = mCurTargetPoint.Next;
                mCurTargetPos = mCurTargetPoint.transform.position;
                defineTimeToTarget();
                mTimeSinceTargetDefine = 0f;
            }
        }
	
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
    }

    void defineTimeToTarget()
    {
        mTimeToTarget = (mCurTargetPoint.transform.position - transform.position).magnitude / mSpeed;
    }
}
