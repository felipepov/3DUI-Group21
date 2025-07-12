using UnityEngine;

public class LightPulse : MonoBehaviour
{
    private Light pointLight;
    private float baseIntensity;
    public float pulseSpeed = 2f;
    public float pulseAmount = 2f;

    void Start()
    {
        pointLight = GetComponent<Light>();
        baseIntensity = pointLight.intensity;
    }

    void Update()
    {
        pointLight.intensity = baseIntensity + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
    }
}
