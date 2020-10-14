using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    public GameObject prefabSelectedTurret;

    public int gold = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameController.grid.GetXY(vec, out int x, out int y);
            Debug.Log(x + ":" + y);

            TileBase tileClicked = gameController.tilemap.GetTile(gameController.tilemap.WorldToCell(new Vector3(x, y, 0)));
            Debug.Log(tileClicked.name);

            bool isThereATurret = false;

            TurretController turretController = null;

            foreach(TurretController item in gameController.listTurret)
            {
                if (item.gameObject.transform.position == new Vector3(x + .5f, y + .5f, 0))
                {
                    isThereATurret = true;
                    turretController = item;
                    break;
                }
            }

            if (!isThereATurret)
            {
                // on en invoque une
                if (tileClicked.name != "road")
                {
                    // Le joueur achete une tourelle et la place
                    if (prefabSelectedTurret.GetComponent<TurretController>().upgradePriceLvl1 <= gold)
                    {
                        GameObject go = Instantiate(prefabSelectedTurret.gameObject);
                        go.transform.position = new Vector3(x + .5f, y + .5f, 0);
                        gold -= go.GetComponent<TurretController>().upgradePriceLvl1;
                        gameController.listTurret.Add(go.GetComponent<TurretController>());
                    }
                }
            }
            else
            {
                // Il y a deja une tourelle donc on l'ameliore
                if (turretController.price <= gold)
                {
                    gold -= turretController.price;
                    turretController.LevelUp();

                }
            }



        }
    }
}
