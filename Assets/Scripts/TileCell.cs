using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour
{

    public int x;
    public int y;

    public Vector3 position;
    public GameItem item;

    
    public Boolean IsOccupied()
    {
        return item != null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
