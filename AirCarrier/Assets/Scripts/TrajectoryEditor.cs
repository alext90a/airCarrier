using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Trajectory))]
public class TrajectoryEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Trajectory trajectory = (Trajectory)target;
        if (GUILayout.Button("Update trajectory"))
        {

            trajectory.updatePoints();
            
        }
        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
    }
}
