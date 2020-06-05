using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    Camera cam;
    public float zoomAmount = 10f;
    public float nextTimeToFire = 1f;
    public float fireRateTimer = 0;
    public float fireRate = 15f;
    public float range = 100f;


    public int bulletMax = 4;
    [System.NonSerialized]
    public int bulletCount;
    public float reloadTime = 2.5f;

    [System.NonSerialized]
    public Boolean reloading;
    float timeLeft;
    AudioSource audio;
    public Canvas ammoVisual;
    Boolean fired = false;

    AudioSource reloadSound;


    public int pelletCount = 5;
    public float spreadAngle = 15;
    public GameObject pellet;
    public Transform barrelExit;
    List<Quaternion> pellets;
    public float pelletFireVel = 2600;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.transform.parent.GetComponent<Camera>();
        bulletCount = bulletMax;
        reloading = false;
        reloadSound = gameObject.transform.GetComponent<AudioSource>();
        transform.GetComponents<AudioSource>()[0].volume = .07f;

    }

    private void Awake()
    {
        bulletCount = bulletMax;
        pellets = new List<Quaternion>(pelletCount);
        for(int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (reloading)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft < 0 && reloading)
        {
            reloading = false;
            bulletCount = bulletMax;
            setAmmo();

        }
        if (!reloading && Input.GetKeyDown("r") && bulletCount != bulletMax)
        {
            startReloading(2.5f);
        }

        if (Input.GetButton("Fire1") && !reloading && !fired)
        {
            if(bulletCount > 0)
            {
                transform.GetComponents<AudioSource>()[1].Play();
                fired = true;
                bulletCount -= 1;
                setAmmo();
                shoot();
            } else if (!reloading)
            {
                startReloading(2.5f);
            }
        }
        if (fired)
        {
            fireRateTimer += Time.deltaTime;
        }
        if(fireRateTimer > nextTimeToFire )
        {
            fired = false;
            fireRateTimer = 0;
        }
        if (!reloading)
        {
            reloadSound.Stop();
        }
    }

    void startReloading(float time)
    {
        transform.GetComponents<AudioSource>()[0].Play();
        timeLeft = time;
        reloading = true;

    }

    void shoot()
    {
        int i = 0;
        foreach (Quaternion quat in pellets.ToList())
        {
            pellets[i] = UnityEngine.Random.rotation;
            GameObject p = Instantiate(pellet, barrelExit.position, barrelExit.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
            p.GetComponent<Rigidbody>().velocity = cam.transform.parent.GetComponent<CharacterController>().velocity;
            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * pelletFireVel);
            i++;
            Destroy(p.transform.gameObject, 2);
        }
    }

    void setAmmo()
    {
        ammoVisual.GetComponent<TextMeshProUGUI>().SetText(bulletCount + "/" + bulletMax);
    }
}
