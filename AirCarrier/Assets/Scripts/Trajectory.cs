using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Trajectory : MonoBehaviour {

    [SerializeField]
    TrajectoryPoint[] mPoints;
    [SerializeField]
    bool mIsLoop;
    int mSegmentCount = 0;
    private void Awake()
    {
        List<Vector3> postions = new List<Vector3>();
        for(int i=0; i<mPoints.Length; ++i)
        {
            mPoints[i].setNext(mPoints[(i + 1) % mPoints.Length]);
            postions.Add(mPoints[i].transform.position);
           
        }
        mSegmentCount = mPoints.Length;
        if(mIsLoop == false)
        {
            mPoints[mPoints.Length - 1].setNext(null);
            mSegmentCount -= 1;
        }
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
        mSegmentCount = mPoints.Length;
        if(!mIsLoop)
        {
            mSegmentCount = mPoints.Length - 1;
        }
        
        for (int i = 0; i < mSegmentCount; ++i)
        {
            //Debug.DrawLine(mPoints[i].transform.position, mPoints[(i + 1) % mPoints.Length].transform.position, Color.red, 0f, false);
            GL.Vertex3(mPoints[i].transform.position.x, mPoints[i].transform.position.y, mPoints[i].transform.position.z);
            GL.Vertex3(mPoints[(i + 1) % mPoints.Length].transform.position.x, mPoints[(i + 1) % mPoints.Length].transform.position.y, mPoints[(i + 1) % mPoints.Length].transform.position.z);
        }
        

        GL.End();
        GL.PopMatrix();
    }



    public TrajectoryPoint getPoint(int index)
    {

        return mPoints[index];
    }
}
