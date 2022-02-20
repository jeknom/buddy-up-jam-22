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
        transform.position = new Vector3(dungBall.position.x, dungBall.position.y - dungBall.localScale.y / 2, 0f);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void FixedUpdate()
    {
        
        

        ParticleSystem ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startSpeed = dungBall.localScale.y;
        main.startLifetime = dungBall.localScale.y *1.5f;
        main.startSize = dungBall.localScale.y / 15;


        if (temp.x > dungBall.position.x+0.1f )
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, dungBall.transform.rotation.z * -1.0f);
            transform.position = new Vector3(dungBall.position.x+0.2f, dungBall.position.y - dungBall.localScale.y / 2, 0f);
            dung.Play();
            temp = dungBall.position;
        }
        else if (temp.x < dungBall.position.x - 0.1f)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180f, dungBall.transform.rotation.z * -1.0f);
            transform.position = new Vector3(dungBall.position.x - 0.2f, dungBall.position.y - dungBall.localScale.y / 2, 0f);
            dung.Play();
            temp = dungBall.position;
        }
        else
        {
            
            dung.Stop();
        }


      
    }









}
