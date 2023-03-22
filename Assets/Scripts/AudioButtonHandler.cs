using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonHandler : MonoBehaviour
{   
    public void MuteButton(User user, Slider audioSlider) {
        user.SetMute(!user.GetMute());

        Image buttonImage = GameObject.Find("SoundButton").transform.GetChild(0).GetComponent<Image>();
        buttonImage.sprite = !user.GetMute() ? 
            Resources.Load<Sprite>("Icons/sound") : 
            Resources.Load<Sprite>("Icons/mute");

        if (user.GetMute() == true)
            audioSlider.value = audioSlider.minValue;
        else
            audioSlider.value = user.GetSound();
    }
}
