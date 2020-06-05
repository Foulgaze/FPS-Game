using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reload : MonoBehaviour
{

    public Boolean reloading;
    public float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(reloading)
        {
            timeLeft -= Time.deltaTime;
        }
        if(timeLeft < 0)
        {
            reloading = false;

        }
    }

    public void startReloading(float time)
    {
        reloading = true;
        timeLeft = time;
    }


}
