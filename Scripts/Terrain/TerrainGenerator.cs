using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[System.Serializable]
public struct TerrainType
{
    public string Name;
    public float Height;
    public Color Color;
}

public enum DrawMode
{
    NoiseMap,
    ColorMap,
    Mesh
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TerrainGenerator : MonoBehaviour
{
    const int mapChunkSize = 241;

    [Range(0,6)]
    public int LOD;

    [Range(0,16)]
    public int Octaves = 4;
    [Range(0, 1)]
    public float Persistance;
    [Range(0f, 25f)]
    public float Lacunarity;
    public float NoiseScale = 5.0f;
    public int Seed;
    public float HeightMultiplier = 2.0f;
    public AnimationCurve HeightCurve;

    public DrawMode drawMode;

    public TerrainType[] Regions;

    public bool AutoUpdate;

    public void Generate()
    {
        Noise noise = new Noise();
        float[,] noise_map = noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, Seed, Octaves, Persistance, Lacunarity, NoiseScale);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentmapChunkSize = noise_map[x, y];

                for (int i = 0; i < Regions.Length; i++)
                {
                    if(currentmapChunkSize <= Regions[i].Height)
                    {
                        colorMap[y * mapChunkSize + x] = Regions[i].Color;
                        break;
                    }
                }
            }
        }

        TerrainDisplay map_display = FindObjectOfType<TerrainDisplay>();

        if (drawMode == DrawMode.NoiseMap)
            map_display.DrawTexture(TextureGenerator.TextureFromNoiseMap(noise_map));
        else if (drawMode == DrawMode.ColorMap)
            map_display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        else if (drawMode == DrawMode.Mesh)
            map_display.DrawMesh(TerrainMesh.GenerateTerrainMeshData(noise_map, HeightMultiplier, HeightCurve, LOD), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
    }
}

