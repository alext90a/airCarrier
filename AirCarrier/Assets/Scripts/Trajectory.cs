using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

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
        for(int i=0; i< mControlPoints.Count; ++i)
        {
            GL.Vertex3(mControlPoints[i].x, mControlPoints[i].y, mControlPoints[i].z);
        }
        */
        
        /*
        for(int i=0; i< mControlPoints.Length; ++i)
        {
            GL.Vertex3(mControlPoints[i].transform.localPosition.x, mControlPoints[i].transform.localPosition.y, mControlPoints[i].transform.localPosition.z);
            GL.Vertex3(mControlPoints[i].transform.localPosition.x + mControlPoints[i].getDir().x, mControlPoints[i].transform.localPosition.y + mControlPoints[i].getDir().y, mControlPoints[i].transform.localPosition.z + mControlPoints[i].getDir().z);
        }
        */
        
        
        GL.End();
        GL.PopMatrix();
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
