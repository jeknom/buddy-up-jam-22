using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(DistanceJoint2D))]
    public class Openable : MonoBehaviour
    {
        [SerializeField, Range(1f, 10f)] float moveSpeed;

        bool isOpen;
        float maxDistance;
        DistanceJoint2D distanceJoint2D;

        public void Open() => this.isOpen = true;
        public void Close() => this.isOpen = false;

        void Awake()
        {
            this.distanceJoint2D = this.GetComponent<DistanceJoint2D>();
            this.maxDistance = this.distanceJoint2D.distance;
        }

        void Update()
        {

            if (isOpen && distanceJoint2D.distance > 2f)
            {
                this.distanceJoint2D.distance -= (this.moveSpeed * Time.deltaTime);
            }
            else if (!isOpen && distanceJoint2D.distance < this.maxDistance)
            {
                this.distanceJoint2D.distance += (this.moveSpeed * Time.deltaTime);
            }
        }
    }
}
