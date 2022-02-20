using UnityEngine.Audio;
using UnityEngine;

public class MixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    public void SetEQ(float gain)
    {
        masterMixer.SetFloat("FrequencyGain", gain);
    }

    public void SetVolume(string whichSound, float volume)
    {
        string floatToSet = whichSound + "Volume";
        masterMixer.SetFloat(floatToSet, volume);
    }

}
