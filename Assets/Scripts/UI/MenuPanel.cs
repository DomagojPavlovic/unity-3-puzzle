using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject mainMenuPanel;
    public GameObject subMenuPanel;

    public GameObject restartButton;
    public TMP_InputField inputField;

    public void Start()
    {
        mainMenuPanel.SetActive(true);
        subMenuPanel.SetActive(false);
        restartButton.SetActive(false);
    }

    public void ToggleMainMenu()
    {
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
    }

    public void ToggleSubMenu()
    {
        subMenuPanel.SetActive(!subMenuPanel.activeSelf);
    }

    public void ChangeMenus()
    {
        ToggleMainMenu();
        ToggleSubMenu();
    }

    public void ChangeTitleOfMenu()
    {
        title.text = "Options";
    }

    public void EnableRestartButton()
    {
        restartButton.SetActive(true);
    }

    public void LeftOriginalMenu()
    {
        ChangeTitleOfMenu();
        EnableRestartButton();
        UIManager.instance.bottomGUI.gameObject.SetActive(true);
        UIManager.instance.playerHasLeftOriginalMenu = true;
    }
}
