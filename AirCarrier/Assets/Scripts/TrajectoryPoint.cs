using UnityEngine;
using System.Collections;

public class TrajectoryPoint : MonoBehaviour {

    TrajectoryPoint mNext;

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
}
