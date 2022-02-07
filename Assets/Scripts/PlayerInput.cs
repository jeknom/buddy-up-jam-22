using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementTemplates.Scripts;

[RequireComponent(typeof(Movement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] KeyCode jumpKey;
    Movement movementBehaviour;

    void Awake()
    {
        this.movementBehaviour = this.GetComponent<Movement>();
    }

    void Update()
    {
        var horizontalAxis = Input.GetAxisRaw("Horizontal");
        var direction = horizontalAxis > 0 ?
            Movement.Direction.Right :
            horizontalAxis < 0 ?
            Movement.Direction.Left :
            Movement.Direction.None;
        var isJumping = Input.GetKeyDown(this.jumpKey);

        this.movementBehaviour.Move(direction, isJumping, false);
    }
}
