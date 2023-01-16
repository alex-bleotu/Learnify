using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtonHandler : MonoBehaviour
{   

    public void MuteButton(User user, Slider audioSlider) {
        bool currentStateOfButton = user.GetSound();
        user.SetSound(!currentStateOfButton);

        Image buttonImage = GameObject.Find("SoundButton").transform.GetChild(0).GetComponent<Image>();
        buttonImage.sprite = !currentStateOfButton ? 
            Resources.Load<Sprite>("Icons/sound") : 
            Resources.Load<Sprite>("Icons/mute");

        if (currentStateOfButton == true)
            audioSlider.value = audioSlider.minValue;
        else
            audioSlider.value = audioSlider.maxValue;
    }
}
