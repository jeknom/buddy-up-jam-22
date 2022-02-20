using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private GameObject goAudioManager;
    private AudioManager AudioManager;
    private MixerManager MixerManager;
    private BoxCollider2D playerCollider;
    private Rigidbody2D rb2D;
    private float currentVelocity;
    private bool isWalking;
    private const float muteVolume = -80.0f;
    private string currentWalkingSound;
    private bool canLand;
    private string currentCollision;
    private bool isMute;
    private float horizontalInput;
    //private float verticalInput;

    private void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        goAudioManager = GameObject.Find("AudioManager");
        AudioManager = goAudioManager.GetComponent<AudioManager>();
        MixerManager = goAudioManager.GetComponent<MixerManager>();
    }

    private void Start()
    {
        MuteAllWalkingSounds();
        PlayAllWalkingSounds();
        currentWalkingSound = "Footsteps";
        isMute = false;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");        
        if ((horizontalInput == 0 || !isGrounded()) && !isMute)
        {
            MuteAllWalkingSounds();
            isMute = true;
        }
        else if (horizontalInput != 0 && isGrounded() && isMute)
        {
            PlayWalkingSound(currentWalkingSound);
            isMute = false;
        }

        //verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        string collisionTag = collision.gameObject.tag;
        int layerTag = collision.gameObject.layer;

        switch (layerTag)
        {
            case 8:
                //PlayBounceSound("BallBouncingWater");
                AudioManager.Play("BallBouncingWater");
                break;

            case 9:
                //PlayBounceSound("BallBouncingLava");
                AudioManager.Play("BallBouncingLava");
                break;

            default:
                break;
        }
        
        if (!isGrounded())
        {
            MuteAllWalkingSounds();
            canLand = true;
        }
        else if (collisionTag != currentCollision && horizontalInput != 0) //starts the appropriate walking sound loop
        {
            switch (collisionTag)
            {
                case "Grower":
                    PlayLandingSound("BallBouncingMud");
                    PlayWalkingSound("FootstepsMud");
                    currentCollision = collisionTag;
                    currentWalkingSound = "FootstepsMud";
                    break;

                case "Shrinker":
                    PlayLandingSound("BallBouncingMud");
                    PlayWalkingSound("Footsteps");
                    currentCollision = collisionTag;
                    currentWalkingSound = "Footsteps";
                    break;

                case "Normal":
                    PlayLandingSound("BallBouncingMud");
                    PlayWalkingSound("Footsteps");
                    currentCollision = collisionTag;
                    currentWalkingSound = "Footsteps";
                    break;

                default:
                    PlayLandingSound("BallBouncing");
                    PlayWalkingSound("FootstepsGrass");
                    break;
            }
        }
    }


    private bool isGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeight, groundLayer);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(playerCollider.bounds.center, Vector2.down * (playerCollider.bounds.extents.y + extraHeight));
        return raycastHit.collider != null;
    }

    private void MuteAllWalkingSounds()
    {
        MixerManager.SetVolume("Footsteps", muteVolume);
        MixerManager.SetVolume("FootstepsGrass", muteVolume);
        MixerManager.SetVolume("FootstepsMud", muteVolume);
    }

    private void PlayAllWalkingSounds()
    {
        AudioManager.Play("Footsteps");
        AudioManager.Play("FootstepsGrass");
        AudioManager.Play("FootstepsMud");
    }

    private void PlayLandingSound(string sound)
    {
        if (canLand)
        {
            AudioManager.Play(sound);
            canLand = false;
        }
    }

    private void PlayWalkingSound(string sound)
    {
            MixerManager.SetVolume(currentWalkingSound, muteVolume);
            MixerManager.SetVolume(sound, 0);
        
    }
}
