using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]
public class Trajectory : MonoBehaviour {

    [SerializeField]
    TrajectoryPoint[] mControlPoints;
    [SerializeField]
    bool mIsLoop = true;
    [SerializeField]
    TrajectoryPoint mPointInstance;
    [SerializeField]
    GameObject mRealTrajectroy = null;

    int mSegmentCount = 0;
    List<TrajectoryPoint> mPoints = new List<TrajectoryPoint>();
    
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
        /*
        for (int i = 0; i < mPoints.Length; ++i)
        {
            Debug.DrawLine(mPoints[i].transform.position, mPoints[(i + 1) % mPoints.Length].transform.position, Color.red, 0f, false);
        }
        */
        
    }
    static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }
    public int lineCount = 100;
    public float radius = 3.0f;
    private void OnRenderObject()
    {
        
        CreateLineMaterial();
        lineMaterial.SetPass(0);
        GL.PushMatrix();
        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        
        for (int i = 0; i < mSegmentCount; ++i)
        {
            //Debug.DrawLine(mPoints[i].transform.position, mPoints[(i + 1) % mPoints.Length].transform.position, Color.red, 0f, false);
            GL.Vertex3(mPoints[i].transform.position.x, mPoints[i].transform.position.y, mPoints[i].transform.position.z);
            GL.Vertex3(mPoints[(i + 1) % mPoints.Count].transform.position.x, mPoints[(i + 1) % mPoints.Count].transform.position.y, mPoints[(i + 1) % mPoints.Count].transform.position.z);
        }
        
        
        GL.End();
        GL.PopMatrix();
    }



    public TrajectoryPoint getPoint(int index)
    {

        return mPoints[index];
    }

    public void updatePoints()
    {
        mPoints.Clear();
        int childCount = mRealTrajectroy.transform.childCount;
        for(int i= childCount-1; i>=0; --i)
        {
            GameObject.DestroyImmediate(mRealTrajectroy.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < mControlPoints.Length; ++i)
        {
            mControlPoints[i].setNext(mControlPoints[(i + 1) % mControlPoints.Length]);
        }
        mSegmentCount = mControlPoints.Length;
        if (mIsLoop == false)
        {
            mControlPoints[mControlPoints.Length - 1].setNext(null);
            mSegmentCount -= 1;
        }

        //create real path
        TrajectoryPoint lastCreatedPoint = null;
        for (int i = 0; i < mControlPoints.Length; ++i)
        {
            TrajectoryPoint prevPoint = mControlPoints[(i + mControlPoints.Length - 1)%mControlPoints.Length];
            if (prevPoint.getNext() == mControlPoints[i])
            {
                TrajectoryPoint newPoint = GameObject.Instantiate(mPointInstance, mRealTrajectroy.transform) as TrajectoryPoint;
                newPoint.transform.position = mControlPoints[i].transform.position - prevPoint.getDir() * GameConstants.kTrajectoryRoundness;
                mPoints.Add(newPoint);
                TrajectoryPoint newPoint2 = GameObject.Instantiate(mPointInstance, mRealTrajectroy.transform) as TrajectoryPoint;
                newPoint2.transform.position = mControlPoints[i].transform.position + mControlPoints[i].getDir() * GameConstants.kTrajectoryRoundness;
                mPoints.Add(newPoint2);
                newPoint.setNext(newPoint2);
                lastCreatedPoint = newPoint2;
            }
        }
        if (mIsLoop && mPoints.Count>0)
        {
            mPoints[0].setNext(lastCreatedPoint);
        }

        mSegmentCount = mPoints.Count;
        if (mIsLoop == false)
        {
            mPoints[mPoints.Count - 1].setNext(null);
            mSegmentCount -= 1;
        }
    }
}
