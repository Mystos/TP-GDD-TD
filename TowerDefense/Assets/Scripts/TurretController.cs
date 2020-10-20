using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.Timeline;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public TurretType type;
    public List<GameObject> target;
    public GameObject bullet;
    public float radiusLvl1;
    public float radiusLvl2;
    public float radiusLvl3;
    internal int damage;
    public int damageLvl1;
    public int damageLvl2;
    public int damageLvl3;

    private CircleCollider2D collider;
    public GameObject turret;
    public GameObject tower;
    public Sprite towerLvl1;
    public Sprite towerLvl2;
    public Sprite towerLvl3;
    public Sprite turretLvl1;
    public Sprite turretLvl2;
    public Sprite turretLvl3;
    internal int level = 0;

    public float fireRate = 2f;
    public float reloadProgress = 0f;
    internal int price = 0;
    public int upgradePriceLvl1 = 0;
    public int upgradePriceLvl2 = 0;
    public int upgradePriceLvl3 = 0;





    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<CircleCollider2D>();
        target = new List<GameObject>();
        LevelUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && target.Count > 0)
        {
            FollowTarget();
            FireTarget();
        }
    }

    private void FollowTarget()
    {
        Vector3 dir = target[0].transform.position - turret.transform.position;
        dir.Normalize();

        float rot_z = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        Vector3 rotation = Quaternion.Lerp(turret.transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), Time.deltaTime * 5).eulerAngles;
        turret.transform.rotation = Quaternion.Euler(0f, 0f, rotation.z);
    }

    internal void LevelUp()
    {
        level++;
        Debug.Log("levelUp : " + level);

        SpriteRenderer spriteTurret = turret.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteTower = tower.GetComponent<SpriteRenderer>();

        switch (level)
        {
            case 1:
                spriteTurret.sprite = turretLvl1;
                spriteTower.sprite = towerLvl1;
                collider.radius = radiusLvl1;
                damage = damageLvl1;
                price = upgradePriceLvl2;
                break;
            case 2:
                spriteTurret.sprite = turretLvl2;
                spriteTower.sprite = towerLvl2;
                collider.radius = radiusLvl2;
                damage = damageLvl2;
                price = upgradePriceLvl3;

                break;
            case 3:
                spriteTurret.sprite = turretLvl3;
                spriteTower.sprite = towerLvl3;
                collider.radius = radiusLvl3;
                damage = damageLvl3;
                break;
        }

    }

    private void FireTarget()
    {
        reloadProgress += Time.deltaTime;
        if (reloadProgress >= fireRate)
        {
            if(type == TurretType.LanceurViennoiserie)
            {
                foreach (GameObject targ in target)
                {
                    GameObject go = Instantiate(bullet);
                    go.transform.position = this.transform.position;
                    go.GetComponent<BulletController>().target = targ;
                    go.GetComponent<BulletController>().damage = damage;
                }
            }
            else
            {
                    GameObject go = Instantiate(bullet);
                    go.transform.position = this.transform.position;
                    go.GetComponent<BulletController>().target = target[0];
                    go.GetComponent<BulletController>().damage = damage;
                
            }



            reloadProgress = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        target.Remove(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (target.Count <= 0)
        {
            target.Add(collision.gameObject);
        }
        else
        {
            float distance = (collision.gameObject.transform.position - this.transform.position).magnitude;
            float distanceTarget = (target[0].transform.position - this.transform.position).magnitude;

            if (distance < distanceTarget)
            {
                target.Insert(0, collision.gameObject);
            }
            else
            {
                target.Add(collision.gameObject);
            }
        }

    }

    public enum TurretType
    {
        CanonBaguette,
        LanceurViennoiserie,
        Verseur
    }
}
