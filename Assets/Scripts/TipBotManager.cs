using UnityEngine;

public class TipBotManager : MonoBehaviour
{
    [Header("Audio Setup")]
    public AudioSource botAudioSource;
    public GameObject audioWavesSprite;

    [Tooltip("Drag all audio clips here in order.")]
    public AudioClip[] scenarioTips = new AudioClip[15];
    public AudioClip winClip;
    public AudioClip loseClip;

    void Update()
    {
        if (audioWavesSprite != null && botAudioSource != null)
        {
            audioWavesSprite.SetActive(botAudioSource.isPlaying);
        }
    }
    public void PlayTip(int dayIndex, int scenarioIndex)
    {
        botAudioSource.Stop();

        int absoluteIndex = (dayIndex * 3) + scenarioIndex;

        if (absoluteIndex < scenarioTips.Length && scenarioTips[absoluteIndex] != null)
        {
            botAudioSource.clip = scenarioTips[absoluteIndex];
            botAudioSource.Play();
        }
    }
    public void PlayEmergencyOutcome(bool playerWon)
    {
        botAudioSource.Stop();

        if (playerWon && winClip != null)
        {
            botAudioSource.clip = winClip;
        }
        else if (!playerWon && loseClip != null)
        {
            botAudioSource.clip = loseClip;
        }

        botAudioSource.Play();
    }
}