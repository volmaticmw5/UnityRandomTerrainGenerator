using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Noise
{
    public float[,] GenerateNoiseMap(int width, int height, int seed, int octaves, float persistance, float lacunarity, float scale)
    {
		float[,] noiseMap = new float[width, height];

		System.Random prng = new System.Random(seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
			float offsetX = prng.Next(-100000, 100000);
			float offsetY = prng.Next(-100000, 100000);
			octaveOffsets[i] = new Vector2(offsetX, offsetY);
		}

		if (scale <= 0)
			scale = 0.0001f;

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = width / 2f;
		float halfHeight = height / 2f;

		for (int zIndex = 0; zIndex < height; zIndex++)
		{
			for (int xIndex = 0; xIndex < width; xIndex++)
			{
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
					float sampleX = (xIndex - halfWidth)/ scale * frequency + octaveOffsets[i].x;
					float sampleZ = (zIndex - halfHeight) / scale * frequency + octaveOffsets[i].y;

					float noise = Mathf.PerlinNoise(sampleX, sampleZ) * 2 - 1;
					noiseHeight += noise * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight)
					maxNoiseHeight = noiseHeight;
				else if (noiseHeight < minNoiseHeight)
					minNoiseHeight = noiseHeight;

				noiseMap[xIndex, zIndex] = noiseHeight;
			}
		}

		for (int zIndex = 0; zIndex < height; zIndex++)
		{
			for (int xIndex = 0; xIndex < width; xIndex++)
			{
				noiseMap[xIndex, zIndex] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[xIndex, zIndex]);
			}
		}

		return noiseMap;
	}
}
