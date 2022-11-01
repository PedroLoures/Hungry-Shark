using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorRenderer : MonoBehaviour
{
    public List<MeshRenderer> renderers;

    public void ChangeColor(Color newColor)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = newColor;
        }
    }

    public void ChangeMaterial(Material newMaterial)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material = newMaterial;
        }
    }
}
