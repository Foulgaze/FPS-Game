using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Grenade : MonoBehaviour
{
    Camera cam ;
    float speed = 20;
    float gravity = -500;
    Rigidbody ball;
    Boolean mouseDown = false;
    Boolean timerStart = false;
    float timer = .0f;
    public float timeToExplode = 2.0f;
    public float bombVolume = 5.0f;
    public GameObject explosion;
    float grenadeRange = 20;
    public float grenadeCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.transform.parent.GetComponent<Camera>();
        ball = gameObject.transform.GetChild(0).GetComponent<Rigidbody>();
        ball.velocity = Vector3.zero;
        ball.useGravity = false;

    }

    void Launch(GrenadeCount nade, int grenadeNO)
    {
        gameObject.transform.parent = null;
        //ball.AddForce(cam.transform.forward * speed, ForceMode.VelocityChange);
        ball.AddForce(cam.transform.forward * speed, ForceMode.VelocityChange);
        //ball.AddForce(Vector3.up * gravity);
        //ball.AddForce(Vector3.down * gravity);
        if (grenadeNO == 0)
        {
            nade.changeNade();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ball.velocity.y);
       
        if (Input.GetMouseButtonDown(0))
        {
            
                mouseDown = true;
                timerStart = true;
                             
        }
        if (Input.GetMouseButtonUp(0) && mouseDown)
        {
            
            GrenadeCount anotherNade = gameObject.transform.parent.parent.GetComponent<GrenadeCount>();
            anotherNade.grenadeAmount -= 1;
            if (anotherNade.grenadeAmount > 0)
            {
                anotherNade.AddGrenade();
                
            }
            Launch(anotherNade, anotherNade.grenadeAmount);
         
            mouseDown = false;
        }
        if (timerStart)
        {
            timer += 1 * Time.deltaTime;
        }
        if(timer >= timeToExplode - 1.9)
        {
            Debug.Log("Adding Gravity");
            ball.AddForce(Vector3.up * gravity);
        }
        if(timer >= timeToExplode)
        {
            Collider[] hits = Physics.OverlapSphere(ball.position, grenadeRange);
            foreach(Collider collide in hits)
            {
                Target target = collide.gameObject.transform.GetComponent<Target>();
                if (target != null)
                {
                    float distance = Vector3.Distance(ball.position, collide.gameObject.transform.position);
                    if ( distance <= grenadeRange*.5)
                    {
                        target.TakeDamage((int)100);
                    }
                    else if (distance < grenadeRange*.7)
                    {
                        target.TakeDamage((int)75);
                    } else if (distance < grenadeRange*.8)
                    {
                        target.TakeDamage((int)50);
                    }
                    else if (distance < grenadeRange)
                    {
                        target.TakeDamage((int)25);
                    }
                   
                }
            }
            AudioSource audio = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, ball.position, bombVolume);
            GameObject gObject = Instantiate(explosion, ball.position, Quaternion.identity) as GameObject;
            Destroy(gObject, 1);
            Destroy(gameObject);
        }
       
    }



}