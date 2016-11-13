using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplitMesh))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SplitMesh myScript = (SplitMesh) target;
        if(Application.isPlaying) {
            if(GUILayout.Button("Split")) {
                myScript.Split();
            }   
        }
    }
}