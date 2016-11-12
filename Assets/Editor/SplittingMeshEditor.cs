using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplittingMesh))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SplittingMesh myScript = (SplittingMesh) target;
        if(GUILayout.Button("Split")) {
            myScript.Split();
        }
        if(GUILayout.Button("Unsplit")) {
            myScript.UnSplit();
        }
    }
}