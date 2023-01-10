using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonobehaviour<SingletonType> : MonoBehaviour
{
    public static SingletonType Instance { get; private set; }

    protected virtual void Awake ()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Singleton already exists, are you sure its single gameobject of that type?");
        }

        Instance = GetComponent<SingletonType>();
    }
}
