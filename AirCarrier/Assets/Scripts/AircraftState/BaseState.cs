using UnityEngine;
using System.Collections;

public class BaseState
{
    protected Aircraft mAircraft = null;
    protected TrajectoryPoint mCurTargetPoint = null;
    public void setData(Aircraft aircraft)
    {
        mAircraft = aircraft;
    }
	public virtual void update()
    {

    }

    public TrajectoryPoint getCurTargetPoint()
    {
        return mCurTargetPoint;
    }

    public virtual void initiate(TrajectoryPoint point)
    {

    }
}
