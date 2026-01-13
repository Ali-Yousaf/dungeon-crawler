using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        impulseSource = GetComponent<CinemachineImpulseSource>();

        if (impulseSource == null)
            impulseSource = gameObject.AddComponent<CinemachineImpulseSource>();
    }

    public void Shake(float duration = 0.3f, float amplitude = 1f, float frequency = 1f)
    {
        impulseSource.ImpulseDefinition.TimeEnvelope.SustainTime = duration;
        impulseSource.ImpulseDefinition.AmplitudeGain = amplitude;
        impulseSource.ImpulseDefinition.FrequencyGain = frequency;

        impulseSource.GenerateImpulse();
    }
}
