using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<T>();

            if (_instance == null)
                throw new System.Exception("Object not found");

            return _instance;
        }
    }

    private static T _instance;
}
