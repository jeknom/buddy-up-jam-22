using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LineUpdater : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] Transform postitionOne;
        [SerializeField] Transform positionTwo;

        void Update()
        {
            this.lineRenderer.SetPositions(new Vector3[] { postitionOne.position, positionTwo.position });
        }
    }
}
