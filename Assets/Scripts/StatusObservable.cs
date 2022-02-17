using UnityEngine;

namespace Game
{
    public class StatusObservable : MonoBehaviour
    {
        [Header("Modifiers")]
        [Range(1f, 30f)] public float renderDistance = 10f;
        [Range(-10f, 10f)] public float xOffset = 0f;
        [Range(-10f, 10f)] public float yOffset = 0f;
        [Range(1f, 20f)] public float statusSize = 1f;
        public string prefix;
    }
}
