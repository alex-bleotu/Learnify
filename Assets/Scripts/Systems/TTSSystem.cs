using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadSpeaker;

public class TTSSystem : MonoBehaviour
{
    public TTSSpeaker speaker;
    public AudioSource audioSource;

    public int volume = 50;

    private float[] audioData;

    public int speed = 90;
    public int pitch = 80;
    public int sampleRate = 15000;
    public int commaPause = 10;

    public struct Voice {
        public string name;
        public int speed;
        public int pitch;
        public int sampleRate;

        public Voice(string name, int speed, int pitch, int sampleRate) {
            this.name = name;
            this.speed = speed;
            this.pitch = pitch;
            this.sampleRate = sampleRate;
        }
    }

    public Voice Julie = new Voice("julie", 90, 80, 15000);
    public Voice Jack = new Voice("julie", 110, 70, 12000);
    
    public void OnTTSButton() {
        ReadText(Jack);
    }

    private void ReadText(Voice voice) {
        try {
            TTS.Init();

            TTSConverter converter = new TTSConverter();
            TTSEngine engine = TTS.GetEngine(voice.name, "n16");

            converter.Engine = engine;

            converter.Speed = voice.speed;
            converter.Pitch = voice.pitch;

            if (TemporaryData.user.GetMute())
                converter.Volume = 0;
            else
                converter.Volume = (int)(TemporaryData.user.GetVolume()) * 10;

            converter.Pause = 10;
            converter.CommaPause = commaPause;

            // converter.EmphasisFactor = 0;
            converter.TextType = TextType.Normal;
            converter.IsAsync = false;

            converter.Text = "Hello World, Antonio!";

            converter.ConvertToBuffer();

            audioData = converter.GetAudioData();

            Debug.Log(audioData);

            PlayAudioData(voice.sampleRate);
        } catch (System.Exception e) {
            Debug.LogError(e.Message);
        }
    }

    private void PlayAudioData(int sampleRate)
    {
        if (audioData == null || audioData.Length == 0)
        {
            Debug.LogError("No audio data provided");
            return;
        }

        AudioClip audioClip = AudioClip.Create("GeneratedAudio", audioData.Length, 1, sampleRate, false);

        audioClip.SetData(audioData, 0);

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
