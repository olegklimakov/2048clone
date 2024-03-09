using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public TileState[] tileStates;

    // Start is called before the first frame update
    void Start()
    {
        _grid = GetComponentInChildren<TileGrid>();
     
        Debug.Log(_grid.columns);
        Debug.Log(_grid.Cells[0, 1]);
        
        AddNewElement();
        AddNewElement();
    }

    // Update is called once per frame
    void Update()
    {
        if (SwipeInput.swipedRight)
        {
            MoveItems(Vector2Int.right, _grid.columns - 2, -1, 0, 1);
        }

        if (SwipeInput.swipedLeft)
        {
            MoveItems(Vector2Int.left, 1, 1, 0, 1);
        }
        
        if (SwipeInput.swipedDown)
        {
            MoveItems(Vector2Int.down, 0, 1, 1, 1);
        }
        
        if (SwipeInput.swipedUp)
        {
            MoveItems(Vector2Int.up, 0, 1, _grid.rows - 2, -1);
        }
    }

    private void MoveItems(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        for (int column = startX; column >= 0 && column < _grid.columns; column += incrementX)
        {
            for(int row = startY; row >= 0 && row < _grid.rows; row += incrementY)
            {
                TileCell cell = _grid.GetCell(column, row);

                if (cell.IsOccupied())
                {
                    MoveItem(cell.item, direction);
                }
            }
        }
        
        AddNewElement();
    }

    private void MoveItem(GameItem tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell neighbor = _grid.GetNeighbourCell(tile.cell, direction);

        while (neighbor != null)
        {
            if (neighbor.IsOccupied())
            {
                if (CanMerge(neighbor.item, tile))
                {
                    Merge(tile, neighbor.item);
                    return;
                }

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

    private bool CanMerge(GameItem a, GameItem b)
    {
        return a.number == b.number;
    }

    private void Merge(GameItem a, GameItem b)
    {
        _items.Remove(a);
        a.Merge(b.cell);

        int index = Math.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length - 1);
        int number = b.number * 2;
        
        b.SetState(tileStates[index], number);
    }

    private int IndexOf(TileState state)
    {
        return tileStates.ToList().FindIndex(x => x == state);
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
        
        item.SetState(tileStates[0], 2);
    }

    private IEnumerator WaitForChanges()
    {
        
        
        
        yield return new WaitForSeconds(0.1f);
        
        
    }
}
