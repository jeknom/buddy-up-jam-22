using UnityEngine.Audio;
using UnityEngine;

public class MixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    public void SetEQ(float gain)
    {
        masterMixer.SetFloat("FrequencyGain", gain);
    }

}
