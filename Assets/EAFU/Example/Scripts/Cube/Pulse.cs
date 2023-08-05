using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField] private bool isPulsing = true;
    [SerializeField] private float pulseSpeed = 1f;
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 1.5f;

    [SerializeField] private float scale = 1f;
    [SerializeField] private float timeOffset;

    [SerializeField] private float pulseSpeedIncreaseRate = 0.1f;
    [SerializeField] private float scaleIncreaseRate = 0.1f;

    [SerializeField] private List<PulseVariables> pulses;
    [SerializeField] private ParticleSystem system;

    [SerializeField] private int listCount = 0;
    private int currentIndex = 0;

    private void Start()
    {
        timeOffset = Random.Range(0f, Mathf.PI * 2f); // Randomize the starting point of the pulse

        pulses = new List<PulseVariables>();

        // Example of initializing the pulseList with initial values
        pulses.Add(new PulseVariables() { pulseSpeed = 1f, minScale = 100, maxScale = 120 });

        // Example of adding more PulseVariables with increasing values
        for (int i = 1; i <= listCount; i++)
        {
            float increasedPulseSpeed = pulses[i - 1].pulseSpeed + pulseSpeedIncreaseRate;
            float increasedMinScale = pulses[i - 1].minScale;
            float increasedMaxScale = pulses[i - 1].maxScale + scaleIncreaseRate;

            pulses.Add(new PulseVariables() { pulseSpeed = increasedPulseSpeed, minScale = increasedMinScale, maxScale = increasedMaxScale });
        }
    }

    private void Update()
    {
        if (isPulsing)
        {
            Pulsate();
        }
    }

    private void Pulsate()
    {
        // Calculate the scale factor based on time and pulse speed
        float pulseFactor = Mathf.Sin((Time.time + timeOffset) * pulseSpeed);
        scale = Mathf.Lerp(minScale, maxScale, (pulseFactor + 1f) / 2f);

        transform.localScale = Vector3.one * scale;
    }

    public void SetPulseVariables(PulseVariables variables)
    {
        pulseSpeed = variables.pulseSpeed;
        minScale = variables.minScale;
        maxScale = variables.maxScale;
    }

    public void IncreasePulse()
    {
        EnablePulseVFX();

        currentIndex = (currentIndex + 1) % pulses.Count;
        SetPulseVariables(pulses[currentIndex]);
    }

    public void EnablePulseVFX()
    {
        system.Play(true);
    }

    public void DisablePulseVFX()
    {
        if (system.isPlaying)
        {
            system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}

[System.Serializable]
public class PulseVariables
{
    public float pulseSpeed;
    public float minScale;
    public float maxScale;
}
