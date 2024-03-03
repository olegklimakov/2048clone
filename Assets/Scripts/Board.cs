using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

enum MoveDirection
{
    Up,
    Down,
    Left,
    Right,
}

public class Board : MonoBehaviour
{

    public GameItem itemPrefab;
    private TileGrid _grid;
    private GameObject _itemContainer;

    private List<GameItem> _items = new List<GameItem>(); 

    // Start is called before the first frame update
    void Start()
    {
        _grid = GetComponentInChildren<TileGrid>();
     
        Debug.Log(_grid.columns);
        Debug.Log(_grid.Cells[0, 1]);
        
        AddNewElement();
    }

    // Update is called once per frame
    void Update()
    {
        if (SwipeInput.swipedRight)
        {
            MoveItems(Vector2Int.right, _grid.columns - 2, -1, 0, 1);
            // AddNewElement();
        }

        if (SwipeInput.swipedLeft)
        {
            MoveItems(Vector2Int.left, 1, 1, 0, 1);
            // AddNewElement();
        }
        
        if (SwipeInput.swipedDown)
        {
            MoveItems(Vector2Int.up, 0, 1, _grid.rows - 2, -1);
            // AddNewElement();
        }
        
        if (SwipeInput.swipedUp)
        {
            MoveItems(Vector2Int.up, 0, 1, 1, 1);
            // AddNewElement();
        }
    }

    private void MoveItems(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        for (int column = startX; column >= 0 && column < _grid.columns; column += incrementX)
        {
            for(int row = startY; row >= 0 && row < _grid.rows; row += incrementY)
            {
                TileCell cell = _grid.GetCell(row, column);

                if (cell.IsOccupied())
                {
                    MoveItem(cell.item, direction);
                }
            }
        }
    }

    private void MoveItem(GameItem tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell neighbor = _grid.GetNeighbourCell(tile.cell, direction);

        while (neighbor != null)
        {
            if (neighbor.IsOccupied())
            {
                // merge
                break;
            }

            newCell = neighbor;
            neighbor = _grid.GetNeighbourCell(neighbor, direction);
        }

        if (newCell != null)
        {
            tile.cell.item = null;
            tile.MoveTo(newCell);
        }
    }

    private void AddNewElement()
    {
        var cell = _grid.GetFreeRandomCell();
        
        var item = Instantiate(itemPrefab, _grid.transform);
        var rt = item.GetComponent<RectTransform>();

        rt.anchoredPosition = cell.position;
        rt.sizeDelta = new Vector2(_grid.tileWidth, _grid.tileHeight);
        
        _items.Add(item);

        cell.item = item;
        item.cell = cell;
    }
}
