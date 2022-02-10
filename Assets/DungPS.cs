using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungPS : MonoBehaviour
{

    [SerializeField] Transform dungBall;
    [SerializeField] ParticleSystem dung;
    private Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
         temp = dungBall.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     
    }

    void Update()
    {
        transform.position = new Vector3(dungBall.position.x, dungBall.position.y - dungBall.localScale.y/2, 0f) ;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, dungBall.transform.rotation.z * -1.0f);

        if (temp.x > dungBall.position.x+0.01f )
        {
            //transform.Rotate(0f, 0f, 0 - dungBall.rotation.z);
            dung.Play();
            temp = dungBall.position;
        }
        else if (temp.x < dungBall.position.x - 0.01f)
        {
           
            dung.Play();
            temp = dungBall.position;
        }
        else
        {
            
            dung.Stop();
        }

        
  
    }









}
