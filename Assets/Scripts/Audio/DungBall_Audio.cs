using UnityEngine;
using System.Collections.Generic;

public class DungBall_Audio : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private float minPitchVel = 1;
    [SerializeField] [Range(0, 10)] private float maxPitchVel = 5;
    [SerializeField] [Range(0, 3)] private float minPitch = 0.5f;
    [SerializeField] [Range(0, 3)] private float maxPitch = 1.5f;
    [SerializeField] [Range(0, 3)] private float minVolumeVel = 0.3f;
    [SerializeField] [Range(0, 3)] private float maxVolumeVel = 1.0f;
    [SerializeField] [Range(0, 1)] private float maxVolume = 0.5f;
    [SerializeField] [Range(0.05f, 3)] private float minEQGain = 0.4f;
    [SerializeField] [Range(0.05f, 3)] private float maxEQGain = 1.5f;
    [SerializeField] [Range(0.1f, 3)] private float minEQScale = 0.9f;
    [SerializeField] [Range(0.1f, 3)] private float maxEQScale = 3f;
    [SerializeField] private GameObject goAudioManager; 
    private Rigidbody2D rb2d;
    private AudioManager AudioManager;
    private MixerManager MixerManager;
    private float currentVelocity;


    void Awake()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
        AudioManager = goAudioManager.GetComponent<AudioManager>();
        MixerManager = goAudioManager.GetComponent<MixerManager>();
    }

    private void Start()
    {
        AudioManager.Play("BallRolling"); 
    }


    private void Update()
    {
        currentVelocity = Mathf.Abs(rb2d.velocity.x);

        SetPitchWithVelocity();
        SetVolumeWithVelocity();
        SetEQWithSize();
    }   
  
    private void SetPitchWithVelocity()
    {
        float t = Mathf.InverseLerp(minPitchVel, maxPitchVel, currentVelocity);
        float rollingPitch = Mathf.Lerp(minPitch, maxPitch, t);
        AudioManager.SetPitch("BallRolling", rollingPitch);
    }

    private void SetVolumeWithVelocity()
    {
        float t = Mathf.InverseLerp(minVolumeVel, maxVolumeVel, currentVelocity);
        float rollingVolume = Mathf.Lerp(0, maxVolume, t);
        AudioManager.SetVolume("BallRolling", rollingVolume);
    }

    private void SetEQWithSize()
    {
        float currentScale = this.transform.localScale.x;
        float t = Mathf.InverseLerp(minEQScale, maxEQScale, currentScale);
        float eqGain = Mathf.Lerp(minEQGain, maxEQGain, t);
        MixerManager.SetEQ(eqGain);
        //Debug.Log("scale = " + this.transform.localScale.x + "eqGain = " + eqGain + "t = " + t);
    }
}
