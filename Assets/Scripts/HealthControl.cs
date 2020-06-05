using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    public int health = 100;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health -= 10;
            healthBar.SetHealth(health);
        }
    }
}
