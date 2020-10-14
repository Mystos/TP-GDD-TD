using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int x, y;
    public GameObject player;
    public Vector3 originPosition;
    public GameObject enemy;
    public Text textGold;
    public Text textPdv;
    public GameObject level;
    public int pdvChateau = 1;
    public Canvas endCanvas;


    public float reloadTime = 2f;
    public float reloadProgress = 0f;

    private GameObject spawnPoint;
    private GameObject endPoint;
    private GameObject waypoints;
    internal Tilemap tilemap;
    internal List<TurretController> listTurret;

    public Grid grid;
    void Start()
    {
        foreach (Transform item in level.GetComponentsInChildren<Transform>())
        {
            if (item.gameObject.name == "SpawnPoint")
            {
                spawnPoint = item.gameObject;
            }
            if (item.gameObject.name == "EndPoint")
            {
                endPoint = item.gameObject;
            }
            if (item.gameObject.name == "Waypoints")
            {
                waypoints = item.gameObject;
            }
            if (item.gameObject.name == "Grid")
            {
                tilemap = item.GetComponentInChildren<Tilemap>();
            }
        }
        grid = new Grid(x, y, 1f, originPosition);
        listTurret = new List<TurretController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.GetXY(vec, out int x, out int y);
            Debug.Log(x + ":" + y);
        }

        reloadProgress += Time.deltaTime;
        if (reloadProgress >= reloadTime)
        {
            GameObject go = Instantiate<GameObject>(enemy);
            EnemyMovement em = go.GetComponent<EnemyMovement>();
            em.waypointsContainer = waypoints;
            em.endPoint = endPoint;
            em.gameManager = this.gameObject;

            reloadProgress = 0;
        }

        if (pdvChateau <= 0)
        {
            endCanvas.gameObject.SetActive(true);         
        }

        textGold.text = "Gold : " + player.GetComponent<PlayerController>().gold;
        textPdv.text = "Pdv : " + pdvChateau;

    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0f;
        return vec;
    }

}