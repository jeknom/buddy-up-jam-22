using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform doorBody;
    [SerializeField] Transform openPosition;
    [SerializeField] Transform closePosition;
    [SerializeField] float speed = 0.1f;

    bool isOpen;

    public void Open() => this.isOpen = true;

    void Update()
    {
        var currentPosition = this.doorBody.position;
        if (isOpen && currentPosition != openPosition.position)
        {
            this.doorBody.position = Vector2.MoveTowards(currentPosition, (Vector2)openPosition.position, this.speed);
        }
        else if (!isOpen && currentPosition != closePosition.position)
        {
            this.doorBody.position = Vector2.MoveTowards(currentPosition, (Vector2)closePosition.position, this.speed);
        }
    }
}
