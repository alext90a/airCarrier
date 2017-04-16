using UnityEngine;
using System.Collections;

public class TrajectoryPoint : MonoBehaviour {

    TrajectoryPoint mNext;
    Vector3 mDir;

    /*
    public TrajectoryPoint Next
    {
        set
        {
            mNext = value;
        }
        get
        {
            return mNext;
        }
    }
    */

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
}
