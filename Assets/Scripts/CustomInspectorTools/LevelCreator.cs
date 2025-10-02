#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelCreator : EditorWindow
{
    private GameObject groundPrefab;
    private GameObject wallObjectPrefab;
    private int definedRows;
    private int definedCols;

    private const float TOP_LEFT_STARTING_POSITION_X = 0;
    private const float TOP_LEFT_STARTING_POSITION_Y = 0;
    private const float MOVE_DISTANCE_TO_NEXT_TILE_X = +1; // expands to the right
    private const float MOVE_DISTANCE_TO_NEXT_TILE_Y = -1; // expands downwards


    [MenuItem("CustomMenus/LevelCreator")]
    public static void ShowWindow()
    {
        GetWindow<LevelCreator>("Level Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn Prefab Settings", EditorStyles.boldLabel);

        groundPrefab =  EditorGUILayout.ObjectField("Ground Prefab", groundPrefab, typeof(GameObject), false) as GameObject;
        wallObjectPrefab =  EditorGUILayout.ObjectField("Wall Object Prefab", wallObjectPrefab, typeof(GameObject), false) as GameObject;

        definedRows = EditorGUILayout.IntField("Rows", definedRows);
        definedCols = EditorGUILayout.IntField("Cols", definedCols);

        if(GUILayout.Button("Generate"))
        {
            Generate();
        }
    }

    private void Generate()
    {
        if (groundPrefab == null || wallObjectPrefab == null || definedRows < 1 || definedCols < 1)
        {
            EditorUtility.DisplayDialog("Error", "Prefabs must be assigned and rows and cols must be > 0.", "OK");
        }

        // player defines how many playable rows and cols there are, add 2 for walls in each dimension
        int rows = definedRows + 2;
        int cols = definedCols + 2;


        GameObject[,] field = new GameObject[rows, cols];

        // create the field
        for (int row = 0; row < rows; ++row) {
            for(int col = 0; col < cols; ++col)
            {
                GameObject spawnedPrefab = Instantiate(groundPrefab);
                if (row == 0 || row == rows - 1 || col == 0 || col == cols - 1) 
                {
                    Instantiate(wallObjectPrefab, spawnedPrefab.transform); 
                } 
				
                float x = TOP_LEFT_STARTING_POSITION_X + col * MOVE_DISTANCE_TO_NEXT_TILE_X; 
                float y = TOP_LEFT_STARTING_POSITION_Y + row * MOVE_DISTANCE_TO_NEXT_TILE_Y;

                spawnedPrefab.transform.position = new Vector2(x, y);

                field[row, col] = spawnedPrefab;

                if(row > 0)
                {
                    spawnedPrefab.GetComponent<Position>().north = field[row - 1, col];
                    field[row - 1, col].GetComponent<Position>().south = spawnedPrefab;
                }
                if(col > 0)
                {
                    spawnedPrefab.GetComponent<Position>().west = field[row, col - 1];
                    field[row, col - 1].GetComponent<Position>().east = spawnedPrefab;
                }
            }
        }
    }
}

#endif