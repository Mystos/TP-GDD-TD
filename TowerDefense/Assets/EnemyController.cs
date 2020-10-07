using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();

    }

    private void CheckDeath()
    {
        if(currentHealth <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
    }
}
