using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Openable : MonoBehaviour
    {
        [SerializeField] Rigidbody2D ballBody;
        [SerializeField, Range(1f, 10f)] float moveSpeed;
        [SerializeField] Transform openPosition;
        [SerializeField] Transform closePosition;
        [SerializeField] float speed;

        bool isOpen;
        Rigidbody2D rb2d;

        public void Open() => this.isOpen = true;
        public void Close() => this.isOpen = false;

        void Awake()
        {
            this.rb2d = this.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Debug.Log(isOpen);
            var currentPosition = this.rb2d.position;
            if (isOpen && currentPosition != (Vector2)openPosition.position)
            {
                this.rb2d.position = Vector2.MoveTowards(currentPosition, (Vector2)openPosition.position, this.speed);
            }
            else if (!isOpen && currentPosition != (Vector2)closePosition.position)
            {
                this.rb2d.position = Vector2.MoveTowards(currentPosition, (Vector2)closePosition.position, this.speed);
            }
        }
    }
}
