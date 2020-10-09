using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform target;
    public int damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

// Update is called once per frame
void Update()
    {
        if(transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 5);
        }
    }
}
