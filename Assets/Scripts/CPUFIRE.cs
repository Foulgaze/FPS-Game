
using System;
using UnityEngine;

public class CPUFIRE : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject impactEffect;
    public GameObject shootingParticle;
    public GameObject shootingLocation;
    public float fireRate = 15f;

    private float nextTimeToFire = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        GameObject shootEffect = Instantiate(shootingParticle, shootingLocation.transform.position, shootingLocation.transform.rotation);
        Destroy(shootEffect, 1f);
        if (Physics.Raycast(shootingLocation.transform.position, shootingLocation.transform.forward, out hit, range))
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
}
