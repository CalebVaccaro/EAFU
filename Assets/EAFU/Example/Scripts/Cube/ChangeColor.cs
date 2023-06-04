using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Color color; // Color to be applied
    [SerializeField] private Material material; // Reference to the material
    [SerializeField] private ParticleSystem[] childParticleSystems; // Array of child particle systems

    public Color Color
    {
        get => color;
        set => color = value;
    }

    private void Start()
    {
        // Get the material attached to the current object
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }

        // Get all child particle systems
        childParticleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void ChangeCubeColor()
    {
        // Change the EmissionColor of the material
        material.SetColor("_EmissionColor", color);

        // Loop through child particle systems
        foreach (var particleSystem in childParticleSystems)
        {
            // Set the trail color of each particle system
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        }
    }
}
