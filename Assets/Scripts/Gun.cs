
using System;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour  
{
    public float damage = 5f;
    public float range = 100f;
    public GameObject impactEffect;
    //public GameObject shootingParticle;

    public ParticleSystem muzzleFlash;
    public float fireRate = 7f;

    private float nextTimeToFire = 0f;
    public Camera fpsCam;

    public int bulletMax = 10;
    public int bulletCount;

    public Canvas ammoVisual;
    public float reloadTime = 2.5f;
    public Boolean reloading;
    public float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        bulletCount = bulletMax;
        reloading = false;
    }

    private void Awake()
    {
        bulletCount = bulletMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
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
        if (!reloading && Input.GetKeyDown("r") && bulletCount != bulletMax)
        {
            startReloading(2.5f);
        }
        if (!reloading)
        {
            transform.parent.Find("Shotgun").GetComponent<AudioSource>().Stop();
        }
    }
    void startReloading(float time)
    {
        transform.GetComponents<AudioSource>()[0].Play();
        timeLeft = time;
        reloading = true;

    }

    void Shoot()
    {
        if(bulletCount > 0 && !reloading)
        {
            bulletCount -= 1;
            setAmmo();

            muzzleFlash.Play();
            AudioSource.PlayClipAtPoint(transform.GetComponents<AudioSource>()[1].clip, transform.position, 0.2f);

            RaycastHit hit;
            //GameObject shootEffect = Instantiate(shootingParticle, shootingLocation.transform.position, shootingLocation.transform.rotation);
            //Destroy(shootEffect, 1f);
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage((int)Math.Round(damage));
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
        else if (!reloading)
        {
            Debug.Log("Reloading!");
            startReloading(2.5f);
        }


    }

    void setAmmo()
    {
        ammoVisual.GetComponent<TextMeshProUGUI>().SetText(bulletCount + "/" + bulletMax);
    }
}
