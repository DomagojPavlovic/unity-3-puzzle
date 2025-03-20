using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    public Image bottomGUI;
    public Image menu;
    public GameObject youWinPanel;

    public bool playerHasLeftOriginalMenu;

    public bool currentlyInMenu;

    private void Update()
    {
        if (playerHasLeftOriginalMenu && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)) && !LevelManager.instance.lastLevelCompleted)
        {
            ToggleMenu();
        }
    }

    protected override void OnAwake()
    {
        menu.gameObject.SetActive(true);
        youWinPanel.SetActive(false);
        bottomGUI.gameObject.SetActive(false);
        playerHasLeftOriginalMenu = false;
        currentlyInMenu = true;
    }

    public void ToggleMenu()
    {
        bool currentState = menu.gameObject.activeSelf;
        currentlyInMenu = !currentState;
        menu.gameObject.SetActive(!currentState);
    }

    public void CreateBottomGUIForCurrentLevel(int levelIndex, bool[] hasKey)
    {
        bottomGUI.GetComponent<BottomGUI>().SetLevelCode(LevelCodes.LEVEL_CODES[levelIndex]);

        bottomGUI.GetComponent<BottomGUI>().EnableKeyPanels(hasKey);
    }

    public void UpdateInventoryOnBottomGUI(int[] keyArray)
    {
        BottomGUI gui = bottomGUI.GetComponent<BottomGUI>();

        gui.SetAmountOfBlueKeys(keyArray[LevelManager.BLUE_KEY_INDEX]);
        gui.SetAmountOfGreenKeys(keyArray[LevelManager.GREEN_KEY_INDEX]);
        gui.SetAmountOfRedKeys(keyArray[LevelManager.RED_KEY_INDEX]);
        gui.SetAmountOfYellowKeys(keyArray[LevelManager.YELLOW_KEY_INDEX]);
    }

    public void ShowTheYouWinPanel()
    {
        youWinPanel.SetActive(true);
    }

    public void RestartForNewGame()
    {
        OnAwake();
    }
}
