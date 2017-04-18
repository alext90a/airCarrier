using UnityEngine;
using System.Collections;

public class RotateTrajectoryPoint : TrajectoryPoint {

    TrajectoryPoint mEndTrajectoryPoint = null;
    TrajectoryPoint mStartTrajectoryPoint = null;
    float mRotationRadius = 1f;
    float mAngularSpeed = 1f;

    public void setEndTrajectoryPoint(TrajectoryPoint point)
    {
        mEndTrajectoryPoint = point;
    }

    public TrajectoryPoint getEndTrajectoryPoint()
    {
        return mEndTrajectoryPoint;
    }

    public void setStartTrajectoryPoint(TrajectoryPoint point)
    {
        mStartTrajectoryPoint = point;
    }

    public TrajectoryPoint getStartTrajectoryPoint()
    {
        return mStartTrajectoryPoint;
    }

    public void setRotationRadius(float radius)
    {
        mRotationRadius = radius;
    }

    public float getRotationRadius()
    {
        return mRotationRadius;
    }

    public void setAngularSpeed(float angularSpeed)
    {
        mAngularSpeed = angularSpeed;
    }

    public float getAngularSpeed()
    {
        return mAngularSpeed;
    }
    
}
