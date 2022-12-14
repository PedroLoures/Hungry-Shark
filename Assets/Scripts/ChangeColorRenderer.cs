using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorRenderer : MonoBehaviour
{
    public List<MeshRenderer> renderers;

    public Color currentColor;
    [HideInInspector]
    public Material currentMaterial;

    public void Awake()
    {
        ChangeColor(currentColor);
    }

    public GameObject ChangeColor(Color newColor)
    {
        currentColor = newColor;
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = newColor;
        }

        return gameObject;
    }

    public GameObject ChangeMaterial(Material newMaterial)
    {
        currentMaterial = newMaterial;
        foreach (Renderer renderer in renderers)
        {
            renderer.material = newMaterial;
        }

        return gameObject;
    }
}
