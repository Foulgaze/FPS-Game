using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeStation : MonoBehaviour
{
    public Boolean hasGrenade = true;
    GameObject grenade;
    float startY;
    public float gravity = -0.2f;
    // Start is called before the first frame update
    void Start()
    {
        //grenade = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        grenade = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        startY = grenade.transform.position.y;
        grenade.transform.position = new Vector3(grenade.transform.position.x, startY, grenade.transform.position.z);
        Physics.gravity = Vector3.up * gravity;

    }

    // Update is called once per frame
    void Update()
    {
        if(grenade.transform.position.y <= startY - .3f)
        {

            Physics.gravity = Vector3.up * Math.Abs(gravity);
            grenade.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if(grenade.transform.position.y >= startY + .3f)
        {

            Physics.gravity = Vector3.up * gravity;
            grenade.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void DisableGrenade()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        hasGrenade = false;
        StartCoroutine(GrenadeRefresh(transform.GetChild(0).gameObject));
    }

    IEnumerator GrenadeRefresh(GameObject obj)
    {

        yield return new WaitForSeconds(15);
        obj.SetActive(true);
        hasGrenade = true;
        //Do Function here...
    }











}
