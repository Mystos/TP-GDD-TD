using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GridTest gameController;
    public GameObject prefabSelectedTurret;
    private TurretController turretController;

    public int gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        turretController = prefabSelectedTurret.GetComponent<TurretController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameController.grid.GetXY(vec, out int x, out int y);
            Debug.Log(x + ":" + y);

            // Le joueur achete une tourelle et la place
            if(turretController.price <= gold)
            {
                GameObject go = Instantiate(prefabSelectedTurret.gameObject);
                go.transform.position = new Vector3(x + .5f, y + .5f, 0);
                gold -= turretController.price;
            }
        }


    }
}
