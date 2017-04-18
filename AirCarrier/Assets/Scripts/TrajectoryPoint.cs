using UnityEngine;
using System.Collections.Generic;

public class TrajectoryPoint : MonoBehaviour {

    [SerializeField]
    float mSpeed;
    [SerializeField]
    TrajectoryPoint mNext;
    Vector3 mDir;

    List<Vector3> mDrawingPoints = new List<Vector3>();

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

    public void setDir(Vector3 dir)
    {
        mDir = dir;
    }

    public float getSpeed()
    {
        return mSpeed;
    }
}
