using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager<PlayerManager>
{
    private readonly float TIME_BETWEEN_TURNS = 0.2f;
    private bool canMove = true;

    void Update()
    {
        if (!canMove || UIManager.instance.currentlyInMenu)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            canMove = !GameManager.instance.Move(GameManager.DIRECTION_NORTH);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            canMove = !GameManager.instance.Move(GameManager.DIRECTION_WEST);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            canMove = !GameManager.instance.Move(GameManager.DIRECTION_SOUTH);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            canMove = !GameManager.instance.Move(GameManager.DIRECTION_EAST);
        } 

        if(!canMove)
        {
            Invoke(nameof(ResetCanMove), TIME_BETWEEN_TURNS);
        }

    }

    void ResetCanMove()
    {
        canMove = true;
    }

}
