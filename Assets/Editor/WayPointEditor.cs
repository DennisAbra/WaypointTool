using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointManager))]
public class WayPointEditor : Editor
{
    WaypointManager manager;
    SerializedObject so;
   
    SerializedProperty propPoints;
    SerializedProperty shouldLoop;

    private void OnEnable()
    {
        manager = (WaypointManager)target;
        SceneView.duringSceneGui += DuringSceneUI;
        so = serializedObject;
        propPoints = so.FindProperty("points");
        shouldLoop = so.FindProperty("looping");
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= DuringSceneUI;
    }

    public override void OnInspectorGUI()
    {
        so.Update();
        EditorGUILayout.PropertyField(shouldLoop);
        EditorGUILayout.PropertyField(propPoints);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Node"))
        {
            manager.Add();
        }

        if (GUILayout.Button("Remove Node"))
        {
            manager.Remove();
        }

        GUILayout.EndHorizontal();
 
        so.ApplyModifiedProperties();
    }

    void DuringSceneUI(SceneView view)
    {
        for(int i = 0; i < propPoints.arraySize; i++)
        {
            Handles.color = Color.red;
            if(Handles.Button(manager.CalculateMidPoint(i), Quaternion.identity, 1f, 1f, Handles.SphereHandleCap)) { manager.Insert(i); }
        }

        so.Update();

        for(int i = 0; i < propPoints.arraySize; i++)
        {
            if(i > 0)
            {
                propPoints.GetArrayElementAtIndex(i).vector3Value = Handles.PositionHandle(manager.points[i], Quaternion.identity);
            }
            else
            {
                propPoints.GetArrayElementAtIndex(i).vector3Value = manager.transform.position;   
            }
        }

        so.ApplyModifiedProperties();
    }
}

