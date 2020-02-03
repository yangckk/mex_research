using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PositionData", menuName ="Endpoint Position Data")]
public class EndpointPositionData : ScriptableObject
{
    public int width;
    public int height;
    public bool[] positions;
    public List<Vector2> coordinates;
}

[System.Serializable]
public class BoolArray : Object
{
    public int width;
    
    public int height;
    
    public bool[] values;

    public int GetLength(int axis)
    {
        return axis == 0 ? width : height;
    }

    public bool this[int x, int y]
    {
        get { return values[y * width + x]; }
        set { values[y * width + x] = value; }
    }

    public BoolArray(int width, int height)
    {
        this.width = width;
        this.height = height;

        values = new bool[width * height];
    }
}