using UnityEngine;
using System.Collections.Generic;

public class LineRender : MonoBehaviour {


    List<Vector3> mPoints = new List<Vector3>();

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

    public List<Vector3> getPoints()
    {
        return mPoints;
    }

    private void OnRenderObject()
    {


        CreateLineMaterial();
        lineMaterial.SetPass(0);
        GL.PushMatrix();
        //GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Color(Color.green);

        for(int i=0; i<mPoints.Count; ++i)
        {
            GL.Vertex3(mPoints[i].x, mPoints[i].y, mPoints[i].z);
        }
        


        GL.End();
        GL.PopMatrix();
    }
}
