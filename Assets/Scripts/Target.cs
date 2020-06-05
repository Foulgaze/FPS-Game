using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

   
    public int health = 100;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
 
    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.SetHealth(health);
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}