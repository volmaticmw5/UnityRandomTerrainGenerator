using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(TerrainGenerator)), CanEditMultipleObjects]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGenerator generator = (TerrainGenerator)target;
        DrawDefaultInspector();

        if (DrawDefaultInspector())
        {
            if (generator.AutoUpdate)
                generator.Generate();
        }

        if(GUILayout.Button("Generate"))
        {
            generator.Generate();
        }
    }
}
