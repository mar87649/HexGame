// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;

// [CustomEditor(typeof(HexCell))]
// [CanEditMultipleObjects]

// public class HexCellEditor : Editor
// {
//     SerializedProperty cordinates;
//     SerializedProperty Passable;
//     SerializedProperty Cost;
//     SerializedProperty Terrain;

//     void OnEnable()
//     {
//         // cordinates = serializedObject.FindProperty("_cordinates");
//         // Passable = serializedObject.FindProperty("Passable");
//         // Cost = serializedObject.FindProperty("Cost");
//         // Terrain = serializedObject.FindProperty("Terrain");
//     }
//     public override void OnInspectorGUI()
//     {
//         serializedObject.Update();
//         // EditorGUILayout.PropertyField(cordinates);
//         // EditorGUILayout.PropertyField(Passable);
//         // EditorGUILayout.PropertyField(Cost);
//         // EditorGUILayout.PropertyField(Terrain);

//         serializedObject.ApplyModifiedProperties();
//     }

//     public void OnSceneGUI()
//     {

//     }

// }
