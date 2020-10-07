using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public float radius;
    public int cost;
    public int damage;
    private CircleCollider2D collider;

    public float fireRate = 2f;
    public float reloadProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<CircleCollider2D>();
        collider.radius = radius;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            FollowTarget();
            FireTarget();
        }
    }

    private void FollowTarget()
    {
        transform.up = Vector3.RotateTowards(transform.up,target.transform.position,360, Time.deltaTime);
    }

    private void FireTarget()
    {
        reloadProgress += Time.deltaTime;
        if (reloadProgress >= fireRate)
        {
            target.GetComponent<EnemyController>().GetDamage(damage);
            reloadProgress = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (target == null)
        {
            target = collision.gameObject;
        }
        else
        {
            float distance = (collision.gameObject.transform.position - this.transform.position).magnitude;
            float distanceTarget = (target.transform.position - this.transform.position).magnitude;

            if (distance < distanceTarget)
            {
                target = collision.gameObject;
            }
        }
    }
}
