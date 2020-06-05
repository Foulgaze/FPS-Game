using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCount : MonoBehaviour
{

    public int grenadeAmount = 1;
    public float nadeTimer = 2.0f;
    public GameObject grenade;
    public Boolean newGrenade = true;
    public float timeToExplode = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void increaseGrenade()
    {
        grenadeAmount += 1;
        try
        {
            GameObject there = transform.GetChild(2).Find("Grenade").gameObject;
        } catch( NullReferenceException)
        {
            Debug.Log("ERROR");
            GameObject nade = Instantiate(grenade) as GameObject;
            nade.transform.parent = transform.GetChild(2);
            nade.name = "Grenade";
            nade.transform.rotation = gameObject.transform.GetChild(2).Find("StationaryGrenade").transform.rotation;
            nade.transform.position = gameObject.transform.GetChild(2).Find("StationaryGrenade").transform.position;
            nade.SetActive(false);

        }
      
        
    }

    public void changeNade()
    {
        transform.GetComponent<WeaponSwitch>().changeWeapon(0);
    }

    // Update is called once per frame
    public void AddGrenade()
    {
        Debug.Log("ADDING");
        GameObject nade = Instantiate(grenade) as GameObject;
        nade.transform.parent = transform.GetChild(1);
        nade.SetActive(false);
        nade.name = "Grenade";
        StartCoroutine(LateCall(nade));
    }
    IEnumerator LateCall(GameObject obj)
    {

        yield return new WaitForSeconds(nadeTimer);
        
        obj.SetActive(true);
        obj.transform.rotation = gameObject.transform.GetChild(2).Find("StationaryGrenade").transform.rotation;
        obj.transform.position = gameObject.transform.GetChild(2).Find("StationaryGrenade").transform.position;
        //Do Function here...
    }

 

}
