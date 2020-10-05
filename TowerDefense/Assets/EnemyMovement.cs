using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public LinkedList<GameObject> waypoints;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!transform.position.Equals(waypoints.First.Value.transform.position))
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints.First.Value.transform.position, Time.deltaTime);
        }
        else
        {
            waypoints.RemoveFirst();
        }
    }
}
