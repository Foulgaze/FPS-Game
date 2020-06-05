using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Sniper : MonoBehaviour
{
    Camera cam;
    public float zoomAmount = 10f;
    Boolean zoomed = false;
    public GameObject cross;
    private float nextTimeToFire = 0f;
    public float fireRate = 15f;
    public float range = 100f;
    LineRenderer lRender;
    float distance;
    public int bulletMax = 1;
    public int bulletCount;
    public float reloadTime = 2.5f;
    public Boolean reloading;
    public float timeLeft;
    AudioSource audio;
    public Canvas ammoVisual;
    // Start is called before the first frame update
    void Start()
    {

        cam = gameObject.transform.parent.GetComponent<Camera>();
        cross.SetActive(false);
        lRender = gameObject.transform.parent.parent.GetComponent<LineRenderer>();
        //lRender.SetColors(Color.red, Color.red);
        //lRender.SetWidth(.2f, .2f);
        lRender.startColor = Color.red;
        lRender.endColor = Color.red;
        lRender.startWidth = .2f;
        lRender.endWidth = .2f;
        bulletCount = bulletMax;
        reloading = false;
        audio = gameObject.GetComponent<AudioSource>();
        Debug.Log("Start");


    }

    private void Awake()
    {
        bulletCount = bulletMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !zoomed)
        {
            cam.fieldOfView -= zoomAmount;
            cross.SetActive(true);
            zoomed = true;
        }
        if(Input.GetMouseButtonUp(1) && zoomed)
        {
            cam.fieldOfView += zoomAmount;
            cross.SetActive(false);
            zoomed = false;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {

            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        if (reloading)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft < 0 && reloading)
        {
            Debug.Log("reloaded");
            reloading = false;
            bulletCount = bulletMax;
            setAmmo();

        }
        if(!reloading && Input.GetKeyDown("r") && bulletCount != bulletMax)
        {
            
            startReloading(2.5f);
        }
    }
    void Shoot()
    {
        if (bulletCount > 0 && !reloading)
        {
            Debug.Log(bulletCount);
            bulletCount -= 1;
            setAmmo();
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                
                lRender.SetVertexCount(2);
                GameObject barrelEnd = gameObject.transform.GetChild(0).gameObject;
                lRender.SetPosition(0, barrelEnd.transform.position);
                lRender.SetPosition(1, hit.point);
                AudioSource.PlayClipAtPoint(audio.clip, barrelEnd.transform.position, 10);
                gameObject.transform.parent.parent.GetComponent<PlayerMovement>().removeLine();
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage((int)100);
                }

            }
            else
            {
                /**lRender.SetVertexCount(2);
                GameObject barrelEnd = gameObject.transform.GetChild(0).gameObject;
                lRender.SetPosition(0, barrelEnd.transform.position);
                lRender.SetPosition(1, barrelEnd.transform.forward*100);
                StartCoroutine(lineDelete(lRender));**/
            }
        }
        else if(!reloading)
        {
            Debug.Log("Reloading!");
            startReloading(2.5f);
        }

    }
    void startReloading(float time)
    {
        gameObject.GetComponents<AudioSource>()[1].Play();
        timeLeft = time;
        reloading = true;
        
    }

    void setAmmo()
    {
        ammoVisual.GetComponent<TextMeshProUGUI>().SetText(bulletCount + "/" + bulletMax);
    }
    
}
