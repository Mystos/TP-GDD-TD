using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridTest : MonoBehaviour
{
    public int x, y;
    public Transform player;
    public Tilemap tilemap;
    public Vector3 originPosition;
    Grid grid;
    void Start()
    {
        grid = new Grid(x, y, 1f, originPosition);
        Debug.Log(tilemap.size);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.GetXY(vec, out int x, out int y);
            Debug.Log(x + ":" + y);

        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0f;
        return vec;
    }

}