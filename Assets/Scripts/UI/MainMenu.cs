using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGameButton()
    {
        LeftOriginalMenu();
        LevelManager.instance.StartNewGame();
        UIManager.instance.ToggleMenu();
    }

    public void SelectLevelButton()
    {
        UIManager.instance.menu.GetComponent<MenuPanel>().ChangeMenus();
    }

    public void RestartLevelButton()
    {
        LevelManager.instance.LoadLevel();
        UIManager.instance.ToggleMenu();
    }

    public void LeftOriginalMenu()
    {
        UIManager.instance.menu.GetComponent<MenuPanel>().LeftOriginalMenu();
    }
}
