using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexCell))]
[CanEditMultipleObjects]

public class HexCellEditor : Editor
{
    SerializedProperty Cordinates;
    SerializedProperty Passable;
    SerializedProperty Cost;
    SerializedProperty Terrain;

    void OnEnable()
    {
        Cordinates = serializedObject.FindProperty("Cordinates");
        Passable = serializedObject.FindProperty("Passable");
        Cost = serializedObject.FindProperty("Cost");
        Terrain = serializedObject.FindProperty("Terrain");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(Cordinates);
        EditorGUILayout.PropertyField(Passable);
        EditorGUILayout.PropertyField(Cost);
        EditorGUILayout.PropertyField(Terrain);

        serializedObject.ApplyModifiedProperties();
    }

    public void OnSceneGUI()
    {

    }

}
