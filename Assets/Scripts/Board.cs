using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    private TileGrid _grid;
    
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _grid = GetComponentInChildren<TileGrid>();
     
        Debug.Log(_grid.columns);
        Debug.Log(_grid.Cells[0, 1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
