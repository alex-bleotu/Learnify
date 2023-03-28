using System;
using GoogleTextToSpeech.Scripts.Data;
using TMPro;
using UnityEngine;
using GoogleTextToSpeech.Scripts;

public class TTSHandler : MonoBehaviour
{
    public VoiceScriptableObject voice;
    public TextToSpeech textToSpeech;
    public AudioSource audioSource;

    private Action<AudioClip> audioClipReceived;
    private Action<BadRequestData> errorReceived;

    public void PlayTTS(string text)
    {
        errorReceived += ErrorReceived;
        audioClipReceived += AudioClipReceived;
        textToSpeech.GetSpeechAudioFromGoogle(text, voice, audioClipReceived, errorReceived);
    }

    public void StopTTS()
    {
        audioSource.Stop();
    }

    private void ErrorReceived(BadRequestData badRequestData)
    {
        Debug.Log($"Error {badRequestData.error.code} : {badRequestData.error.message}");
    }

    private void AudioClipReceived(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        if (TemporaryData.user.GetMute())
            audioSource.volume = 0;
        else
            audioSource.volume = TemporaryData.user.GetVolume();
        audioSource.Play();
    }
}
