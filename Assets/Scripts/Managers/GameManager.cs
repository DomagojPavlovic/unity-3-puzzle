using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
    public static readonly int DIRECTION_NORTH = 1;
    public static readonly int DIRECTION_WEST = 2;
    public static readonly int DIRECTION_SOUTH = 3;
    public static readonly int DIRECTION_EAST = 4;

    public static readonly int TILE_SIZE = 1;

    public bool Move(int direction)
    {
        GameObject playersTile = LevelManager.instance.playersTile;

        return MovePlayer(playersTile, direction);
    }

    public bool MovePlayer(GameObject from, int direction)
    {
        if (LevelManager.instance.lastLevelCompleted)
        {
            return false;
        }

        GameObject to = GetTileInDirectionFromOrigin(from, direction);

        if (IsLevelComplete(to))
        {
            LevelManager.instance.LoadNextLevel();
            return true;
        }

        if (!CanMovePlayer(to, direction))
        {
            return false;
        }

        MoveObject(from, to);
        LevelManager.instance.playersTile = to;
        return true;
    }

    private bool IsLevelComplete(GameObject to)
    {
        if (to.transform.childCount != 0 && to.transform.GetChild(0).gameObject.GetComponent<Properties>().isExit)
        {
            return true;
        }
        return false;
    }

    public void MoveObject(GameObject tileFrom, GameObject tileTo)
    {
        tileFrom.transform.GetChild(0).gameObject.transform.SetParent(tileTo.transform, false);
    }

    public bool CanMovePlayer(GameObject to, int direction)
    {
        if (to.transform.childCount == 0)
        {
            return true;
        }

        Properties props = to.transform.GetChild(0).gameObject.GetComponent<Properties>();

        if (props == null)
        {
            return true;
        }

        if (props.isNotMovableOnto)
        {
            return false;
        }

        if (props.isPickupable)
        {
            InventoryManager.instance.CollectKey(props.keyType);
            ClearTile(to);
            return true;
        }

        if (props.isUnlockable && InventoryManager.instance.AttemptToUnlock(props.keyType))
        {
            ClearTile(to);
            return true;
        }

        GameObject tileToPushObjectTo = GetTileInDirectionFromOrigin(to, direction);
        if (props.isPushable && CanPushObject(tileToPushObjectTo) && !HasChildAndIsPushableInto(tileToPushObjectTo))
        {
            MoveObject(to, tileToPushObjectTo);
            return true;
        } else if(props.isPushable && CanPushObject(tileToPushObjectTo) && HasChildAndIsPushableInto(tileToPushObjectTo))
        {
            ClearTile(to);
            ClearTile(tileToPushObjectTo);
            return true;
        }

        return false;
    }

    public bool HasChildAndIsPushableInto(GameObject tile)
    {
        if (tile.transform.childCount == 0)
        {
            return false;
        }

        Properties destinationProperties = tile.transform.GetChild(0).gameObject.GetComponent<Properties>();

        return destinationProperties.isPushableInto;
    }

    public void ClearTile(GameObject tile)
    {
        Destroy(tile.transform.GetChild(0).gameObject);
    }

    public bool CanPushObject(GameObject to)
    {

        if (to.transform.childCount == 0)
        {
            return true;
        }
        Properties props = to.transform.GetChild(0).gameObject.GetComponent<Properties>();

        if (props == null)
        {
            return true;
        }

        return !props.isNotPushableOnto;
    }

    public GameObject GetTileInDirectionFromOrigin(GameObject from, int direction)
    {
        if (direction == DIRECTION_NORTH)
        {
            return from.GetComponent<Position>().north;
        }        
        if (direction == DIRECTION_WEST)
        {
            return from.GetComponent<Position>().west;
        }        
        if (direction == DIRECTION_SOUTH)
        {
            return from.GetComponent<Position>().south;
        }
        if (direction == DIRECTION_EAST)
        {
            return from.GetComponent<Position>().east;
        }

        return null;
    }
}