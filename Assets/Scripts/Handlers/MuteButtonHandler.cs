using UnityEngine;
using UnityEngine.UI;

public class MuteButtonHandler : MonoBehaviour
{
    public void MuteButton()
    {
        TemporaryData.user.SetMute(!TemporaryData.user.GetMute());

        UpdateButtonIcon();
    }

    public void UpdateButtonIcon()
    {
        // Image buttonImage = GameObject.Find("SoundButton").transform.GetChild(0).GetComponent<Image>();
        // buttonImage.sprite = !TemporaryData.user.GetMute() ?
        //     Resources.Load<Sprite>("Icons/sound") :
        //     Resources.Load<Sprite>("Icons/mute");
    }
}
