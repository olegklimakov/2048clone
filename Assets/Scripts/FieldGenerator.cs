using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // Assign your tile prefab in the inspector
    public int rows = 4; // Number of rows in the game field
    public int columns = 4; // Number of columns in the game field
    public float tileWidth = 1.0f; // The width of each tile
    public float tileHeight = 1.0f; // The height of each tile
    public float spacing = 0.1f; // Spacing between tiles

    void Start()
    {
        GenerateGameField();
    }

    void GenerateGameField()
    {
        GameObject tilesContainer = new GameObject("TilesContainer");
        Transform parentTransform = tilesContainer.transform;

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
        tilesContainer.transform.position = new Vector3(-totalWidth / 2 + tileWidth / 2, totalHeight / 2 - tileHeight / 2, 0);
    }
}
