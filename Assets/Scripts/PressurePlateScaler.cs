using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(WeightTrigger))]
    public class PressurePlateScaler : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] Transform pressurePlateParent;

        [Header("Modifiers")]
        [SerializeField, Range(0.1f, 1f)] float minimizedScale;
        [SerializeField, Range(0.1f, 5f)] float pressSpeed;

        WeightTrigger trigger;
        bool isPressed;
        Vector3 originalScale;

        void Awake()
        {
            this.trigger = this.GetComponent<WeightTrigger>();
            this.trigger.onEnoughDungBallMass.AddListener(() => this.isPressed = true);
            this.trigger.onBallExit.AddListener(() => this.isPressed = false);
            this.originalScale = this.pressurePlateParent.localScale;
        }

        void Update()
        {
            var parentLocalScale = this.pressurePlateParent.localScale;
            if (isPressed && parentLocalScale.y > this.minimizedScale)
            {
                this.pressurePlateParent.localScale -= new Vector3(0f, this.pressSpeed * Time.deltaTime, 0f);
            }
            else if (!isPressed && parentLocalScale.y < this.originalScale.y)
            {
                this.pressurePlateParent.localScale += new Vector3(0f, this.pressSpeed * Time.deltaTime, 0f);
            }
        }
    }
}
