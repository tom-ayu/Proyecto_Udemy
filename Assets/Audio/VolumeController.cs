using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;

    void Start()
    {
        float currentVolume;
        if (mixer.GetFloat("VolumeMaster", out currentVolume))
        {
            volumeSlider.value = Mathf.Pow(10f, currentVolume / 20f);
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        if (value <= 0.0001f)
            value = 0.0001f;

        float volumenDB = Mathf.Log10(value) * 20f;
        mixer.SetFloat("VolumeMaster", volumenDB);
    }
}