using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObjects<T>
{
    [SerializeField] private List<ObjectInfo> pool;
    
    public List<T> GetRandom()
    {
        List<T> output = new List<T>();

        foreach (var objectInfo in pool)
        {
            for(int i = 0; i < Random.Range(objectInfo.Min, objectInfo.Max); i++)
            {
                output.Add(objectInfo.Object);
            }
        }

        return output;
    }

    [System.Serializable]
    public struct ObjectInfo
    {
        public T Object;
        public int Min;
        public int Max;
    }
}
