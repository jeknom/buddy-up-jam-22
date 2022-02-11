using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Openable : MonoBehaviour
    {
        public void Open() => this.gameObject.SetActive(false);
        public void Close() => this.gameObject.SetActive(true);
    }
}
