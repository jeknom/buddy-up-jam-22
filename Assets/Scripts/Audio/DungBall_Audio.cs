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
    [SerializeField] [Range(0, 1)] private float maxVolume = 1.0f;
    [SerializeField] [Range(0.05f, 3)] private float minEQGain = 0.4f;
    [SerializeField] [Range(0.05f, 3)] private float maxEQGain = 2f;
    [SerializeField] [Range(0.1f, 3)] private float minEQScale = 0.9f;
    [SerializeField] [Range(0.1f, 3)] private float maxEQScale = 3f;
    [SerializeField] private GameObject goAudioManager; 
    private Rigidbody2D rb2d;
    private AudioManager AudioManager;
    private MixerManager MixerManager;
    private float currentVelocity;
    private string currentCollision;
    private string currentRollingSound = "BallRolling";
    private const float muteVolume = -80.0f; 


    void Awake()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
        AudioManager = goAudioManager.GetComponent<AudioManager>();
        MixerManager = goAudioManager.GetComponent<MixerManager>();
    }

    private void Start()
    {
        //We need to play all the rolling sounds at the same time on loop and mute them because otherwise blank noise happens when playing/stopping a looping sound for another       
        MuteAllRollingSounds();
        PlayAllRollingSounds();
        currentRollingSound = "BallRolling";
    }


    private void Update()
    {
        currentVelocity = Mathf.Abs(rb2d.velocity.x);

        SetPitchWithVelocity();
        SetVolumeWithVelocity();
        SetEQWithSize();
    }

    //Plays the appropriate sound depending on the ground type
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        string collisionTag = collision.gameObject.tag;
        if (collisionTag != currentCollision)
        {
            switch (collisionTag)
            {
                case "Grower":
                    MixerManager.SetVolume(currentRollingSound, muteVolume);
                    MixerManager.SetVolume("BallRollingMud", 0);
                    currentCollision = collisionTag;
                    currentRollingSound = "BallRollingMud";
                    break;

                case "Shrinker":
                    MixerManager.SetVolume(currentRollingSound, muteVolume);
                    MixerManager.SetVolume("BallRollingGrass", 0);
                    currentCollision = collisionTag;
                    currentRollingSound = "BallRollingGrass";
                    break;

                default:
                    break;
            }            
        }
        
    }

    //Stops making sounds if the ball doesn't touch anything
    private void OnCollisionExit2D(Collision2D collision)
    {
        MuteAllRollingSounds();
    }

    private void SetPitchWithVelocity()
    {
        if (currentRollingSound != "BallRollingGrass")
        {
            float t = Mathf.InverseLerp(minPitchVel, maxPitchVel, currentVelocity);
            float rollingPitch = Mathf.Lerp(minPitch, maxPitch, t);
            AudioManager.SetPitch(currentRollingSound, rollingPitch);
        }
       
    }

    private void SetVolumeWithVelocity()
    {
        float t = Mathf.InverseLerp(minVolumeVel, maxVolumeVel, currentVelocity);
        float rollingVolume = Mathf.Lerp(0, maxVolume, t);
        AudioManager.SetVolume(currentRollingSound, rollingVolume);
    }

    private void SetEQWithSize()
    {
        float currentScale = this.transform.localScale.x;
        float t = Mathf.InverseLerp(minEQScale, maxEQScale, currentScale);
        float eqGain = Mathf.Lerp(minEQGain, maxEQGain, t);
        MixerManager.SetEQ(eqGain);
        //Debug.Log("scale = " + this.transform.localScale.x + "eqGain = " + eqGain + "t = " + t);
    }

    private void MuteAllRollingSounds()
    {
        MixerManager.SetVolume("BallRolling", muteVolume);
        MixerManager.SetVolume("BallRollingMud", muteVolume);
        MixerManager.SetVolume("BallRollingGrass", muteVolume);
    }

    private void PlayAllRollingSounds()
    {
        AudioManager.Play("BallRolling");
        AudioManager.Play("BallRollingMud");
        AudioManager.Play("BallRollingGrass");
    }
}
