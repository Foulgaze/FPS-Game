using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }
    public void activate()
    {
        gameObject.SetActive(true);
    }
}
