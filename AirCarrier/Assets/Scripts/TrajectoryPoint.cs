using UnityEngine;
using System.Collections;

public class TrajectoryPoint : MonoBehaviour {

    [SerializeField]
    float mSpeed;

    TrajectoryPoint mNext;
    Vector3 mDir;


    private void Start()
    {
        if(mSpeed > GameConstants.kAircraftMaxSpeed)
        {
            mSpeed = GameConstants.kAircraftMaxSpeed;
        }
        if(mSpeed < GameConstants.kAircraftMinSpeed)
        {
            mSpeed = GameConstants.kAircraftMinSpeed;
        }
    }

    public void setNext(TrajectoryPoint nextPoint)
    {
        
        mNext = nextPoint;
        if(mNext == null)
        {
            return;
        }
        mDir = (mNext.transform.position - transform.position).normalized;
    }

    public TrajectoryPoint getNext()
    {
        return mNext;
    }

    public Vector3 getDir()
    {
        return mDir;
    }

    public float getSpeed()
    {
        return mSpeed;
    }
}
