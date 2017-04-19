using UnityEngine;
using System.Collections.Generic;


[ExecuteInEditMode]
public class Trajectory : MonoBehaviour {

    [SerializeField]
    TrajectoryPoint[] mControlPoints;
    [SerializeField]
    bool mIsLoop = true;



    List<Vector3> mTrajectoryDrawingPoints = new List<Vector3>();
    private void Awake()
    {

        updatePoints();
        

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
    


    public TrajectoryPoint getPoint(int index)
    {

        return mControlPoints[index];
    }

    public void updatePoints()
    {
        for(int i=0; i<mControlPoints.Length; ++i)
        {
            mControlPoints[i].setNext(mControlPoints[(i + 1) % mControlPoints.Length]);
        }
        if(!mIsLoop)
        {
            mControlPoints[mControlPoints.Length - 1].setNext(null);
            int prevPointInd = mControlPoints.Length - 2;
            if(prevPointInd>0)
            {
                Vector3 dir = mControlPoints[prevPointInd].getDir();
                dir.y = 0f;
                mControlPoints[mControlPoints.Length - 1].setDir(dir);
            }
        }
    }
}
