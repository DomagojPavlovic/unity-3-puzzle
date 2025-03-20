using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    public void LoadLevelButton()
    {
        string input = UIManager.instance.menu.GetComponent<MenuPanel>().inputField.text;
        if(LevelManager.instance.AttemptToStartLevelFromCode(input))
        {
            UIManager.instance.menu.GetComponent<MenuPanel>().inputField.text = "";
            UIManager.instance.menu.GetComponent<MenuPanel>().ChangeMenus();
            UIManager.instance.ToggleMenu();
            LeftOriginalMenu();
        }
    }

    public void BackButton()
    {
        UIManager.instance.menu.GetComponent<MenuPanel>().ChangeMenus();
    }

    public void LeftOriginalMenu()
    {
        UIManager.instance.menu.GetComponent<MenuPanel>().LeftOriginalMenu();
    }
}
