using UnityEngine;
using System.Collections;

public class LandingState : BaseState {

    public override void update()
    {
        mAircraft.transform.position = Vector3.MoveTowards(mAircraft.transform.position, mCurTargetPoint.transform.position, mAircraft.getCurSpeed() * Time.deltaTime);
        if (mAircraft.transform.position == mCurTargetPoint.transform.position)
        {
            if (mCurTargetPoint.getNext() != null)
            {
                mAircraft.transform.forward = (mCurTargetPoint.getNext().transform.position - mAircraft.transform.position).normalized;
                mAircraft.setCurSpeed(mCurTargetPoint.getSpeed());
            }
            mCurTargetPoint = mCurTargetPoint.getNext();
            if (mCurTargetPoint == null)
            {
                mAircraft.setLandedState();
            }
        }
    }

    public override void initiate(TrajectoryPoint trajectoryPoint)
    {
        mCurTargetPoint = trajectoryPoint;
        mAircraft.transform.forward = (mCurTargetPoint.transform.position - mAircraft.transform.position).normalized;
    }
}
