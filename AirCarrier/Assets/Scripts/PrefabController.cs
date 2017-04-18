using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PrefabController : MonoBehaviour {

    [SerializeField]
    TrajectoryPoint mTrajectoryPointPrefab;
    [SerializeField]
    RotateTrajectoryPoint mRotateTrajPointPrefab;
    

    static PrefabController mInstance = null;
    public static PrefabController getInstance()
    {
        if(mInstance == null)
        {
            mInstance = GameObject.Find("PrefabController").GetComponent<PrefabController>();
        }
        return mInstance;
    }
    private void Awake()
    {
        mInstance = this;
    }

    public TrajectoryPoint createTrajectoryPoint()
    {
        return GameObject.Instantiate(mTrajectoryPointPrefab) as TrajectoryPoint;
    }

    public RotateTrajectoryPoint createRotateTrajectoryPoint()
    {
        return GameObject.Instantiate(mRotateTrajPointPrefab) as RotateTrajectoryPoint;
    }
}
