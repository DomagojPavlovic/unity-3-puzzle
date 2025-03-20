using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : Manager<T>
{
    public static T instance;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = (T) this;
        }

        OnAwake();
    }

    protected virtual void OnAwake() { }
}
