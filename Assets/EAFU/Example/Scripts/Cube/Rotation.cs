using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private bool isRotating;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float xAmplitude = 1f;
    [SerializeField] private float yAmplitude = 1f;
    [SerializeField] private float xRotationOffset = 0f;
    [SerializeField] private float yRotationOffset = 0f;

    [SerializeField] private float rotationSpeedIncreaseRate = 0.1f;
    [SerializeField] private float amplitudeIncreaseRate = 0.1f;
    [SerializeField] private float offsetIncreaseRate = 1f;
    [SerializeField] private List<RotationVariables> rotations;

    [SerializeField] private int listCount = 0;
    private int currentIndex = 0;

    public void Start()
    {
        rotations = new List<RotationVariables>();

        // Example of initializing the rotationList with initial values
        rotations.Add(new RotationVariables()
        {
            rotationSpeed = 1f,
            xAmplitude = 1f,
            yAmplitude = 1f,
            xRotationOffset = 0f,
            yRotationOffset = 0f
        });

        // Example of adding more RotationVariables with increasing values
        for (int i = 1; i <= listCount; i++)
        {
            float increasedRotationSpeed = rotations[i - 1].rotationSpeed + rotationSpeedIncreaseRate;
            float increasedAmplitude = rotations[i - 1].xAmplitude + amplitudeIncreaseRate;
            float increasedOffset = rotations[i - 1].xRotationOffset + offsetIncreaseRate;

            rotations.Add(new RotationVariables()
            {
                rotationSpeed = increasedRotationSpeed,
                xAmplitude = increasedAmplitude,
                yAmplitude = increasedAmplitude,
                xRotationOffset = increasedOffset,
                yRotationOffset = increasedOffset
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        // Calculate rotation angles based on time and amplitudes
        float xRotation = Mathf.Sin(Time.time * rotationSpeed) * xAmplitude;
        float yRotation = Mathf.Cos(Time.time * rotationSpeed) * yAmplitude;

        // Apply rotation offsets
        xRotation += xRotationOffset;
        yRotation += yRotationOffset;

        // Apply rotation to the object
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    public void SetRotationVariables(RotationVariables variables)
    {
        rotationSpeed = variables.rotationSpeed;
        xAmplitude = variables.xAmplitude;
        yAmplitude = variables.yAmplitude;
        xRotationOffset = variables.xRotationOffset;
        yRotationOffset = variables.yRotationOffset;
    }


    public void IncreaseRotation()
    {
        currentIndex = (currentIndex + 1) % rotations.Count;
        SetRotationVariables(rotations[currentIndex]);
    }
}

[System.Serializable]
public class RotationVariables
{
    public float rotationSpeed;
    public float xAmplitude;
    public float yAmplitude;
    public float xRotationOffset;
    public float yRotationOffset;
}