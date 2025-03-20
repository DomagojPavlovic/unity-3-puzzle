using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Manager<InventoryManager>
{

    [SerializeField] int yellowKeys;
    [SerializeField] int greenKeys;
    [SerializeField] int blueKeys;
    [SerializeField] int redKeys;


    protected override void OnAwake()
    {
        ClearInventory();
    }

    public void ClearInventory()
    {
        yellowKeys = 0; greenKeys = 0; blueKeys = 0; redKeys = 0;
    }

    public void CollectKey(KeyTypeEnum keyType)
    {
        switch(keyType)
        {
            case KeyTypeEnum.Blue:
                blueKeys++;
                break;
            case KeyTypeEnum.Green: 
                greenKeys++; 
                break;
            case KeyTypeEnum.Red: 
                redKeys++; 
                break;
            case KeyTypeEnum.Yellow: 
                yellowKeys++; 
                break;   
        }

        UIManager.instance.UpdateInventoryOnBottomGUI(GetKeysAsArray());
    }

    public bool AttemptToUnlock(KeyTypeEnum keyType)
    {
        bool result = false;

        switch (keyType)
        {
            case KeyTypeEnum.Blue:
                if (blueKeys > 0)
                {
                    blueKeys--;
                    result = true;
                }
                break;

            case KeyTypeEnum.Red:
                if (redKeys > 0)
                {
                    redKeys--;
                    result = true;
                }
                break;

            case KeyTypeEnum.Green:
                if (greenKeys > 0)
                {
                    greenKeys--;
                    result = true;
                }
                break;

            case KeyTypeEnum.Yellow:
                if (yellowKeys > 0)
                {
                    yellowKeys--;
                    result = true;
                }
                break;

        }

        if (result)
        {
            UIManager.instance.UpdateInventoryOnBottomGUI(GetKeysAsArray());
        }

        return result;
    }
    
    public int[] GetKeysAsArray()
    {
        int[] keyArray = new int[4];
        keyArray[LevelManager.GREEN_KEY_INDEX] = greenKeys;
        keyArray[LevelManager.BLUE_KEY_INDEX] = blueKeys;
        keyArray[LevelManager.RED_KEY_INDEX] = redKeys;
        keyArray[LevelManager.YELLOW_KEY_INDEX] = yellowKeys;
        return keyArray;
    }

}
