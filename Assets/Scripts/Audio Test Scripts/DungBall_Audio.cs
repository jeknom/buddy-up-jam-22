using UnityEngine;
using System.Collections.Generic;

public class DungBall_Audio : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private float minPitchVel = 1;
    [SerializeField] [Range(0, 10)] private float maxPitchVel = 5;
    [SerializeField] [Range(0, 3)] private float minPitch = 0.7f;
    [SerializeField] [Range(0, 3)] private float maxPitch = 1.3f;
    [SerializeField] [Range(0, 3)] private float minVolumeVel = 0.1f;
    [SerializeField] [Range(0, 3)] private float maxVolumeVel = 1.0f;
    [SerializeField] [Range(0, 1)] private float maxVolume = 1.0f;
    [SerializeField] private GameObject goAudioManager; 
    private Rigidbody2D rb2d;
    private AudioManager AudioManager; 
    
    void Awake()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
        AudioManager = goAudioManager.GetComponent<AudioManager>();        
    }

    private void Start()
    {
        AudioManager.Play("BallRolling"); 
    }


    private void Update()
    {
        float currentVelocity = Mathf.Abs(rb2d.velocity.x);

        float tPitch = Mathf.InverseLerp(minPitchVel, maxPitchVel, currentVelocity);        
        float rollingPitch = Mathf.Lerp(minPitch, maxPitch, tPitch);
        AudioManager.SetPitch("BallRolling", rollingPitch);

        
        float tVolume = Mathf.InverseLerp(minVolumeVel, maxVolumeVel, currentVelocity);
        float rollingVolume = Mathf.Lerp(0, maxVolume, tVolume);
        AudioManager.SetVolume("BallRolling", rollingVolume);

        //Debug.Log("velocity = " + rb2d.velocity.x);     
                                                        
    }   
  
}
