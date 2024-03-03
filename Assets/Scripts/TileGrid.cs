using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TileGrid : MonoBehaviour
{
    public TileCell tilePrefab;
 
    public int rows = 4; // Number of rows in the game field
    public int columns = 4; // Number of columns in the game field
    public float spacing = 10f; // Spacing between tiles

    public float tileWidth;
    public float tileHeight;

    private Rect _parentRect;
    public TileCell[,] Cells;
    void GenerateGameField()
    {
        // Calculate the total size of the grid to adjust positions for centering
        float totalWidth = _parentRect.width;
        float totalHeight = _parentRect.height;
        
        // Calculate the start position for the first tile to center the grid
        Vector2 startPosition = new Vector2(
            -(totalWidth / 2) + (tileWidth / 2) + spacing,
            (totalHeight / 2) - (tileHeight / 2) - spacing);
        
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                TileCell newTile = Instantiate(tilePrefab, transform);
                RectTransform rt = newTile.GetComponent<RectTransform>();
                Vector2 position = new Vector2(startPosition.x + col * (tileWidth + spacing),
                    startPosition.y - row * (tileHeight + spacing));
                newTile.position = position;
                rt.anchoredPosition = position;
                rt.sizeDelta = new Vector2(tileWidth, tileHeight);

                newTile.name = "Tile " + row + "," + col;
                newTile.x = row;
                newTile.y = col;
                
                TextMeshProUGUI textMesh = newTile.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null)
                {
                    textMesh.text = row + " - " + col;
                }

                Cells[row, col] = newTile;
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Cells = new TileCell[rows, columns];
        RectTransform objectRectTransform = gameObject.GetComponent<RectTransform>();
        _parentRect = objectRectTransform.rect;

        tileWidth = (_parentRect.width - (columns + 1) * spacing)  / columns;
        tileHeight = (_parentRect.height - (rows + 1) * spacing)  / rows;

        GenerateGameField();
    }

    public TileCell GetCell(int x, int y)
    {
        if (x > columns - 1 || y > rows - 1 || x < 0 || y < 0)
        {
            return null;
        }

        return Cells[x, y];
    }

    public TileCell GetNeighbourCell(TileCell cell, Vector2Int direction)
    {
        var x = cell.x;
        var y = cell.y;

        var newX = x += direction.x;
        var newY = y += direction.y;

        return GetCell(newX, newY);
    }

    public TileCell GetFreeRandomCell()
    {
        var lst = Cells.Cast<TileCell>().ToList().Where(x => !x.IsOccupied()).ToList();
        var index = Random.Range(0, lst.Count);
        return lst[index];
    }
}
