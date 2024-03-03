using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileGrid : MonoBehaviour
{
    public TileCell tilePrefab;
 
    public int rows = 4; // Number of rows in the game field
    public int columns = 4; // Number of columns in the game field
    public float spacing = 10f; // Spacing between tiles

    private float _tileWidth;
    private float _tileHeight;

    private Rect _parentRect;
    public TileCell[,] Cells;
    void GenerateGameField()
    {
        // Calculate the total size of the grid to adjust positions for centering
        float totalWidth = _parentRect.width;
        float totalHeight = _parentRect.height;
        
        // Calculate the start position for the first tile to center the grid
        Vector2 startPosition = new Vector2(
            -(totalWidth / 2) + (_tileWidth / 2) + spacing,
            (totalHeight / 2) - (_tileHeight / 2) - spacing);
        
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                TileCell newTile = Instantiate(tilePrefab, transform);
                RectTransform rt = newTile.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(startPosition.x + col * (_tileWidth + spacing), startPosition.y - row * (_tileHeight + spacing));
                rt.sizeDelta = new Vector2(_tileWidth, _tileHeight);
                
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

        _tileWidth = (_parentRect.width - (columns + 1) * spacing)  / columns;
        _tileHeight = (_parentRect.height - (rows + 1) * spacing)  / rows;

        GenerateGameField();
    }
}
