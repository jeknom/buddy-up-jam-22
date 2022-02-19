using System.Collections;
using UnityEngine;

namespace Game
{
    public class Breakable : MonoBehaviour
    {
        public void Break() => Destroy(this.gameObject);
    }
}