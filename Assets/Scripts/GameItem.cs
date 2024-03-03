using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameItem : MonoBehaviour
{
    public TileCell cell;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveTo(TileCell cell)
    {
        this.cell = cell;
        cell.item = this;

        transform.position = cell.transform.position;
    }

}
