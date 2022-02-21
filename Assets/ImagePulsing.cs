using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePulsing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i > 5; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 2, Time.deltaTime * 10);
        }

        for (int i = 0; i > 5; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale / 2, Time.deltaTime * 10);
        }

    }
}
