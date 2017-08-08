using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CustomEditor(typeof(ObjectPooler))]
public class ObjectPoolerEditor : Editor
{
    ObjectPooler _ObjectPooler;
    public override void OnInspectorGUI()
    {
        _ObjectPooler = (ObjectPooler)target;
        EditorUtility.SetDirty(_ObjectPooler);

        _ObjectPooler.defaultCount=EditorGUILayout.IntField("Default Buffer Amount",_ObjectPooler.defaultCount);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("GO");
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Buffer Amount");
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < _ObjectPooler.Prefabs.Count; i++) {
            EditorGUILayout.BeginHorizontal("box");
            _ObjectPooler.Prefabs[i].GO=(GameObject)EditorGUILayout.ObjectField(_ObjectPooler.Prefabs[i].GO, typeof(GameObject), false);
            _ObjectPooler.Prefabs[i].instanceCount=EditorGUILayout.IntField(_ObjectPooler.Prefabs[i].instanceCount);
            if (GUILayout.Button("X"))
            {
                _ObjectPooler.Prefabs.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Prefabs"))
        {
            ObjectPoolInfo info= new ObjectPoolInfo();
            info.instanceCount = _ObjectPooler.defaultCount;
            _ObjectPooler.Prefabs.Add(info);
        }

    }

}