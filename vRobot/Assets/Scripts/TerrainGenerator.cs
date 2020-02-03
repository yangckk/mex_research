using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TerrainGenerator : MonoBehaviour
{
    public int height;
    public int width;
    public int depth;

    public float scale;

    [Range(0f, 1f)]
    public float friction;
    
    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        terrain.transform.position = new Vector3(-width / 2, -terrain.terrainData.bounds.max.y, -height / 2);

        TerrainCollider collider = GetComponent<TerrainCollider>();
        collider.material.dynamicFriction = friction;
        collider.material.staticFriction = friction;
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width+1, height+1];
        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    private float CalculateHeight(int x, int y)
    {
        float x_coord = (float)x / width * scale;
        float y_coord = (float)y / height * scale;

        return Mathf.PerlinNoise(x_coord, y_coord);
    }
}
