using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BottomGUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelCode;

    [SerializeField] GameObject blueKeysPanel;
    [SerializeField] TextMeshProUGUI numberOfBlueKeys;

    [SerializeField] GameObject greenKeysPanel;
    [SerializeField] TextMeshProUGUI numberOfGreenKeys;

    [SerializeField] GameObject redKeysPanel;
    [SerializeField] TextMeshProUGUI numberOfRedKeys;

    [SerializeField] GameObject yellowKeysPanel;
    [SerializeField] TextMeshProUGUI numberOfYellowKeys;


    public void SetLevelCode(string code)
    {
        levelCode.text = code;
    }

    public void DisableAllKeyPanels()
    {
        blueKeysPanel.SetActive(false);
        greenKeysPanel.SetActive(false);
        redKeysPanel.SetActive(false);
        yellowKeysPanel.SetActive(false);
    }

    public void EnableKeyPanels(bool[] hasKey)
    {
        DisableAllKeyPanels();
        blueKeysPanel.SetActive(hasKey[LevelManager.BLUE_KEY_INDEX]);
        greenKeysPanel.SetActive(hasKey[LevelManager.GREEN_KEY_INDEX]);
        redKeysPanel.SetActive(hasKey[LevelManager.RED_KEY_INDEX]);
        yellowKeysPanel.SetActive(hasKey[LevelManager.YELLOW_KEY_INDEX]);
    }

    public void SetAmountOfBlueKeys(int amount) 
    { 
        numberOfBlueKeys.text = amount.ToString(); 
    }

    public void SetAmountOfGreenKeys(int amount) 
    {  
        numberOfGreenKeys.text = amount.ToString();
    }

    public void SetAmountOfRedKeys(int amount) 
    {  
        numberOfRedKeys.text = amount.ToString();
    }

    public void SetAmountOfYellowKeys(int amount)
    {
        numberOfYellowKeys.text = amount.ToString();
    }

}
