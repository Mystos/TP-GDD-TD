using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject target;
    public int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

// Update is called once per frame
void Update()
    {
        if (target != null && transform.position != target.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 5);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            if (target != null)
            {
                target.GetComponent<EnemyController>().GetDamage(damage);
            }
            GameObject.Destroy(this.gameObject);
        }
    }


}
