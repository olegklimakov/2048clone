using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

class GridPosition
{
    public GridPosition(float x, float y)
    {
        X = x;
        Y = y;
        IsEmpty = true;
    }

    // Property implementation:
    public float X { get; set; }

    public float Y { get; set; }

    public Boolean IsEmpty { get; set; }

    public override string ToString()
    {
        return "Tile: " + X + " - " + Y;
    }
}

public class FieldGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // Assign your tile prefab in the inspector

    public GameObject itemPrefab;
    
    public int rows = 4; // Number of rows in the game field
    public int columns = 4; // Number of columns in the game field
    public float tileWidth = 1.0f; // The width of each tile
    public float tileHeight = 1.0f; // The height of each tile
    public float spacing = 0.1f; // Spacing between tiles

    private List<GridPosition> _field = new List<GridPosition>();
    
    private GameObject _tilesContaier;

    void Start()
    {
        GenerateGameField();
        PlaceInitialItems();
    }
    
    void PlaceInitialItems()
    {
        // Simple example logic for placing two items
        PlaceRandomItem();
        PlaceRandomItem();
    }
    
    void PlaceRandomItem()
    {
        
        Debug.Log(_field);
        for (int i = 0; i < _field.Count; i++)
        {
            Debug.Log(_field[i].ToString());
        }
        
        
        
        Transform parentTransform = _tilesContaier.transform;
        List<GridPosition> emptyPositions = _field.Where(item => item.IsEmpty).ToList();
        // Assuming you have a way to track which positions on the grid are empty,
        // populate emptyPositions with those locations

        if (emptyPositions.Count > 0)
        {
            int randomIndex = Random.Range(0, emptyPositions.Count);
            Vector3 position = new Vector3(emptyPositions[randomIndex].X, emptyPositions[randomIndex].Y, 0);
            
            // Convert grid position to world space or adjust as necessary
            Instantiate(itemPrefab, position, Quaternion.identity, parentTransform);
            // Optionally, set the item's value here (e.g., to "2" or "4")
        }
    }

    Vector3 GridToWorldPosition(Vector2 gridPos)
    {
        // Implement conversion from grid position to the world position
        // This depends on how you've structured your grid
        return new Vector3(gridPos.x * tileWidth, gridPos.y * tileWidth, 0);
    }

    void GenerateGameField()
    {
        _tilesContaier = new GameObject("TilesContainer");
        Transform parentTransform = _tilesContaier.transform;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calculate the position for each tile
                float posX = col * (tileWidth + spacing);
                float posY = row * -(tileHeight + spacing); // Negative to move up
                
                // Instantiate the tile at the calculated position
                GameObject tile = Instantiate(tilePrefab, new Vector3(posX, posY, 0), Quaternion.identity, parentTransform);
                tile.name = "Tile " + row + "," + col;
                
                // Find the TextMeshPro component and set the text
                TextMeshPro textMesh = tile.GetComponentInChildren<TextMeshPro>();
                if (textMesh != null)
                {
                    textMesh.text = "Tile " + row + "," + col;
                }
                
                // Optional: Adjust tile size if your prefab needs resizing
                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    float scaleRatioX = tileWidth / sr.sprite.bounds.size.x;
                    float scaleRatioY = tileHeight / sr.sprite.bounds.size.y;
                    tile.transform.localScale = new Vector3(scaleRatioX, scaleRatioY, 1);
                }
            }
        }

        // Center the tilesContainer based on the total size of the game field
        float totalWidth = columns * tileWidth + (columns - 1) * spacing;
        float totalHeight = rows * tileHeight + (rows - 1) * spacing;
        _tilesContaier.transform.position = new Vector3(-totalWidth / 2 + tileWidth / 2, totalHeight / 2 - tileHeight / 2, 0);
        
        
        for (int i = 0; i < _tilesContaier.transform.childCount; i++)
        {
            Transform child = _tilesContaier.transform.GetChild(i);
            // Now you have access to the child transform
            // You can do something with it, for example, print its name
            Debug.Log(child.name);
            GridPosition position = new GridPosition(child.position.x, child.position.y);
            _field.Add(position);
        }
    }
}
