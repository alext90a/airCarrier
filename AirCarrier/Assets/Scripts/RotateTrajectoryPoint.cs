using UnityEngine;
using System.Collections;

public class RotateTrajectoryPoint : TrajectoryPoint {

    TrajectoryPoint mEndTrajectoryPoint = null;

    public void setEndTrajectoryPoint(TrajectoryPoint point)
    {
        mEndTrajectoryPoint = point;
    }
}
