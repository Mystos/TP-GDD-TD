﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject waypointsContainer;
    public List<GameObject> waypoints;
    public GameObject currentWaypoint;
    int indexWaypoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform trans in waypointsContainer.GetComponentsInChildren<Transform>())
        {
            if(trans.gameObject != waypointsContainer)
            {
                waypoints.Add(trans.gameObject);
            }
        }
        currentWaypoint = waypoints[indexWaypoints];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypoint != null)
        {
            if (!transform.position.Equals(currentWaypoint.transform.position))
            {
                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.transform.position, Time.deltaTime);
            }
            else
            {
                GetNextWaypoint();
                if (currentWaypoint == null)
                {
                    Debug.Log("Bravo");
                }
            }
        }
    }

    public void GetNextWaypoint()
    {
        if(indexWaypoints + 1 < waypoints.Count)
        {
            currentWaypoint = waypoints[indexWaypoints + 1];
            indexWaypoints++;
        }
        else
        {
            currentWaypoint = null;
        }

    }

}
