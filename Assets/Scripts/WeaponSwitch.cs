using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Camera cam;
    public Canvas ammoVisual;
    int bulletMax;
    int bulletCount;
    bool[] firstSwitch = new bool[2];



    private void Start()
    {
        changeWeapon(0);
    }
    void Update()
    {
        if (Input.GetKeyDown("2"))
        {
            changeWeapon(1);
        }
        if (Input.GetKeyDown("1"))
        {
            changeWeapon(0);
        }
        if (Input.GetKeyDown("3"))
        {
            changeWeapon(2);
        }
        if (Input.GetKeyDown("4"))
        {
            changeWeapon(3);
        }
    }



    public void changeWeapon(int weaponNO)
    {
        int count = cam.transform.GetChildCount();
        string weaponName;
        for (int i = 0; i < count; i++)
        {
            cam.transform.GetChild(i).gameObject.SetActive(false);
        }

            
        switch (weaponNO)
        {
            case 0: weaponName = "Gun"; break;
            case 1: weaponName = "Grenade"; break;
            case 2: weaponName = "Sniper"; break;
            case 3: weaponName = "Shotgun"; break;
            default:weaponName = "Gun";break;

        }
        if(weaponName == "Sniper")
        {
            //This is considered the 2nd weapon not counting grenade
            bulletMax = cam.transform.Find(weaponName).GetComponent<Sniper>().bulletMax;
            bulletCount = cam.transform.Find(weaponName).GetComponent<Sniper>().bulletCount;
            cam.transform.Find(weaponName).GetComponent<Sniper>().reloading = false;




            ammoVisual.GetComponent<TextMeshProUGUI>().SetText(bulletCount + "/" + bulletMax);
        }
        if(weaponName == "Gun")
        {

            bulletMax = cam.transform.Find(weaponName).GetComponent<Gun>().bulletMax;
            bulletCount = cam.transform.Find(weaponName).GetComponent<Gun>().bulletCount;
            cam.transform.Find(weaponName).GetComponent<Gun>().reloading = false;
            ammoVisual.GetComponent<TextMeshProUGUI>().SetText(bulletCount + "/" + bulletMax);
        }
        if (weaponName == "Shotgun")
        {
            bulletMax = cam.transform.Find(weaponName).GetComponent<Shotgun>().bulletMax;
            bulletCount = cam.transform.Find(weaponName).GetComponent<Shotgun>().bulletCount;
            cam.transform.Find(weaponName).GetComponent<Shotgun>().reloading = false;
            ammoVisual.GetComponent<TextMeshProUGUI>().SetText(bulletCount + "/" + bulletMax);
        }
        //ammoVisual.GetComponent<TextMeshProUGUI>().SetText(bulletCount + "/" + bulletCount);
        GameObject weapon;
        try
        {
            weapon = cam.transform.Find(weaponName).gameObject;
        } catch(NullReferenceException e)
        {
            Debug.Log("Not find");
             weapon = cam.transform.Find("Gun").gameObject;
        }
        weapon.SetActive(true);
        
        
        
    }
}
