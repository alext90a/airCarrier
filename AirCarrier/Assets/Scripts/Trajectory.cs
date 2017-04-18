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
    [SerializeField]
    RotateTrajectoryPoint mRotatePointInstance = null;

    int mSegmentCount = 0;
    List<TrajectoryPoint> mPoints = new List<TrajectoryPoint>();
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

    private void OnRenderObject()
    {
        
        CreateLineMaterial();
        lineMaterial.SetPass(0);
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        /*
        for(int i=0; i<mTrajectoryDrawingPoints.Count; ++i)
        {
            GL.Vertex3(mTrajectoryDrawingPoints[i].x, mTrajectoryDrawingPoints[i].y, mTrajectoryDrawingPoints[i].z);
        }
        */

        for(int i=0; i<mPoints.Count; ++i)
        {
            GL.Vertex3(mPoints[i].transform.localPosition.x, mPoints[i].transform.localPosition.y, mPoints[i].transform.localPosition.z);
            GL.Vertex3(mPoints[i].transform.localPosition.x + mPoints[i].getDir().x, mPoints[i].transform.localPosition.y + mPoints[i].getDir().y, mPoints[i].transform.localPosition.z + mPoints[i].getDir().z);
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
        mTrajectoryDrawingPoints.Clear();
        TrajectoryPoint lastCreatedPoint = null;
        for (int i = 0; i < mControlPoints.Length; ++i)
        {
            TrajectoryPoint prevPoint = mControlPoints[(i + mControlPoints.Length - 1)%mControlPoints.Length];
            TrajectoryPoint curPoint = mControlPoints[i];
            if (prevPoint.getNext() == mControlPoints[i] && mControlPoints[i].getNext()!=null)
            {
                float radius = 1f;
                float angle = Vector2.Angle(new Vector2((-1f * prevPoint.getDir()).x, (-1f * prevPoint.getDir()).z),
                    new Vector2(curPoint.getDir().x, curPoint.getDir().z));
                if (angle > 1)
                {
                     
                    float fDistance = radius / Mathf.Tan(Mathf.Deg2Rad* (angle/2f));
                    TrajectoryPoint startPoint = PrefabController.getInstance().createTrajectoryPoint();
                    startPoint.transform.parent = mRealTrajectroy.transform;
                    startPoint.transform.position = curPoint.transform.position - prevPoint.getDir() * fDistance;
                    if(lastCreatedPoint != null)
                    {
                        mTrajectoryDrawingPoints.Add(lastCreatedPoint.transform.localPosition);
                        mTrajectoryDrawingPoints.Add(startPoint.transform.localPosition);
                    }

                    mPoints.Add(startPoint);

                    RotateTrajectoryPoint rotationPoint = PrefabController.getInstance().createRotateTrajectoryPoint();
                    rotationPoint.transform.parent = mRealTrajectroy.transform;
                    Vector3 rotationPos = (-1f * prevPoint.getDir() + curPoint.getDir()).normalized;
                    rotationPos = rotationPos * radius / Mathf.Sin(Mathf.Deg2Rad * (angle / 2f));
                    rotationPoint.transform.position = curPoint.transform.position + rotationPos;
                    //mPoints.Add(rotationPoint);

                    TrajectoryPoint endPoint = PrefabController.getInstance().createTrajectoryPoint();
                    endPoint.transform.parent = mRealTrajectroy.transform;
                    endPoint.transform.position = curPoint.transform.position + curPoint.getDir() * fDistance;

                    for(int j=0; j<5; ++j)
                    {
                        float delta = (float)j / 5f;

                        mTrajectoryDrawingPoints.Add(rotationPoint.transform.localPosition + Vector3.Slerp(startPoint.transform.localPosition - rotationPoint.transform.localPosition,
                            endPoint.transform.localPosition - rotationPoint.transform.localPosition, delta));

                        mTrajectoryDrawingPoints.Add(rotationPoint.transform.localPosition + Vector3.Slerp(startPoint.transform.localPosition - rotationPoint.transform.localPosition, 
                            endPoint.transform.localPosition-rotationPoint.transform.localPosition, delta+(1f)/5f));

                    }
                    rotationPoint.setEndTrajectoryPoint(endPoint);
                    rotationPoint.setStartTrajectoryPoint(startPoint);
                    rotationPoint.setRotationRadius(radius);
                    rotationPoint.setAngularSpeed(-startPoint.getSpeed() / radius);

                    mPoints.Add(endPoint);
                    startPoint.setNext(endPoint);
                    rotationPoint.setNext(endPoint);
                    if(lastCreatedPoint != null)
                    {
                        lastCreatedPoint.setNext(startPoint);
                    }
                    lastCreatedPoint = endPoint;
                    //endPoint.getDrawingPoint().Add(endPoint.transform.localPosition);
                }
                else
                {
                    TrajectoryPoint startPoint = GameObject.Instantiate(mPointInstance, mRealTrajectroy.transform) as TrajectoryPoint;
                    startPoint.transform.position = curPoint.transform.position;
                    mPoints.Add(startPoint);
                }             
                
            }
            else
            {
                TrajectoryPoint newPoint = PrefabController.getInstance().createTrajectoryPoint();
                newPoint.transform.parent = mRealTrajectroy.transform;
                newPoint.transform.position = mControlPoints[i].transform.position;
                if(lastCreatedPoint!=null)
                {
                    mTrajectoryDrawingPoints.Add(lastCreatedPoint.transform.localPosition);
                    mTrajectoryDrawingPoints.Add(newPoint.transform.localPosition);
                    lastCreatedPoint.setNext(newPoint);
                }
                mPoints.Add(newPoint);
                lastCreatedPoint = newPoint;
            }
        }

        if (mIsLoop && mPoints.Count>0)
        {
            lastCreatedPoint.setNext(mPoints[0]);
            mTrajectoryDrawingPoints.Add(lastCreatedPoint.transform.localPosition);
            mTrajectoryDrawingPoints.Add(mPoints[0].transform.localPosition);
        }

        mSegmentCount = mPoints.Count;
        if (mIsLoop == false)
        {
            mPoints[mPoints.Count - 1].setNext(null);
            mSegmentCount -= 1;
        }
        
        Vector2 vec1 = new Vector2(1, 0);
        Vector2 vec2 = new Vector2(-1, 1);
        
        float angle1 = Vector2.Angle(vec1, vec2);
        int k = 10;
    }
}
