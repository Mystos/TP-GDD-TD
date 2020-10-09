using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public float radius;
    public int cost;
    public int damage;
    private CircleCollider2D collider;
    public GameObject turret;
    public GameObject tower;
    public float fireRate = 2f;
    public float reloadProgress = 0f;
    public int price = 0;

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
        Vector3 dir = target.transform.position - turret.transform.position;
        dir.Normalize();

        float rot_z = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        Vector3 rotation = Quaternion.Lerp(turret.transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), Time.deltaTime * 5).eulerAngles;
        turret.transform.rotation = Quaternion.Euler(0f, 0f, rotation.z);
    }

    private void FireTarget()
    {
        reloadProgress += Time.deltaTime;
        if (reloadProgress >= fireRate)
        {
            GameObject go = Instantiate(bullet);

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
