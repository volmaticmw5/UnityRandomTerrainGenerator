using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TerrainDisplay : MonoBehaviour
{
    public Renderer TextureRenderer;
    public MeshFilter MeshFilter;
    public MeshRenderer MeshRenderer;

    public void DrawTexture(Texture2D texture)
    {
        TextureRenderer.sharedMaterial.mainTexture = texture;
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        MeshFilter.sharedMesh = meshData.CreateMesh();
        MeshRenderer.sharedMaterial.mainTexture = texture;
    }
}
