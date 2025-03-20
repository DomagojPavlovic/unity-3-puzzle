using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Manager<LevelManager>
{
    public GameObject levels;
    public GameObject playersTile;
    public Camera mainCamera;

    public const int FIRST_LEVEL_INDEX = 0;

    public const int YELLOW_KEY_INDEX = 0;
    public const int GREEN_KEY_INDEX = 1;
    public const int BLUE_KEY_INDEX = 2;
    public const int RED_KEY_INDEX = 3;

    private int currentLevelIndex = FIRST_LEVEL_INDEX;

    private GameObject copiedCurrentLevelObject;

    public const float TIME_AFTER_GAME_OVER_TO_RESET = 3f;

    public bool lastLevelCompleted = false;

    protected override void OnAwake()
    {
        HideAllLevelTemplates();

        //Invoke(nameof(StartNewGame), 0.1f);
    }

    public void HideAllLevelTemplates()
    {
        foreach (Transform level in levels.transform)
        {
            level.gameObject.SetActive(false);
        }
    }

    public void StartNewGame()
    {
        StartGameFromLevel(FIRST_LEVEL_INDEX);
    }

    public void StartGameFromLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        LoadLevel();
    }

    public void LoadNextLevel()
    {
        ++currentLevelIndex;
        LoadLevel();
    }

    private void ReloadTheGameAfterItsCompleted()
    {
        Destroy(copiedCurrentLevelObject);
        //UIManager.instance.RestartForNewGame();
        SceneManager.LoadScene(0);
    }

    public bool AttemptToStartLevelFromCode(string code)
    {
        for (int i = 1; i < LevelCodes.LEVEL_CODES.Length; i++) {
            if (code == LevelCodes.LEVEL_CODES[i] && i < levels.transform.childCount)
            {
                StartGameFromLevel(i);
                return true;
            }
        }

        return false;
    }

    public void LoadLevel()
    {
        Destroy(copiedCurrentLevelObject);

        if (levels.transform.childCount == currentLevelIndex)
        {
            lastLevelCompleted = true;
            UIManager.instance.ShowTheYouWinPanel();
            Invoke(nameof(ReloadTheGameAfterItsCompleted), TIME_AFTER_GAME_OVER_TO_RESET);
            return;
        }

        copiedCurrentLevelObject = Instantiate(levels.transform.GetChild(currentLevelIndex).gameObject);
        copiedCurrentLevelObject.SetActive(true);

        Vector2 currentLevelBottomRightMostTile = new(0, 0);

        bool[] hasKeys = new bool[4];

        for(int i = 0; i < copiedCurrentLevelObject.transform.childCount; ++i)
        {
            Transform tileTransform = copiedCurrentLevelObject.transform.GetChild(i).gameObject.transform;

            if (tileTransform.position.x >= currentLevelBottomRightMostTile.x && tileTransform.position.y <= currentLevelBottomRightMostTile.y)
            {
                currentLevelBottomRightMostTile = new(tileTransform.position.x, tileTransform.position.y);
            }

            // tile doesn't have an object on it, go next
            if (tileTransform.childCount == 0)
            {
                continue;
            }
            Properties props = tileTransform.GetChild(0).GetComponent<Properties>();
            if (props != null && props.isPlayer)
            {
               playersTile = tileTransform.gameObject;
            }

            if (props == null || !props.isPickupable)
            {
                continue;
            }

            switch(props.keyType)
            {
                case KeyTypeEnum.Yellow:
                    hasKeys[YELLOW_KEY_INDEX] = true;
                    break;
                case KeyTypeEnum.Green:
                    hasKeys[GREEN_KEY_INDEX] = true;
                    break;
                case KeyTypeEnum.Blue:
                    hasKeys[BLUE_KEY_INDEX] = true;
                    break;
                case KeyTypeEnum.Red:
                    hasKeys[RED_KEY_INDEX] = true;
                    break;
            }
        }

        AdjustCameraToCurrentLevel(new(0, 0), currentLevelBottomRightMostTile);

        InventoryManager.instance.ClearInventory();

        UIManager.instance.CreateBottomGUIForCurrentLevel(currentLevelIndex, hasKeys);
    }

    public void AdjustCameraToCurrentLevel(Vector2 topLeftPosition, Vector2 botRightPosition)
    {
        Vector2 center = new((topLeftPosition.x + botRightPosition.x) / 2, (topLeftPosition.y + botRightPosition.y) / 2);

        float height = Mathf.Abs(topLeftPosition.y - botRightPosition.y) + GameManager.TILE_SIZE; // GameManager.TILE_SIZE used to add half on each side
        float width = Mathf.Abs(topLeftPosition.x - botRightPosition.x) + GameManager.TILE_SIZE;

        mainCamera.transform.position = center;

        const float sizeAdjustor = 2f;
        // current resolution of playable zone
        const float widthAdjustor = 16f / 8f;

        float barsOnTheSide = height / sizeAdjustor;
        float barsOnTopAndBottom = width / (sizeAdjustor * widthAdjustor);

        float size = Mathf.Max(barsOnTheSide, barsOnTopAndBottom);

        mainCamera.orthographicSize = size;
    }


}
