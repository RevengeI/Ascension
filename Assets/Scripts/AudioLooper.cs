using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLooper : MonoBehaviour
{
    [SerializeField] private double loopStartTime;
    [SerializeField] private double loopEndTime;

    private int loopStartSamples;
    private int loopEndSamples;
    private int loopLengthSamples;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        loopStartSamples = (int)(loopStartTime * audioSource.clip.frequency);
        loopEndSamples = (int)(loopEndTime * audioSource.clip.frequency);
        loopLengthSamples = loopEndSamples - loopStartSamples;
    }

    private void Update()
    {
        if (audioSource.timeSamples >= loopEndSamples) { audioSource.timeSamples -= loopLengthSamples; }
    }
}