using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(EndpointPositionData))]
public class EndpointPositionEditor : Editor
{
    int width = 1;
    int height = 1;

    EndpointPositionData endpointPositionData;
    
    void OnEnable()
    {
        endpointPositionData = (EndpointPositionData)target;
        width = endpointPositionData.width;
        height = endpointPositionData.height;
    }

    public override void OnInspectorGUI()
    {
        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);
        if (width != endpointPositionData.width || height != endpointPositionData.height)
        {
            endpointPositionData.width = width;
            endpointPositionData.height = height;
            endpointPositionData.positions = new bool[width * height];
        }
        ChangeArrayWidthAndHeight();
        if (GUILayout.Button("Apply"))
        {
            endpointPositionData.coordinates.Clear();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (endpointPositionData.positions[j*width+i] == true)
                    {
                        Debug.Log(new Vector2(i, j).ToString());
                        endpointPositionData.coordinates.Add(new Vector2(-i, j));
                    }
                }
            }
        }
        EditorUtility.SetDirty(target);
    }

    void ChangeArrayWidthAndHeight()
    {
        Debug.Log(endpointPositionData.positions.Length);
        for (int j = 0; j < height; j++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < width; i++)
            {
                endpointPositionData.positions[j*width+i] = EditorGUILayout.Toggle(endpointPositionData.positions[j*width+i]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}

